using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AlibabaScraper.Models;

namespace NicheResearchTool.Models
{
    public class SavedAlibabaItem
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Item")]
        public long AlibabaItemId { get; set; }

        public virtual ResearchProject Project { get; set; }
        public virtual AlibabaItem Item { get; set; }
    }
}