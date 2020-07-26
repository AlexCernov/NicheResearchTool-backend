using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoogleTrendsApiClient.Models
{
    public class RankedKeywordQueries
    {
        public string query { get; set; }
        public int value { get; set; }
        public string formattedValue { get; set; }
        public bool hasData { get; set; }
        public string link { get; set; }

    }
    public class RankedListQueries
    {
        public IList<RankedKeywordQueries> rankedKeyword { get; set; }

    }
    public class RelatedQueriesDefault
    {

        public IList<RankedListQueries> rankedList { get; set; }

    }

    public class RelatedQueriesResponse
    {
        public RelatedQueriesDefault @default { get; set; }

    }
}
