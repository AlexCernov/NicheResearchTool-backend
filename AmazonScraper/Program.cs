using AmazonScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using AmazonScraper.Utility;
using HtmlAgilityPack;
using NUglify;
using NUglify.Helpers;

namespace AmazonScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter keywords: ");
            string input = Console.ReadLine();
            var page = Scraper.GetPageForKeywords(input, CookieLocation.US);
            page.Items.ForEach(Console.WriteLine);

            //var apiclient = new FbaToolkitApiClient();
            //var response = apiclient.GetEstimatedSales();
            //Console.WriteLine(response);

            /*var something = Scraper.GetBestSellerRank(
                Scraper.GetBestSellerInfoLinks(Scraper.LoadDocument(input,
                    CookieLocation.US)));*/


        }

     

      

    }
}
