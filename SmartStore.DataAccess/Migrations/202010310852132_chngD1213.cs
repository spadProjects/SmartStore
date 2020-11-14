namespace SmartStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chngD1213 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "ParentHistoryId");
            DropColumn("dbo.Users", "InsertUser");
            DropColumn("dbo.Users", "InsertDate");
            DropColumn("dbo.Users", "UpdateUser");
            DropColumn("dbo.Users", "UpdateDate");
            DropColumn("dbo.Users", "IsArchived");
            DropColumn("dbo.Users", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsArchived", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Users", "UpdateUser", c => c.String());
            AddColumn("dbo.Users", "InsertDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "InsertUser", c => c.String());
            AddColumn("dbo.Users", "ParentHistoryId", c => c.Int());
        }
    }
}
