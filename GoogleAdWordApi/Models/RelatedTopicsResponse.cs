using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoogleTrendsApiClient.Models
{
    public class Topic
    {
        public string mid { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }

    public class RankedKeywordTopics
    {
        public Topic topic { get; set; }
        public int value { get; set; }
        public string formattedValue { get; set; }
        public bool hasData { get; set; }
        public string link { get; set; }
    }

    public class RankedListTopics
    {
        public IList<RankedKeywordTopics> rankedKeyword { get; set; }
    }

    public class RelatedTopicsDefault
    {
        public IList<RankedListTopics> rankedList { get; set; }
    }

   
    class RelatedTopicsResponse
    {
        [JsonProperty("default")]
        public RelatedTopicsDefault @default { get; set; }

}
}
