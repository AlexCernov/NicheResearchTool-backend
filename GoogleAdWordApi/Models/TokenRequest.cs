using System.Collections.Generic;
using Newtonsoft.Json;

namespace GoogleTrendsApiClient.Models
{
    public class ComparisonItemToken
    {
        public string keyword { get; set; }
        public string geo { get; set; }
        public string time { get; set; }
    }

    public class TokenRequest
    {
        public List<ComparisonItemToken> comparisonItem { get; set; }
        public int category { get; set; }
        public string property { get; set; }
    }
   
}
