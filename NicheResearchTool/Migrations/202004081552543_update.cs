namespace NicheResearchTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropPrimaryKey("dbo.Roles");
            AlterColumn("dbo.Roles", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Roles", "Id");
            AddForeignKey("dbo.Users", "Role_Id", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropPrimaryKey("dbo.Roles");
            AlterColumn("dbo.Roles", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Roles", "Id");
            AddForeignKey("dbo.Users", "Role_Id", "dbo.Roles", "Id");
        }
    }
}
