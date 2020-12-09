namespace SmartStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduserpoint2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPoints", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserPoints", "UserId");
            AddForeignKey("dbo.UserPoints", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPoints", "UserId", "dbo.Users");
            DropIndex("dbo.UserPoints", new[] { "UserId" });
            DropColumn("dbo.UserPoints", "UserId");
        }
    }
}
