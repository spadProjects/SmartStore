namespace SmartStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedproductgroupparentrelation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductGroups", "ParentId");
            RenameColumn(table: "dbo.ProductGroups", name: "ProductGroup2_Id", newName: "ParentId");
            RenameIndex(table: "dbo.ProductGroups", name: "IX_ProductGroup2_Id", newName: "IX_ParentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductGroups", name: "IX_ParentId", newName: "IX_ProductGroup2_Id");
            RenameColumn(table: "dbo.ProductGroups", name: "ParentId", newName: "ProductGroup2_Id");
            AddColumn("dbo.ProductGroups", "ParentId", c => c.Int());
        }
    }
}
