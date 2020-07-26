using System.Collections.Generic;
using System.Text;

namespace AlibabaScraper.Models
{
    public class AlibabaPage
    {
        public string Link { get; set; }
        public decimal AverageMinPrice { get; set; }
        public decimal AverageMaxPrice { get; set; }
        public decimal AverageResponseRate { get; set; }

        public List<AlibabaItem> Items { get; set; }
        public SnData FilterData { get; set; }
        public override string ToString()
        {
            StringBuilder returnString = new StringBuilder();
            returnString.Append("Alibaba page").Append('\n');
            returnString.Append("Page link:").Append(Link).Append('\n');
            returnString.Append("Average price:").Append(AverageMinPrice + "-" + AverageMaxPrice).Append('\n');
            Items.ForEach(x => {
                    returnString.Append("----------------------------------------------------------------------------------------------------------------------").Append('\n');
                    returnString.Append(x.ToString());
                }
            );
            return returnString.ToString();
        }
    }
}
