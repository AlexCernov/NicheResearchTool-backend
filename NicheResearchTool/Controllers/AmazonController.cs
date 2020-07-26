using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AmazonScraper.Models;
using NicheResearchTool.Models;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using AmazonScraper.Utility;

namespace NicheResearchTool.Controllers
{
    public class AmazonController : ApiController
    {
        private NicheResearchContext db = new NicheResearchContext();


        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Amazon/Page")]

        public JsonResult<AmazonPage> Get(string keywords)
        {
            return Json(Scraper.GetPageForKeywords(keywords, CookieLocation.US));
        }

        // GET: api/AmazonItems
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Amazon/Item/All")]

        public IQueryable<AmazonItem> GetAmazonItems()
        {
            return db.AmazonItems;
        }

        // GET: api/AmazonItems/5
        [ResponseType(typeof(AmazonItem))]
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Amazon/Item")]

        public async Task<IHttpActionResult> GetAmazonItem(string id)
        {
            AmazonItem amazonItem = await db.AmazonItems.FindAsync(id);
            if (amazonItem == null)
            {
                return NotFound();
            }

            return Ok(amazonItem);
        }

        // PUT: api/AmazonItems/5
        [ResponseType(typeof(void))]
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Amazon/Item")]

        public async Task<IHttpActionResult> PutAmazonItem(string id, AmazonItem amazonItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != amazonItem.ASIN)
            {
                return BadRequest();
            }

            db.Entry(amazonItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmazonItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AmazonItems
        [ResponseType(typeof(AmazonItem))]
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Amazon/Item")]

        public async Task<IHttpActionResult> PostAmazonItem(AmazonItem amazonItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AmazonItems.Add(amazonItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AmazonItemExists(amazonItem.ASIN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = amazonItem.ASIN }, amazonItem);
        }


        // POST: api/AmazonItems
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Amazon/ItemList")]

        public async Task<IHttpActionResult> PostAmazonItem(List<AmazonItem> amazonItemList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                using (var context = new NicheResearchContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        //context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[AmazonItems] ON");
                        //context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[[BestSellerRanks]] ON");
                        amazonItemList.ForEach(item =>
                        {
                            if (context.AmazonItems.Any(x => x.ASIN == item.ASIN))
                                return;
                            else
                            {
                                context.AmazonItems.Add(item);

                            }
                        });
                        await context.SaveChangesAsync();
                        //context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[[BestSellerRanks]] OFF");
                        //context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[AmazonItems] OFF");

                        transaction.Commit();
                    }
                }
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.Created);
        }

        // DELETE: api/AmazonItems/5
        [ResponseType(typeof(AmazonItem))]
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Amazon/Item")]

        public async Task<IHttpActionResult> DeleteAmazonItem(string id)
        {
            AmazonItem amazonItem = await db.AmazonItems.FindAsync(id);
            if (amazonItem == null)
            {
                return NotFound();
            }

            db.AmazonItems.Remove(amazonItem);
            await db.SaveChangesAsync();

            return Ok(amazonItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AmazonItemExists(string id)
        {
            return db.AmazonItems.Count(e => e.ASIN == id) > 0;
        }
    }
}