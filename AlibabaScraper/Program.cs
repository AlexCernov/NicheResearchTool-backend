using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using AlibabaScraper.Utility;

namespace AlibabaScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter keywords: ");
            string input = Console.ReadLine();
            var page = Scraper.GetPageWithLink(input, 100);
            Console.WriteLine();
            /*var timer = new Stopwatch();
            timer.Start();
            var scraper = new Scraper(input);
            timer.Stop();
            Console.WriteLine(timer.Elapsed);
            Console.WriteLine(scraper.Page);*/
            Console.ReadKey();
        }

    }
}
