using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AmazonScraper.Models;
using HtmlAgilityPack;
using NUglify.Helpers;
using NUglify.JavaScript.Syntax;

namespace AmazonScraper.Utility

{
    public static class Scraper
    {
        /*
         requestLink = The base of the link on which we will make the GET request for the html page 
        */
        private static string requestLink = "https://www.amazon.com/s?k=";

        /*
         autoCompleteRequestLink = The base of the link on which we will make the GET request for autocomplete suggestions
        */
        //private static readonly string autoCompleteRequestLink =
        //    "https://completion.amazon.com/api/2017/suggestions?page-type=Gateway&lop=en_US&client-info=amazon-search-ui&mid=ATVPDKIKX0DER&alias=aps&ks=66&prefix=";

        private static readonly FbaToolkitApiClient apiClient = new FbaToolkitApiClient();

        private static readonly Dictionary<string, string> availableCategories = apiClient.GetAvailableCategories();
        private static Dictionary<(string,string),List<int>> ranksToRequest = new Dictionary<(string, string), List<int>>();

        public static AmazonPage GetPageForKeywords(string keywords, CookieLocation location)
        {
            var page = new AmazonPage();

            var doc = LoadDocument(RequestLinkSetup(keywords),location);
            page.NoOfItems = ExtractNumberOfItemsInNiche(doc);
            page.Items = ExtractItems(doc);
            page.AveragePrice = ComputeAveragePrice(page.Items);
            page.AverageEstimatedRevenue = ComputeAverageEstimatedRevenue(page.Items);
            page.AverageRating = ComputeAverageRating(page.Items);
            page.AverageReviewsTopProducts = ComputeAverageNumberOfRatingForTopProducts(page.Items);
            page.CrowdedMarket = ComputeCrowdedMarket(page.NoOfItems);
            return page;
        }

        public static decimal ComputeAveragePrice(List<AmazonItem> items)
        {
            var sum = items.Sum(x => x.Price);
            return System.Math.Round(sum / items.Count,2);
        }

        public static decimal ComputeAverageRating(List<AmazonItem> items)
        {
            var sum = items.Sum(x => x.Rating);
            return System.Math.Round(sum / items.Count, 2);
        }

        public static int ComputeAverageNumberOfRatingForTopProducts(List<AmazonItem> items)
        {
            int topProducts = 0;
            int average = items.Sum(x =>
            {
                if (!x.isTopProduct()) return 0;
                topProducts++;
                return x.NumberOfRatings;
            });
            if (topProducts == 0) return items.Sum(x => x.NumberOfRatings) / items.Count;

            return average/topProducts;
        }

        public static decimal ComputeCrowdedMarket(int numberOfItems) => numberOfItems / 10000;
      

        public static decimal ComputeAverageEstimatedRevenue(List<AmazonItem> items)
        {
            decimal sum = 0;
            int count = 0;
            items.ForEach(item =>
            {
                if (item.EstimatedRevenue != 0)
                {
                    count++;
                    sum += item.EstimatedRevenue;
                }
            });
            if (sum == 0) return 0;
            return System.Math.Round(sum / count, 2);
        }

        private static string RequestLinkSetup(string keyword)
        {
            var builder = new StringBuilder();
            builder.Append(requestLink);
            keyword.Split(' ').ToList().ForEach(x => builder.Append(x).Append("+"));
            // delete the + from the end
            return builder.ToString().Remove(builder.ToString().Length - 1, 1);
        }

        public static HtmlDocument LoadDocument(string link, CookieLocation location)
        {
            var handler = new HttpRequestHandler(location);
            var response = handler.DownloadPageAsync(link);
            var document = new HtmlDocument();
            document.LoadHtml(response);
            return document;
        }


        public static int ExtractNumberOfItemsInNiche(HtmlDocument document)
        {
            var dom = document.DocumentNode.ChildNodes[5].SelectNodes("//div[@class ='a-section a-spacing-small a-spacing-top-small']");
            var html = HttpUtility.HtmlDecode(dom[0].ChildNodes[1].InnerText);
            var numOfItems = html.Contains("over") ? html.Split(' ')[3] : html.Split(' ')[2];
            return Int32.Parse(numOfItems.Replace(",", ""));
        }

