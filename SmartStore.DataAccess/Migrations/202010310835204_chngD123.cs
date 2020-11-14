namespace SmartStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chngD123 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LoginProvider);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        IsLocked = c.Boolean(),
                        Description = c.String(),
                        Avatar = c.Binary(),
                        CenterId = c.Int(),
                        GroupId = c.Int(),
                        LastLoginDate = c.DateTime(),
                        LastLoginDatePreview = c.DateTime(),
                        LastPasswordChangedDate = c.DateTime(),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthorizationItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        FarsiTitle = c.String(),
                        ItemType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthorizationItemsRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstItemId = c.Int(nullable: false),
                        SecondItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogImage = c.String(),
                        BlogTitle = c.String(maxLength: 50),
                        BlogSubTitle = c.String(maxLength: 50),
                        CreateDate = c.String(),
                        CreatorName = c.String(),
                        Description = c.String(),
                        Order = c.Int(nullable: false),
                        NotShow = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(maxLength: 30),
                        BrandImg = c.String(maxLength: 100),
                        BrandOrder = c.Int(nullable: false),
                        BrandNotShow = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductGroupId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        ProductName = c.String(),
                        ProductImg = c.String(),
                        ProductPrice = c.Int(nullable: false),
                        ProductDiscountPercent = c.Int(nullable: false),
                        ProductDescription = c.String(),
                        ProductShortDescription = c.String(),
                        ProductOrder = c.Int(nullable: false),
                        ProductNotShow = c.Boolean(nullable: false),
                        ProductCost = c.Int(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.ProductGroupId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Image = c.String(),
                        Order = c.Int(nullable: false),
                        NotShow = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupImage = c.String(),
                        GroupName = c.String(),
                        GroupOrder = c.Int(nullable: false),
                        GroupNotShow = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ProductGroup2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroup2_Id)
                .Index(t => t.ProductGroup2_Id);
            
            CreateTable(
                "dbo.ShopCartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Address = c.String(maxLength: 100),
                        Tel = c.String(maxLength: 11),
                        Email = c.String(maxLength: 50),
                        AboutUs = c.String(maxLength: 200),
                        ShortAboutUs = c.String(maxLength: 50),
                        NotShow = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FactorDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FactorId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        DetailCount = c.Int(nullable: false),
                        UnitPrice = c.Int(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Factors", t => t.FactorId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.FactorId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Factors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FactorNumber = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        PayDate = c.String(),
                        PayTime = c.String(),
                        PayNumber = c.String(),
                        Price = c.Int(nullable: false),
                        IsPay = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserCode = c.String(maxLength: 8),
                        UserFirstName = c.String(maxLength: 50),
                        UserLastName = c.String(maxLength: 50),
                        UserImage = c.String(),
                        UserFatherName = c.String(maxLength: 50),
                        UserDateofBirth = c.String(maxLength: 10),
                        UserNationalCode = c.String(maxLength: 10),
                        UserPostalCode = c.String(maxLength: 10),
                        UserPhoneNumber = c.String(maxLength: 11),
                        UserEmail = c.String(maxLength: 50),
                        UserPassword = c.String(),
                        UserVerificationCode = c.String(maxLength: 6),
                        UserIdentifierCode = c.String(maxLength: 8),
                        IsActive = c.Boolean(nullable: false),
                        UserAddress = c.String(maxLength: 200),
                        UserLandlinePhone = c.String(maxLength: 11),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 20),
                        RoleTitle = c.String(maxLength: 20),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductGroupId = c.Int(nullable: false),
                        FeatureTitle = c.String(maxLength: 20),
                        FeatureOrder = c.Int(nullable: false),
                        FeatueNotShow = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.ProductGroupId);
            
            CreateTable(
                "dbo.ProductFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureId = c.Int(nullable: false),
                        FeatureValue = c.String(),
                        ProductFeatureOrder = c.Int(nullable: false),
                        ProductFeatueNotShow = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .Index(t => t.FeatureId);
            
            CreateTable(
                "dbo.IdeaHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(),
                        NotShow = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AliasName = c.String(maxLength: 50),
                        DegreeOfEducation = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Job = c.String(maxLength: 50),
                        IdeaTitle = c.String(maxLength: 50),
                        AreaOfExpertise = c.String(maxLength: 50),
                        FeaturesOftheIdea = c.String(maxLength: 50),
                        Description = c.String(maxLength: 50),
                        BenefitsOftheIdea = c.String(maxLength: 50),
                        ThePurposeOftheIdea = c.String(maxLength: 50),
                        IdeaCosts = c.String(maxLength: 50),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 300),
                        LogType = c.Int(nullable: false),
                        Event = c.String(maxLength: 300),
                        IpAddress = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SliderHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SliderTitle = c.String(),
                        SliderSubTitle = c.String(),
                        SliderDescription = c.String(),
                        SliderImg = c.String(),
                        SliderUrl = c.String(),
                        SliderOrder = c.Int(nullable: false),
                        SliderNotShow = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SocialNetWorks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Icon = c.String(maxLength: 100),
                        Link = c.String(maxLength: 100),
                        Order = c.Int(nullable: false),
                        NotShow = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SystemParameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParameterName = c.String(),
                        Value = c.String(),
                        PersianTitle = c.String(),
                        Description = c.String(),
                        IsSystemic = c.Boolean(nullable: false),
                        ParentHistoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAuthorizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        AuthorizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Features", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductFeatures", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.FactorDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.FactorDetails", "FactorId", "dbo.Factors");
            DropForeignKey("dbo.Factors", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ShopCartItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "ProductGroup2_Id", "dbo.ProductGroups");
            DropForeignKey("dbo.Galleries", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropIndex("dbo.ProductFeatures", new[] { "FeatureId" });
            DropIndex("dbo.Features", new[] { "ProductGroupId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Factors", new[] { "UserId" });
            DropIndex("dbo.FactorDetails", new[] { "ProductId" });
            DropIndex("dbo.FactorDetails", new[] { "FactorId" });
            DropIndex("dbo.ShopCartItems", new[] { "ProductId" });
            DropIndex("dbo.ProductGroups", new[] { "ProductGroup2_Id" });
            DropIndex("dbo.Galleries", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            DropTable("dbo.UserAuthorizations");
            DropTable("dbo.SystemParameters");
            DropTable("dbo.SocialNetWorks");
            DropTable("dbo.SliderHeaders");
            DropTable("dbo.Logs");
            DropTable("dbo.Ideas");
            DropTable("dbo.IdeaHeaders");
            DropTable("dbo.ProductFeatures");
            DropTable("dbo.Features");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Factors");
            DropTable("dbo.FactorDetails");
            DropTable("dbo.ContactUs");
            DropTable("dbo.ShopCartItems");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.Galleries");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
            DropTable("dbo.Blogs");
            DropTable("dbo.AuthorizationItemsRelations");
            DropTable("dbo.AuthorizationItems");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetRoles");
        }
    }
}
