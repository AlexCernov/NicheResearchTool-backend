using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTrendsApiClient.Models
{


    public class ComparisonItem
    {
        public Geo geo { get; set; }
        public ComplexKeywordsRestriction complexKeywordsRestriction { get; set; }
    }


    public class InterestOverTimeRequest
    {
        public string time { get; set; }
        public string resolution { get; set; }
        public string locale { get; set; }
        public List<ComparisonItem> comparisonItem { get; set; }
        public RequestOptions requestOptions { get; set; }
    }
   
}
