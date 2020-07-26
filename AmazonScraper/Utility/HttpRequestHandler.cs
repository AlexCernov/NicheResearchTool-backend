using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using AmazonScraper.Models;

namespace AmazonScraper.Utility

{
    public class HttpRequestHandler : WebClient
    {
      
        private readonly CookieContainer cookies;
        private readonly HttpClient httpClient;

        private void SetCookiesToUS()
        {
            using var r = new StreamReader(@"USCookie.json");
            var json = r.ReadToEnd();
            var cookiesFromJson = JsonConvert.DeserializeObject<List<Cookie>>(json);
            cookiesFromJson.ForEach(x => cookies.Add(x));
        }

        public HttpRequestHandler(CookieLocation location)
        {
            cookies = new CookieContainer();
            if (location == CookieLocation.US) SetCookiesToUS();
            var handler = new HttpClientHandler() { CookieContainer = cookies, AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClient = new HttpClient(handler);
        }

     

        public string DownloadPageAsync(string url)
        {
            var uri = new Uri(url);
            var result = httpClient.GetAsync(uri).Result;
            result.EnsureSuccessStatusCode();
            using var responseStream = result.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            //using var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
            using var streamReader = new StreamReader(responseStream);
            return streamReader.ReadToEndAsync().Result;
        }
      
    }
}