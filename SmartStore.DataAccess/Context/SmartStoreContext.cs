using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;
using SmartStore.Model.Entities.Base.Log;
using SmartStore.Model.Entities.Base.Authorization;

namespace SmartStore.DataAccess.Context
{
    public class SmartStoreContext : DbContext
    {
        static SmartStoreContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SmartStoreContext, Migrations.Configuration>());            
        }


        #region Instance
        private static SmartStoreContext _instance;

        public static SmartStoreContext GetInstance()
        {
            return new SmartStoreContext(); 
        }
        #endregion

        #region AspNetUser
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        #endregion

        public DbSet<Log> Logs { get; set; }
        public DbSet<UserAuthorization> UserAuthorizations { get; set; }

        public DbSet<AuthorizationItem> AuthorizationItems { get; set; }
        public DbSet<AuthorizationItemsRelation> AuthorizationItemsRelations { get; set; }

        public DbSet<SystemParameter> SystemParameters { get; set; }


        public DbSet<Product> Products { get; set; }
        public DbSet<SliderHeader> SliderHeaders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<ShopCartItem> ShopCartItems { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorDetail> FactorDetails { get; set; }
        public DbSet<SocialNetWork> SocialNetWorks { get; set; }
        public DbSet<ContactUs> ContactUss { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<IdeaHeader> IdeaHeaders { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<ProductFeatureValue> ProductFeatureValues { get; set; }
        public DbSet<ProductGroupBrand> ProductGroupBrands { get; set; }
        public DbSet<ProductGroupFeature> ProductGroupFeatures { get; set; }
        public DbSet<ProductMainFeature> ProductMainFeatures { get; set; }
        public DbSet<SubFeature> SubFeatures { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<UserPoint> UserPoints { get; set; }
        public DbSet<UserProfitPayment> UserProfitPayments { get; set; }

    }
}