        public static List<AmazonItem> ExtractItems(HtmlDocument document)
        {
            var Items = document.DocumentNode.Descendants().Where(n => n.Attributes.Any(a => a.Name == "data-asin" && a.Value != "")).ToList();
            var amazonItems = new List<AmazonItem>();
            Parallel.ForEach(Items, item =>
            {
                string title,numberOfRatings, price, rating;
                var id = Items.IndexOf(item);
                var asin = item.Attributes["data-asin"].Value;
                if (asin.Length != 10) return;
                var link = "https://www.amazon.com/dp/" + asin;
                try
                {
                    var productPage = LoadDocument(link, CookieLocation.US);
                    // Title section

                    var titleNode = productPage.DocumentNode.SelectSingleNode(".//span[@id='productTitle']");
                    if (titleNode == null) return;
                    else title = titleNode.InnerText.Replace("\n", "").Replace("  ", "");

                    //Number of ratings sections

                    var numberOfRatingsNode = productPage.DocumentNode.SelectSingleNode(".//div[@data-hook='total-review-count']");
                    if (numberOfRatingsNode == null) numberOfRatings = "0";
                    else numberOfRatings = Regex.Match(numberOfRatingsNode.InnerText, @"\d+").Value;

                    //Rating section 

                    var ratingNode = productPage.DocumentNode.SelectSingleNode(".//i[@data-hook='average-star-rating']");
                    if (ratingNode == null) rating = "0000";
                    else rating = ratingNode.InnerText;

                    //Price section

                    var priceNode = productPage.DocumentNode.SelectSingleNode(".//span[@id='priceblock_ourprice']");
                    if (priceNode == null)
                    {
                        priceNode = productPage.DocumentNode.SelectSingleNode(".//span[@id='priceblock_saleprice']");
                        if (priceNode == null) price = "0";
                        else price = priceNode.InnerText.Replace("$", "");
                    }
                    else price = priceNode.InnerText.Replace("$", "");

                    if (price.Contains("-"))
                        price = price.Split('-')[1].Replace("$", "").Trim();

                    //Image section
                    var imageNode = productPage.DocumentNode.SelectSingleNode(".//div[contains(@class, 'imgTagWrapper')]");
                    string imageLink = "";
                    if (imageNode == null)
                    {
                        imageNode = productPage.DocumentNode.SelectSingleNode(".//img[contains(@class, 'a-dynamic-image')]");
                        imageLink = imageNode.Attributes["src"].Value;


                    }
                    else
                    {
                        var images = imageNode.ChildNodes["img"].Attributes["data-a-dynamic-image"].Value.Split(',');
                        imageLink = images[0].Remove(0, 7).Split('&')[0];
                    }


                    //Best seller section
                    var bestSeller = GetBestSellerRank(GetBestSellerInfoLinks(productPage));
                    if (bestSeller == null) return;
                    lock (ranksToRequest)
                    {
                        bestSeller.ForEach(x =>
                        {
                            if (!availableCategories.ContainsKey(x.CategoryId)) return;
                            if (ranksToRequest.ContainsKey((availableCategories[x.CategoryId], x.CategoryId)))

                                ranksToRequest[(availableCategories[x.CategoryId], x.CategoryId)].Add(x.Rank);
                            else
                                ranksToRequest.Add((availableCategories[x.CategoryId], x.CategoryId), new List<int>() { x.Rank });

                        });
                    }


                    var amazonItem = new AmazonItem
                    {
                        Id = id,
                        Name = title,
                        Rating = Decimal.Parse(rating.Substring(0, 3)),
                        NumberOfRatings = Int32.Parse(numberOfRatings),
                        Price = Decimal.Parse(price),
                        Link = link,
                        ASIN = asin,
                        Image = imageLink,
                        BestSellerRank = bestSeller
                    };

                    amazonItems.Add(amazonItem);
                }
                catch (Exception e)
                {
                    return;
                }

                
            });
            ranksToRequest.ForEach( pair => { pair = new KeyValuePair<(string, string), List<int>>(pair.Key,pair.Value.Distinct().ToList());});
            amazonItems = amazonItems.DistinctBy(x => x.ASIN).ToList();
            getEstimatedSales(amazonItems);
            return amazonItems.OrderBy(x => x.Id).ToList();
        }

