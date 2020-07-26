using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlibabaScraper.Models;
using AmazonScraper.Models;

namespace NicheResearchTool.Models
{
    public class PostRequestProjectAlibabaModel
    {
        public ResearchProject ResearchProject { get; set; }
        public List<AlibabaItem> AlibabaItems { get; set; }
    }

    public class PostRequestProjectAmazonModel
    {
        public ResearchProject ResearchProject { get; set; }
        public List<AmazonItem> AmazonItems { get; set; }
    }
}