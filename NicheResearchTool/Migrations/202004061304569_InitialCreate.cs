namespace NicheResearchTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlibabaItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Seller = c.String(),
                        SellerLink = c.String(),
                        SellerTimeOnPlatform = c.Int(nullable: false),
                        ResponseRate = c.String(),
                        ResponseTime = c.String(),
                        Images = c.String(),
                        ReviewScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReviewCount = c.Int(nullable: false),
                        Link = c.String(),
                        MinimumOrder = c.Int(nullable: false),
                        Country = c.String(),
                        GoldStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AmazonItems",
                c => new
                    {
                        ASIN = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        Image = c.String(),
                        Name = c.String(),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfRatings = c.Int(nullable: false),
                        Link = c.String(),
                        Prime = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ASIN);
            
            CreateTable(
                "dbo.BestSellerRanks",
                c => new
                    {
                        CategoryId = c.String(nullable: false, maxLength: 128),
                        CategoryName = c.String(nullable: false, maxLength: 128),
                        Rank = c.Int(nullable: false),
                        NoOfSales = c.Int(nullable: false),
                        AmazonItem_ASIN = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.CategoryName, t.Rank })
                .ForeignKey("dbo.AmazonItems", t => t.AmazonItem_ASIN)
                .Index(t => t.AmazonItem_ASIN);
            
            CreateTable(
                "dbo.ResearchProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Details = c.String(),
                        Keywords = c.String(),
                        ProjectType = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Photo = c.String(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SavedAlibabaItems",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        AlibabaItemId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.AlibabaItemId })
                .ForeignKey("dbo.AlibabaItems", t => t.AlibabaItemId, cascadeDelete: true)
                .ForeignKey("dbo.ResearchProjects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.AlibabaItemId);
            
            CreateTable(
                "dbo.SavedAmazonItems",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        AmazonItemId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.AmazonItemId })
                .ForeignKey("dbo.AmazonItems", t => t.AmazonItemId, cascadeDelete: true)
                .ForeignKey("dbo.ResearchProjects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.AmazonItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavedAmazonItems", "ProjectId", "dbo.ResearchProjects");
            DropForeignKey("dbo.SavedAmazonItems", "AmazonItemId", "dbo.AmazonItems");
            DropForeignKey("dbo.SavedAlibabaItems", "ProjectId", "dbo.ResearchProjects");
            DropForeignKey("dbo.SavedAlibabaItems", "AlibabaItemId", "dbo.AlibabaItems");
            DropForeignKey("dbo.ResearchProjects", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.BestSellerRanks", "AmazonItem_ASIN", "dbo.AmazonItems");
            DropIndex("dbo.SavedAmazonItems", new[] { "AmazonItemId" });
            DropIndex("dbo.SavedAmazonItems", new[] { "ProjectId" });
            DropIndex("dbo.SavedAlibabaItems", new[] { "AlibabaItemId" });
            DropIndex("dbo.SavedAlibabaItems", new[] { "ProjectId" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.ResearchProjects", new[] { "User_Id" });
            DropIndex("dbo.BestSellerRanks", new[] { "AmazonItem_ASIN" });
            DropTable("dbo.SavedAmazonItems");
            DropTable("dbo.SavedAlibabaItems");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.ResearchProjects");
            DropTable("dbo.BestSellerRanks");
            DropTable("dbo.AmazonItems");
            DropTable("dbo.AlibabaItems");
        }
    }
}
