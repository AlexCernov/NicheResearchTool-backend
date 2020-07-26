using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.UI.WebControls;

namespace AmazonScraper.Models
{
    public class AmazonItem
    {
    
        public AmazonItem()
        {

        }

        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public decimal Price { get; set; }
        public int NumberOfRatings { get; set; }
        public string Link { get; set; }
        public bool Prime { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ASIN { get; set; }
        public List<BestSellerRank> BestSellerRank { get; set; }
        public decimal EstimatedRevenue { get; set; }


        public bool isTopProduct()
        {
            int minRank = 99999999;
            BestSellerRank.ForEach(rank =>
            {
                if (rank.Rank < minRank) minRank = rank.Rank;
            });

            return minRank < 1000;
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Rank:" + Id + '\n');
            stringBuilder.Append("Image:" + Image + '\n');
            stringBuilder.Append("Name:" + Name + '\n');
            stringBuilder.Append("Rating:" + Rating + '\n');
            stringBuilder.Append("NumberOfRatings:" + NumberOfRatings + '\n');
            stringBuilder.Append("Price:" + Price + '\n');
            stringBuilder.Append("ASIN:" + ASIN + '\n');
            stringBuilder.Append("Link:" + Link + '\n');
            BestSellerRank.ForEach(x =>
                stringBuilder.Append($"Rank: {x.Rank} Category: {x.CategoryName} CategoryId: {x.CategoryId}" + '\n'));

            return stringBuilder.ToString();
        }

    }
}