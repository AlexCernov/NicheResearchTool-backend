using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using GoogleTrendsApiClient;
using GoogleTrendsApiClient.Models;

namespace NicheResearchTool.Controllers
{
    public class GoogleApiController : ApiController
    {
        // GET: GoogleApi
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/GoogleApi/GetInterestOverTime")]

        public JsonResult<InterestOverTimeDefault> GetInterstOverTime(string keywords)
        {
            var client = new GoogleTrendsClient(keywords.Split(',').ToList());
            var result = client.GetInterestsOverTime();
            return Json(result);
        }

        // GET: GoogleApi
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/GoogleApi/GetRelatedTopics")]

        public JsonResult<RelatedTopicsDefault> GetRelatedTopics(string keywords)
        {
            var client = new GoogleTrendsClient(keywords.Split(',').ToList());
            var result = client.GetRelatedTopics();
            return Json(result);
        }

        // GET: GoogleApi
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/GoogleApi/GetRelatedQueries")]

        public JsonResult<RelatedQueriesDefault> GetRelatedQueries(string keywords)
        {
            var client = new GoogleTrendsClient(keywords.Split(',').ToList());
            var result = client.GetRelatedQueries();
            return Json(result);
        }
    }
}