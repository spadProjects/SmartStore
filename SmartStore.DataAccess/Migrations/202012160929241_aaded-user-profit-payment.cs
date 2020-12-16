﻿namespace SmartStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aadeduserprofitpayment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfitPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Amount = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfitPayments", "UserId", "dbo.Users");
            DropIndex("dbo.UserProfitPayments", new[] { "UserId" });
            DropTable("dbo.UserProfitPayments");
        }
    }
}
