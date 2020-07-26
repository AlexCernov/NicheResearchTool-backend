using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoogleTrendsApiClient.Models
{

    public class TimelineData
    {
        public string time { get; set; }
        public string formattedTime { get; set; }
        public string formattedAxisTime { get; set; }
        public List<int> value { get; set; }
        public List<bool> hasData { get; set; }
        public List<string> formattedValue { get; set; }
        public bool? isPartial { get; set; }
    }

    public class InterestOverTimeDefault
    {
        public List<TimelineData> timelineData { get; set; }
        public List<int> averages { get; set; }
    }

    public class InterestOverTimeResponse
    {
        [JsonProperty("default")]
        public InterestOverTimeDefault @default { get; set; }
    }
    
}
