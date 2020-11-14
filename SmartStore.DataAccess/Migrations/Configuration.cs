namespace SmartStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using SmartStore.DataAccess.Context;
    using SmartStore.Model.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<SmartStore.DataAccess.Context.SmartStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "SmartStore.DataAccess.Context.SmartStoreContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SmartStore.DataAccess.Context.SmartStoreContext context)
        {
            //SmartStoreContext db = new SmartStoreContext();

            //string myHash = FormsAuthentication.HashPasswordForStoringInConfigFile("0000", "MD5");

            //Random random = new Random();
            //int MyUserCode = random.Next(12300000, 99000000);
            //int MyVerificationCode = 0;
            //bool isValidCod = false;
            //while (!isValidCod)
            //{
            //    Random random1 = new Random();

            //    MyVerificationCode = random1.Next(542000, 990000);

            //    var userverificationcode = db.Users.FirstOrDefault(j => j.UserVerificationCode == MyVerificationCode.ToString());
            //    if (userverificationcode == null)
            //    {
            //        isValidCod = true;
            //    }
            //}

            //if (db.Roles.Count() == 0 && db.Users.Count() == 0)
            //{
            //    Role role = new Role()
            //    {
            //        RoleName = "Admin",
            //        RoleTitle = "admin"
            //    };
            //    db.Roles.Add(role);
            //    db.SaveChanges();

            //    User user = new User()
            //    {
            //        IsActive = true,
            //        UserCode = MyUserCode.ToString(),
            //        RoleId = role.Id,
            //        UserEmail="amin_ni77@yahoo.com",
            //        UserPhoneNumber = "09120000000",
            //        UserPassword = myHash,
            //        UserImage = "436450-avatar.jpg",
            //        UserFirstName = "Amin",
            //        UserLastName = "Noruzi",
            //        UserFatherName = "Hossein",
            //        UserLandlinePhone = "02632238994",
            //        UserAddress = "Karaj",
            //        UserNationalCode = "0311754333",
            //        UserPostalCode = "3134647468",
            //        UserVerificationCode = MyVerificationCode.ToString()
            //    };
            //    db.Users.Add(user);
            //    db.SaveChanges();

            //    Role role2 = new Role()
            //    {
            //        RoleName = "User",
            //        RoleTitle = "user"
            //    };

            //    db.Roles.Add(role2);
            //    db.SaveChanges();

            //    Random random2 = new Random();
            //    int MyUserCode2 = random2.Next(10000000, 99000000);
            //    int MyVerificationCode2 = 0;
            //    bool isValidCod2 = false;
            //    while (!isValidCod2)
            //    {
            //        Random random3 = new Random();

            //        MyVerificationCode2 = random3.Next(100000, 990000);

            //        var userverificationcode2 = db.Users.FirstOrDefault(j => j.UserVerificationCode == MyVerificationCode2.ToString());
            //        if (userverificationcode2 == null)
            //        {
            //            isValidCod2 = true;
            //        }
            //    }


            //    User user1 = new User()
            //    {
            //        IsActive = true,
            //        UserCode = MyUserCode2.ToString(),
            //        RoleId = role2.Id,
            //        UserEmail = "aminni1376@gmail.com",
            //        UserPhoneNumber = "09370000000",
            //        UserPassword = myHash,
            //        UserImage = "320550-avatar-1.jpg",
            //        UserFirstName = "Mohammad",
            //        UserLastName = "Karimi",
            //        UserFatherName = "Abas",
            //        UserLandlinePhone = "02636548985",
            //        UserAddress = "Karaj",
            //        UserNationalCode = "0326548569",
            //        UserPostalCode = "6963356236",
            //        UserVerificationCode = MyVerificationCode2.ToString()

            //    };
            //    db.Users.Add(user1);
            //    db.SaveChanges();
        }
    }
}



