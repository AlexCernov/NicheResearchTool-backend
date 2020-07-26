using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonScraper.Models
{
    public class AmazonPage
    {
        public int NoOfItems { get; set; }

        public decimal AveragePrice { get; set; }
        public decimal AverageRating { get; set; }
        public decimal AverageReviewsTopProducts{ get; set; }

        public decimal CrowdedMarket { get; set; }

        public decimal AverageEstimatedRevenue { get; set; }

        public List<AmazonItem> Items { get; set; }

        public AmazonPage()
        {
        }
    }
}