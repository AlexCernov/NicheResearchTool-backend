using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScraper.Models
{
    class RankSalesEstimatorModelRequest
    {
        public string marketplace { get; set; }
        public string category { get; set; }
        public List<int> rank { get; set; }

        public RankSalesEstimatorModelRequest()
        {
            
        }
    }
}
