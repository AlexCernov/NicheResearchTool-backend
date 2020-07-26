namespace NicheResearchTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisableIdGeneration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SavedAlibabaItems", "AlibabaItemId", "dbo.AlibabaItems");
            DropPrimaryKey("dbo.AlibabaItems");
            AlterColumn("dbo.AlibabaItems", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.AlibabaItems", "Id");
            AddForeignKey("dbo.SavedAlibabaItems", "AlibabaItemId", "dbo.AlibabaItems", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavedAlibabaItems", "AlibabaItemId", "dbo.AlibabaItems");
            DropPrimaryKey("dbo.AlibabaItems");
            AlterColumn("dbo.AlibabaItems", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.AlibabaItems", "Id");
            AddForeignKey("dbo.SavedAlibabaItems", "AlibabaItemId", "dbo.AlibabaItems", "Id", cascadeDelete: true);
        }
    }
}
