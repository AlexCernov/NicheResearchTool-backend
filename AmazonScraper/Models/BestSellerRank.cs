using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonScraper.Models
{
    public class BestSellerRank
    {
        [Key]
        [Column(Order = 1)]
        public string CategoryId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string CategoryName { get; set; }
        [Key]
        [Column(Order = 3)]
        public int Rank { get; set; }
        public decimal NoOfSales { get; set; }
    }
}
