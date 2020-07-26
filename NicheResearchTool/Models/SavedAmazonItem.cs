using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmazonScraper.Models;

namespace NicheResearchTool.Models
{
    public class SavedAmazonItem
    {

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Item")]
        public string AmazonItemId { get; set; }

        public virtual ResearchProject Project { get; set; }
        public virtual AmazonItem Item { get; set; }
    }
}