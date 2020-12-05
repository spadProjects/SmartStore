namespace SmartStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedfeatures : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Features", "ProductGroupId", "dbo.ProductGroups");
            DropIndex("dbo.Features", new[] { "ProductGroupId" });
            DropColumn("dbo.Features", "ProductGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Features", "ProductGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.Features", "ProductGroupId");
            AddForeignKey("dbo.Features", "ProductGroupId", "dbo.ProductGroups", "Id", cascadeDelete: true);
        }
    }
}
