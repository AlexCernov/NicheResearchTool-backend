namespace NicheResearchTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAlibabaItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlibabaItems", "MinPrice", c => c.Decimal(nullable: false, precision: 2, scale: 2));
            AddColumn("dbo.AlibabaItems", "MaxPrice", c => c.Decimal(nullable: false, precision: 2, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AlibabaItems", "MaxPrice");
            DropColumn("dbo.AlibabaItems", "MinPrice");
        }
    }
}
