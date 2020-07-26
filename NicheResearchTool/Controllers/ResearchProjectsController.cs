using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NicheResearchTool.Models;
using System.Web.Http.Cors;
using AlibabaScraper.Models;
using AmazonScraper.Models;
using Microsoft.Ajax.Utilities;

namespace NicheResearchTool.Controllers
{
    public class ResearchProjectsController : ApiController
    {
        private NicheResearchContext db = new NicheResearchContext();


        // GET: api/ResearchProjects/Alibaba/User
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/ResearchProjects/Alibaba")]
        public List<ResearchProject> GetAlibabaResearchProjects(string userId)
        {

            var projects = db.ResearchProjects.Where(x => x.User.Id == userId && x.ProjectType == ProjectType.Alibaba).ToList();

            return projects;
        }

        // GET: api/ResearchProjects/Amazon/User
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/ResearchProjects/Amazon")]
        public List<ResearchProject> GetAmazonResearchProjects(string userId)
        {

            var projects = db.ResearchProjects.Where(x => x.User.Id == userId && x.ProjectType == ProjectType.Amazon).ToList();

            return projects;
        }

        // GET: api/ResearchProjects/All
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/ResearchProjects/All")]
        public List<ResearchProject> GetAllResearchProjects(string userId)
        {

            var projects = db.ResearchProjects.Where(x => x.User.Id == userId).ToList();

            return projects;
        }


        // GET: api/ResearchProjects/5
        [ResponseType(typeof(ResearchProject))]
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<IHttpActionResult> GetResearchProject(int id)
        {
            ResearchProject researchProject = await db.ResearchProjects.FindAsync(id);
            if (researchProject == null)
            {
                return NotFound();
            }

            return Ok(researchProject);
        }

        // GET: api/ResearchProjects/GetAlibabaItems/5
        [ResponseType(typeof(List<AlibabaItem>))]
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/ResearchProjects/GetAlibabaItems")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAlibabaItemsFromProject(int projectId)
        {
            var alibabaItems = await db.SavedAlibabaItems.Where(x => x.ProjectId == projectId).Select(x => x.Item).ToListAsync();
          
            return Ok(alibabaItems);
        }

        // GET: api/ResearchProjects/GetAmazonItems/5
        [ResponseType(typeof(List<AmazonItem>))]
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/ResearchProjects/GetAmazonItems")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAmazonItemsFromProject(int projectId)
        {
            var amazonItems = await db.SavedAmazonItems.Where(x => x.ProjectId == projectId).Select(x => x.Item).ToListAsync();
           
            
            amazonItems.ForEach(item =>
                {
                    item.BestSellerRank = db.BestSellerRanks
                        .SqlQuery(
                            "SELECT [CategoryId],[CategoryName],[Rank],[NoOfSales] FROM [dbo].[BestSellerRanks] AS A WHERE A.AmazonItem_ASIN = '" + item.ASIN +"'")
                        .ToList();
                });
            return Ok(amazonItems);
        }

        // PUT: api/ResearchProjects/5
        [ResponseType(typeof(void))]
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> PutResearchProject(int id, ResearchProject researchProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != researchProject.Id)
            {
                return BadRequest();
            }

            db.Entry(researchProject).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResearchProjectExists(id))
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

