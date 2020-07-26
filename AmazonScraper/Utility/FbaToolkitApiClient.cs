using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AmazonScraper.Models;
using Newtonsoft.Json;

namespace AmazonScraper.Utility
{
    class FbaToolkitApiClient
    {
        private static readonly string base_address = "https://www.fbatoolkit.com/api/v1/";
        private static readonly string API_KEY = "NBciiqJvlVxHWkGrSAzKkgI6Jix5ZpqK:";

        public FbaToolkitApiClient()
        {
            
        }

        public RankSalesEstimatorModelResponse GetEstimatedSales(List<int> rankPositionsList, string cat, string market = "US")
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), base_address + "estimate_sales_by_ranks");
            var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes(API_KEY));
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

            var requestModel = new RankSalesEstimatorModelRequest() {marketplace = market, category = cat, rank = rankPositionsList};


            request.Content = new StringContent(JsonConvert.SerializeObject(requestModel));
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var result = httpClient.SendAsync(request).Result;
            using var responseStream = result.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            using var streamReader = new StreamReader(responseStream);
            return JsonConvert.DeserializeObject<RankSalesEstimatorModelResponse>(streamReader.ReadToEndAsync().Result);
        }

        public string Test()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://www.fbatoolkit.com/api/v1/estimate_sales_by_rank"))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("NBciiqJvlVxHWkGrSAzKkgI6Jix5ZpqK:"));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                    request.Content = new StringContent("{\"marketplace\": \"DE\", \"category\": \"Drogerie & Ku00f6rperpflege\", \"rank\": 10000}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var result = httpClient.SendAsync(request).Result;
                    using var responseStream = result.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    //using var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
                    using var streamReader = new StreamReader(responseStream);
                    return streamReader.ReadToEndAsync().Result;
                }
            }
        }

        public Dictionary<string, string> GetAvailableCategories()
        {
            var dict = new Dictionary<string,string>();

            dict.Add("toys-and-games", "Toys & Games");
            dict.Add("automotive", "Automotive");
            dict.Add("hpc", "Health & Household");
            dict.Add("hi", "Tools & Home Improvement");
            dict.Add("beauty", "Beauty & Personal Care");
            dict.Add("home-garden", "Home & Kitchen");
            dict.Add("grocery", "Grocery & Gourmet Food");
            dict.Add("kitchen", "Kitchen & Dining");
            dict.Add("sporting-goods", "Sports & Outdoors");
            dict.Add("lawn-garden", "Patio, Lawn & Garden");
            dict.Add("electronics", "Electronics");
            dict.Add("pet-supplies", "Pet Supplies");
            dict.Add("office-products", "Office Products");
            dict.Add("arts-crafts", "Arts, Crafts & Sewing");
            dict.Add("industrial", "Industrial & Scientific");
            dict.Add("baby-products", "Baby");
            dict.Add("wireless", "Cell Phones & Accessories");
            dict.Add("musical-instruments", "Musical Instruments");
            dict.Add("books", "Books");
            dict.Add("videogames", "Video Games");
            dict.Add("appliances", "Appliances");
            dict.Add("photo", "Camera & Photo");
            dict.Add("pc", "Computers & Accessories");
            dict.Add("electronics/12097478011", "Earbud & In-Ear Headphones");
            dict.Add("boost", "Amazon Launchpad");
            dict.Add("photo/173565", "SLR Camera Lenses");
            dict.Add("electronics/7073956011", "Portable Bluetooth Speakers");
            dict.Add("electronics/12097479011", "Over-Ear Headphones");


            return dict;
        }
    }
}
