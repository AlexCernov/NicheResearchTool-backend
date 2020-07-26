using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlibabaScraper.Models;
using MoreLinq.Extensions;

namespace AlibabaScraper.Utility
{
    public static class Scraper
    {
        private static string baseAddressSearch = "https://www.alibaba.com/trade/search?SearchText=";


        public static AlibabaPage GetPageWithKeywords(string keywords, int entries)
        {
            var page = new AlibabaPage {Link = RequestLinkSetup(keywords)};
            var data = LoadItemsFromDocument(page.Link, entries);
            page.FilterData = data.Item2;
            page.Items = ExtractItems(data.Item1);
            (page.AverageMinPrice, page.AverageMaxPrice) = CalculateAveragePrice(page.Items);
            page.AverageResponseRate = decimal.Round(CalculateAverageResponseRate(page.Items),2);
            return page;
        }

        public static AlibabaPage GetPageWithLink(string url, int entries)
        {
            var data = HttpRequestHandler.GetOffers(url, entries);
            var preparedLink = url.Split('&')[0];
            var page = new AlibabaPage {Link = preparedLink, Items = ExtractItems(data.Item1), FilterData = data.Item2};

            (page.AverageMinPrice, page.AverageMaxPrice) = CalculateAveragePrice(page.Items);
            page.AverageResponseRate = decimal.Round(CalculateAverageResponseRate(page.Items), 2);

            return page;
        }

        public static string RequestLinkSetup(string keywords)
        {
            var builder = new StringBuilder();
            builder.Append(baseAddressSearch);
            keywords.Split(' ').ToList().ForEach(x => builder.Append(x).Append("+"));
            // delete the + from the end
            return builder.ToString().Remove(builder.ToString().Length - 1, 1);
        }

        public static (List<OfferList>,SnData) LoadItemsFromDocument(string url,int entries)
        {
            return HttpRequestHandler.GetOffers(url, entries);
        }

        public static List<AlibabaItem> ExtractItems(IEnumerable<OfferList> offers)
        {
            var returnList = new List<AlibabaItem>();

            foreach (var offer in offers)
            {
              
                returnList.Add(new AlibabaItem
                {
                    Id = long.Parse(offer.id.ToString()),
                    Images = offer.image.mainImage,
                    Name = offer.information.puretitle,
                    Seller = offer.supplier.supplierName,
                    SellerLink = offer.supplier.supplierHref,
                    SellerTimeOnPlatform = int.Parse(offer.supplier.supplierYear),
                    ResponseRate = offer.company.record.responseRate,
                    ResponseTime = offer.company.record.responseTime,
                    GoldStatus = offer.supplier.goldSupplier,
                    ReviewScore = offer.reviews.productScore == null ? 0 :decimal.Parse(offer.reviews.productScore),
                    ReviewCount = offer.reviews.reviewCount,
                    Link = offer.information.productUrl,
                    MinPrice = offer.promotionInfoVO.originalPriceFrom == null ? 0 : decimal.Parse(offer.promotionInfoVO.originalPriceFrom),
                    MaxPrice = offer.promotionInfoVO.originalPriceTo == null ? 0 : decimal.Parse(offer.promotionInfoVO.originalPriceTo),
                    MinimumOrder = offer.tradePrice.minOrder == null ? 0 : int.Parse(offer.tradePrice.minOrder.Split(' ')[0].Split('.')[0]),
                    Country = offer.supplier.supplierCountry.name
                });
            }

            return returnList.DistinctBy(x => x.Id).ToList();
        }

        public static decimal CalculateAverageResponseRate(List<AlibabaItem> Items)
        {
            if (Items.Count == 0) return 0;
            var averageList = new List<decimal>();
            foreach (var item in Items)
                if (item.ResponseRate == null || decimal.Parse(item.ResponseRate.Replace("%", "")) == 0)
                    continue;
                else averageList.Add(decimal.Parse(item.ResponseRate.Replace("%", "")));

            return averageList.Sum() / averageList.Count;
        }

        public static (decimal, decimal) CalculateAveragePrice(List<AlibabaItem> Items)
        {
            if (Items.Count == 0) return (0,0);
            var sumMin = Items.Sum(x => x.MinPrice);
            var sumMax = Items.Sum(x => x.MaxPrice);

            return (decimal.Round(sumMin / Items.Count, 2), decimal.Round(sumMax / Items.Count, 2));

        }
    }
}