        // POST: api/ResearchProjects
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [ResponseType(typeof(ResearchProject))]
        public async Task<IHttpActionResult> PostResearchProject(ResearchProject researchProject)
        {
            if (!ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            
            db.Users.Attach(researchProject.User);
            db.Roles.Attach(researchProject.User.Role);

            db.ResearchProjects.Add(researchProject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ResearchProjectExists(researchProject.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = researchProject.Id }, researchProject);
        }

        // POST: api/ResearchProjects
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [ResponseType(typeof(ResearchProject))]
        [Route("api/ResearchProjects/Alibaba/AddItems")]

        public async Task<IHttpActionResult> AddAlibabaItemsToProject(PostRequestProjectAlibabaModel request)
        {
            if (!ModelState.IsValid || request?.ResearchProject == null || request.AlibabaItems == null)
            {
                return BadRequest(ModelState);
            }


            try
            {
                db.Users.Attach(request.ResearchProject.User);
                db.Roles.Attach(request.ResearchProject.User.Role);

                request.AlibabaItems = request.AlibabaItems.DistinctBy(x => x.Id).ToList();

                var dbProject = db.ResearchProjects.FirstOrDefault(p => p.Id == request.ResearchProject.Id);
                foreach (var item in request.AlibabaItems)
                {
                    var dbItem = db.AlibabaItems.FirstOrDefault(x => x.Id == item.Id);
                    var entity = new SavedAlibabaItem() { Item = dbItem, Project = dbProject };

                    if (db.SavedAlibabaItems.Any(x => x.Item.Id == entity.Item.Id && x.Project.Id == entity.Project.Id)) continue;
                    db.SavedAlibabaItems.Add(entity);
                }

                await db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return StatusCode(HttpStatusCode.Created); ;
        }


        // POST: api/ResearchProjects
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [ResponseType(typeof(ResearchProject))]
        [Route("api/ResearchProjects/Amazon/AddItems")]

        public async Task<IHttpActionResult> AddAmazonItemsToProject(PostRequestProjectAmazonModel request)
        {
            if (!ModelState.IsValid || request?.ResearchProject == null || request.AmazonItems == null)
            {
                return BadRequest(ModelState);
            }


            try
            {

                db.Users.Attach(request.ResearchProject.User);
                db.Roles.Attach(request.ResearchProject.User.Role);

                request.AmazonItems = request.AmazonItems.DistinctBy(x => x.ASIN).ToList();

                var dbProject = db.ResearchProjects.FirstOrDefault(p => p.Id == request.ResearchProject.Id);
                foreach (var item in request.AmazonItems)
                {
                    var dbItem = db.AmazonItems.FirstOrDefault(x => x.ASIN == item.ASIN);
                    var entity = new SavedAmazonItem() { Item = dbItem, Project = dbProject };

                    if (db.SavedAmazonItems.Any(x => x.Item.ASIN == entity.Item.ASIN && x.Project.Id == entity.Project.Id)) continue;
                    db.SavedAmazonItems.Add(entity);
                }
                await db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return StatusCode(HttpStatusCode.Created); ;
        }

        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [ResponseType(typeof(ResearchProject))]
        [Route("api/ResearchProjects/Amazon/DeleteItems")]
        public async Task<IHttpActionResult> DeleteAmazonItemsFromProject(int id, List<AmazonItem> items)
        {
            var savedItems = db.SavedAmazonItems.Where(savedItem => savedItem.Project.Id == id).ToList();
            savedItems.ForEach(item =>
            {
                if (items.FirstOrDefault(x => x.ASIN == item.AmazonItemId) != null)
                    db.SavedAmazonItems.Remove(item);
            });
            await db.SaveChangesAsync();

            return Ok();
        }

        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [Route("api/ResearchProjects/Alibaba/DeleteItems")]
        public async Task<IHttpActionResult> DeleteAlibabaItemsFromProject(int id, List<AlibabaItem> items)
        {
            var savedItems = db.SavedAlibabaItems.Where(savedItem => savedItem.Project.Id == id);
            savedItems.ForEach(item =>
            {
                if (items.FirstOrDefault(x => x.Id == item.AlibabaItemId) != null)
                    db.SavedAlibabaItems.Remove(item);
            });
            await db.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/ResearchProjects/5
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        [ResponseType(typeof(ResearchProject))]
        public async Task<IHttpActionResult> DeleteResearchProject(int id)
        {
            ResearchProject researchProject = await db.ResearchProjects.FindAsync(id);
            if (researchProject == null)
            {
                return NotFound();
            }

            db.ResearchProjects.Remove(researchProject);
            await db.SaveChangesAsync();

            return Ok(researchProject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResearchProjectExists(int id)
        {
            return db.ResearchProjects.Count(e => e.Id == id) > 0;
        }
    }
}