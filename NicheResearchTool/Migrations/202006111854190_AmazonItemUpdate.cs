namespace NicheResearchTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmazonItemUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AmazonItems", "EstimatedRevenue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BestSellerRanks", "NoOfSales", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BestSellerRanks", "NoOfSales", c => c.Int(nullable: false));
            DropColumn("dbo.AmazonItems", "EstimatedRevenue");
        }
    }
}
