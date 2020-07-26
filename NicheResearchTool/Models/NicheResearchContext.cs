using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AlibabaScraper.Models;
using AmazonScraper.Models;
using DbContext = System.Data.Entity.DbContext;

namespace NicheResearchTool.Models
{
    public class NicheResearchContext : DbContext
    {
        public NicheResearchContext() : base("NicheResearchContext")
        {
        }

        public NicheResearchContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AlibabaItem> AlibabaItems { get; set; }
        public DbSet<AmazonItem> AmazonItems { get; set; }
        public DbSet<ResearchProject> ResearchProjects { get; set; }
        public DbSet<SavedAlibabaItem> SavedAlibabaItems { get; set; }
        public DbSet<SavedAmazonItem> SavedAmazonItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BestSellerRank> BestSellerRanks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlibabaItem>()
                .Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            base.OnModelCreating(modelBuilder);
        }


    }
}