using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NicheResearchTool.Models
{
    public class ResearchProject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Keywords { get; set; }
        public ProjectType ProjectType { get; set; }
        public virtual User User { get; set; }


    }



    public enum ProjectType
    {
        Amazon,
        Alibaba
    }
}