        private static void getEstimatedSales(List<AmazonItem> amazonItems)
        {
            var estimatedSales = new Dictionary<int, decimal>();
            string categoryId = "";

            KeyValuePair<(string, string), List<int>> maxpair = ranksToRequest.First();

            if (ranksToRequest.Count > 1)
            {
                ranksToRequest.ForEach(pair =>
                {
                    if (pair.Value.Count > maxpair.Value.Count) maxpair = pair;
                });
            }

            var category = maxpair.Key;
             
            if (ranksToRequest[category].Count > 50)
            {
                var first = ranksToRequest[category].Take(50).ToList();
                var second = ranksToRequest[category].Skip(50).ToList();
                var firstResults = apiClient.GetEstimatedSales(first, category.Item1);
                Thread.Sleep(1000);
                var secondResults = apiClient.GetEstimatedSales(second, category.Item1);
                firstResults.results.ForEach(result =>
                {
                    decimal sales = 0;
                    var monthEstimation = result.result_30day;
                    if (!decimal.TryParse(monthEstimation.estimation.sales, out sales)) return;
                    if (!estimatedSales.ContainsKey(monthEstimation.estimation.rank))
                        estimatedSales.Add(monthEstimation.estimation.rank, sales);

                });
                secondResults.results.ForEach(result =>
                {
                    decimal sales = 0;
                    var monthEstimation = result.result_30day;
                    if (!decimal.TryParse(monthEstimation.estimation.sales, out sales)) return;
                    if (!estimatedSales.ContainsKey(monthEstimation.estimation.rank))
                        estimatedSales.Add(monthEstimation.estimation.rank, sales);

                });

            }
            else
            {
                var estimationResults = apiClient.GetEstimatedSales(ranksToRequest[category], category.Item1);
                estimationResults.results.ForEach(result =>
                {
                    decimal sales = 0;
                    var monthEstimation = result.result_30day;
                    if (monthEstimation.estimation.sales == "NaN" && monthEstimation.estimation.rank < monthEstimation.min.rank)
                    {
                        decimal.TryParse(monthEstimation.min.sales, out sales);
                        estimatedSales.Add(monthEstimation.estimation.rank,sales);
                        return;
                    }
                    if (!decimal.TryParse(monthEstimation.estimation.sales, out sales)) return;
                    if(!estimatedSales.ContainsKey(monthEstimation.estimation.rank))
                        estimatedSales.Add(monthEstimation.estimation.rank, sales);


                });
            }

            categoryId = category.Item2;
           
            amazonItems.ForEach(item =>
            {
                item.BestSellerRank.ForEach(rank =>
                {
                    if (rank.CategoryId != categoryId) return;
                    if (!estimatedSales.ContainsKey(rank.Rank)) return;
                    rank.NoOfSales = estimatedSales[rank.Rank];
                    item.EstimatedRevenue = System.Math.Round(item.Price * rank.NoOfSales,2);

                });
            });
        }


        public static (string, HtmlNodeCollection) GetBestSellerInfoLinks(HtmlDocument doc)
        {
        
            var collection = doc.DocumentNode.SelectSingleNode(".//div[contains(@id,'dp-container')]");
            if (collection == null) return (null,null);
            return (collection.InnerText, collection.SelectNodes(".//a[contains(@href,'bestsellers')]"));

        }

        public static List<BestSellerRank> GetBestSellerRank((string,HtmlNodeCollection) information)
        {
            var (plainTextFromHtml, linkNodes) = information;
            if (plainTextFromHtml == null || linkNodes == null) return null;
            plainTextFromHtml = plainTextFromHtml.Replace("&nbsp;", " ").Replace("\n", "").Replace("\t", "").Replace("     ", "");
            var matches = (from Match m in Regex.Matches(plainTextFromHtml, @"(#[0-9]+(,[0-9][0-9][0-9])?)(\s*in\s*)(([a-zA-Z',]+ )+(& ([a-zA-Z']+ )*)*)", RegexOptions.Multiline) select m.Value.Trim('\n')).ToList();


            var result = new List<(string,string)>();
            linkNodes.ForEach(item =>
                result.Add(
                    item.InnerText.Contains("See Top 100 in")
                        ? (
                            matches.FirstOrDefault(x => 
                                            x.Contains(
                                                item.InnerText.Replace("See Top 100 in", "")
                                                )
                                            ),
                             item.Attributes["href"].Value
                            )
                        : (
                            matches.FirstOrDefault(x =>
                                                x.Contains(item.InnerText)
                                                ),
                            item.Attributes["href"].Value
                            )
                        )
                    );

            var resultList = new List<BestSellerRank>();
            result.ForEach(item =>
                {
                    var (rankAndCategory, link) = item;
                    if (rankAndCategory == null) return;
                    var split = rankAndCategory.Split(new[] {" in "}, StringSplitOptions.None);
                    var preparedString = split[0].Replace(" ","").Replace("#", "").Replace(",", "");
                    int startPos = link.IndexOf("/bestsellers/") + "/bestsellers/".Length;
                    int length = link.IndexOf("/ref=") - startPos;
                    var id = link.Substring(startPos, length);
                    resultList.Add(new BestSellerRank() {CategoryId = id,CategoryName = split[1], Rank = int.Parse(preparedString) });
                }
                );


            return resultList;

        }
    }
}