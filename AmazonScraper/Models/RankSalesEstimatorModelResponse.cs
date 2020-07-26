using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScraper.Models
{
    public class Max
    {
        public string sales { get; set; }
        public int rank { get; set; }
    }

    public class Estimation
    {
        public string sales { get; set; }
        public int rank { get; set; }
    }

    public class Min
    {
        public string sales { get; set; }
        public int rank { get; set; }
    }

    public class Result1day
    {
        public Max max { get; set; }
        public Estimation estimation { get; set; }
        public Min min { get; set; }
    }

    public class Result30day
    {
        public Max max { get; set; }
        public Estimation estimation { get; set; }
        public Min min { get; set; }
    }

    public class EstimatedRank
    {
        public Result1day result_1day { get; set; }
        public Result30day result_30day { get; set; }
    }

    public class RankSalesEstimatorModelResponse
    {
        public IList<EstimatedRank> results { get; set; }
    }

}
