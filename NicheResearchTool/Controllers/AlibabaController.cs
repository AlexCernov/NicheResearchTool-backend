using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using AlibabaScraper.Models;
using AlibabaScraper.Utility;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Microsoft.Ajax.Utilities;
using NicheResearchTool.Models;

namespace NicheResearchTool.Controllers
{
    public class AlibabaController : ApiController
    {
        private NicheResearchContext db = new NicheResearchContext();

        // GET /api/Alibaba?keywords=
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Alibaba/Page")]

        public JsonResult<AlibabaPage> Get(string keywords)
        {
            return Json(Scraper.GetPageWithKeywords(keywords,52));
        }

        // GET /api/Alibaba?url=
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Alibaba/Page")]

        public JsonResult<AlibabaPage> GetFiltered(string url)
        {
            url = url.Replace(";", "&");
            return Json(Scraper.GetPageWithLink(url, 50));
        }

        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Alibaba/Item")]

        // GET: api/AlibabaItems
        public IQueryable<AlibabaItem> GetAlibabaItems()
        {
            return db.AlibabaItems;
        }
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        // GET: api/AlibabaItems/5
        [ResponseType(typeof(AlibabaItem))]
        [Route("api/Alibaba/Item")]

        public async Task<IHttpActionResult> GetAlibabaItem(long id)
        {
            AlibabaItem alibabaItem = await db.AlibabaItems.FindAsync(id);
            if (alibabaItem == null)
            {
                return NotFound();
            }

            return Ok(alibabaItem);
        }

        // PUT: api/AlibabaItems/5
        [ResponseType(typeof(void))]
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Alibaba/Item")]

        public async Task<IHttpActionResult> PutAlibabaItem(long id, AlibabaItem alibabaItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alibabaItem.Id)
            {
                return BadRequest();
            }

            db.Entry(alibabaItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlibabaItemExists(id))
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
        // POST: api/AlibabaItems
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [ResponseType(typeof(AlibabaItem))]
        [Route("api/Alibaba/Item")]

        public async Task<IHttpActionResult> PostAlibabaItem(AlibabaItem alibabaItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AlibabaItems.Add(alibabaItem);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = alibabaItem.Id }, alibabaItem);
        }

        // POST: api/AlibabaItems
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/Alibaba/ItemList")]
        public async Task<IHttpActionResult> PostAlibabaItem(List<AlibabaItem> alibabaItemList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            try
            {
                alibabaItemList = alibabaItemList.DistinctBy(x => x.Id).ToList();

                using (var context = new NicheResearchContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[AlibabaItems] ON");
                        alibabaItemList.ForEach(item =>
                        {
                            if (context.AlibabaItems.Any(x => x.Id == item.Id))
                            {
                                context.AlibabaItems.AddOrUpdate(item);
                            }
                            else
                            {
                                context.AlibabaItems.Add(item);

                            }
                        });
                        await context.SaveChangesAsync();
                        context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[AlibabaItems] OFF");
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


        // DELETE: api/AlibabaItems/5
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [ResponseType(typeof(AlibabaItem))]
        [Route("api/Alibaba/Item")]

        public async Task<IHttpActionResult> DeleteAlibabaItem(long id)
        {
            AlibabaItem alibabaItem = await db.AlibabaItems.FindAsync(id);
            if (alibabaItem == null)
            {
                return NotFound();
            }

            db.AlibabaItems.Remove(alibabaItem);
            await db.SaveChangesAsync();

            return Ok(alibabaItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlibabaItemExists(long id)
        {
            return db.AlibabaItems.Count(e => e.Id == id) > 0;
        }

    }
}
