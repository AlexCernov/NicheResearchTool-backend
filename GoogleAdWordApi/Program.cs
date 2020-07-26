using System;
using System.Collections.Generic;

namespace GoogleTrendsApiClient
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            var client = new GoogleTrendsClient(new List<string>() { "leather bracelet" });

           var response = client.GetRelatedQueries();
            Console.WriteLine("done");

            Console.ReadKey();

        }

       
    }
}