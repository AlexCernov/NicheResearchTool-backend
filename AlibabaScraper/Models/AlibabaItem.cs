using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlibabaScraper.Models
{
    public class AlibabaItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Seller { get; set; }
        public string SellerLink { get; set; }
        public int SellerTimeOnPlatform { get; set; }
        public string ResponseRate { get; set; }
        public string ResponseTime { get; set; }
        public string Images { get; set; }
        public decimal ReviewScore { get; set; }
        public int ReviewCount { get; set; }
        public string Link { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int MinimumOrder { get; set; }
        public string Country { get; set; }
        public bool GoldStatus { get; set; }

        public override string ToString()
        {
            return "Id:" + Id + '\n'
                   + "Title:" + Name + '\n'
                   + "Price:" + MinPrice + "-" + MaxPrice + '\n'
                   + "Minimum order:" + MinimumOrder + '\n'
                   + "Review:" + ReviewScore + '\n'
                   + "Number of reviews:" + ReviewCount + '\n'
                   + "Seller's name:" + Seller + '\n'
                   + "Seller's time on platform:" + SellerTimeOnPlatform + '\n'
                   + "Selling from:" + Country + '\n'
                   + "Seller's page:" + SellerLink + '\n'
                   + "Product page:" + Link + '\n'
                   + "Is gold:" + GoldStatus + '\n'
                ;
        }
    }
}
