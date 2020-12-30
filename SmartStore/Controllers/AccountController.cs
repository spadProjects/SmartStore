using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;
using SmartStore.Models;
using System.Web.Security;
using SmartStore.Classes;


namespace SmartStore.Controllers
{
    public class AccountController : Controller
    {
        SmartStoreContext db = new SmartStoreContext();

        public ActionResult VerificationCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerificationCode(VerificationCodeViewModel verificationCode)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.UserVerificationCode == verificationCode.UserVerificationCode);
                if (user != null)
                {
                    Random random = new Random();
                    int MyUserCode = random.Next(10000000, 99000000);
                    int MyVerificationCode = 0;
                    bool isValidCod = false;
                    while (!isValidCod)
                    {
                        Random random1 = new Random();

                        MyVerificationCode = random1.Next(100000, 990000);

                        var userverificationcode = db.Users.FirstOrDefault(j => j.UserVerificationCode == MyVerificationCode.ToString());
                        if (userverificationcode == null)
                        {
                            isValidCod = true;
                        }
                    }

                    user.IsActive = true;
                    user.UserVerificationCode = MyVerificationCode.ToString();
                    db.SaveChanges();

                    if (user.Role.RoleName == "Admin")
                    {
                        return Redirect("/Admin/Dashboard/Index");
                    }

                    return Redirect("/Customer/Index");

                }
                else
                {
                    ModelState.AddModelError("UserCode", "Your activation code is invalid");
                }

            }

            return View(verificationCode);
        }

        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                string MyHash = FormsAuthentication.HashPasswordForStoringInConfigFile(login.UserPassword, "MD5");
                var user = db.Users.FirstOrDefault(u => u.UserEmail == login.UserEmail && u.UserPassword == MyHash);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(login.UserEmail, true);
                    if (user.IsActive)
                    {
                        if (user.Role.RoleName == "User")
                        {
                            return Redirect("/Customer/Index");
                        }
                        else
                        {
                            ModelState.AddModelError("UserEmail", "There is no user with the entered details");
                        }
                    }
                    else
                    {
                        return RedirectToAction("RegisterWithEmail");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserEmail", "There is no user with the entered details");
                }
            }

            return PartialView();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("AdminLogin");
        }

        public ActionResult RegisterWithEmail(RegisterWithEmailViewModel registerwithemail)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(u => u.UserEmail == registerwithemail.UserEmail))
                {
                    string MyHash = FormsAuthentication.HashPasswordForStoringInConfigFile(registerwithemail.UserPassword, "MD5");

                    Random random = new Random();
                    int MyUserCode = random.Next(10000000, 99000000);
                    int MyVerificationCode = 0;
                    bool isValidCod = false;
                    while (!isValidCod)
                    {
                        Random random1 = new Random();

                        MyVerificationCode = random1.Next(100000, 990000);

                        var userverificationcode = db.Users.FirstOrDefault(j => j.UserVerificationCode == MyVerificationCode.ToString());
                        if (userverificationcode == null)
                        {
                            isValidCod = true;
                        }
                    }

                    User user = new User()
                    {
                        RoleId =2,
                        UserCode = MyUserCode.ToString(),
                        UserEmail = registerwithemail.UserEmail,
                        UserPassword = MyHash,
                        UserVerificationCode = MyVerificationCode.ToString(),
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    SendEmail.Send(registerwithemail.UserEmail, "VerificationCode", "VerificationCode :" + " " + MyVerificationCode.ToString());



                    return RedirectToAction("VerificationCode");

                }
                else
                {
                    ModelState.AddModelError("UserEmail", "You are already Registered");
                }
            }
            return View(registerwithemail);
        }

        public ActionResult RegisterWithPhoneNumber(RegisterWithPhoneNumberViewModel registerwithphonenumber)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(u => u.UserPhoneNumber == registerwithphonenumber.UserPhoneNumber))
                {
                    string MyHash = FormsAuthentication.HashPasswordForStoringInConfigFile(registerwithphonenumber.UserPassword, "MD5");

                    Random random = new Random();

                    int MyCode = random.Next(100000, 990000);

                    User user = new User()
                    {
                        RoleId = db.Roles.Max(r => r.Id),
                        UserPhoneNumber = registerwithphonenumber.UserPhoneNumber,
                        UserPassword = MyHash,
                        UserCode = MyCode.ToString(),
                    };

                    SmsService sms = new SmsService();
                    sms.Send(registerwithphonenumber.UserPhoneNumber, MyCode.ToString());


                    db.Users.Add(user);
                    db.SaveChanges();



                    return RedirectToAction("VerificationCode");

                }
                else
                {
                    ModelState.AddModelError("UserPhoneNumber", "You are already Registered");
                }
            }
            return PartialView(registerwithphonenumber);
        }


        public ActionResult CheckPhoneNumber()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckPhoneNumber(CheckPhoneNumberViewModel check)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.UserPhoneNumber == check.UserPhoneNumber);
                if (user != null)
                {
                    SmsService sms = new SmsService();
                    sms.Send(check.UserPhoneNumber, user.UserCode);
                    return RedirectToAction("ForgetPassword");
                }
                else
                {
                    ModelState.AddModelError("UserPhoneNumber", "You are not registered yet");
                }
            }
            return View(check);
        }

        public ActionResult CheckEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckEmail(CheckEmailViewModel check)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.UserEmail == check.UserEmail);
                if (user != null)
                {
                    SendEmail.Send(check.UserEmail, "VerificationCode", "VerificationCode :" + " " + user.UserVerificationCode);
                    return RedirectToAction("ForgetPassword");
                }
                else
                {
                    ModelState.AddModelError("UserEmail", "You are not registered yet");
                }
            }
            return View(check);
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordViewModel forget)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.UserVerificationCode == forget.VerificationCode);
                if (user != null)
                {
                    Random random = new Random();
                    int MyUserCode = random.Next(10000000, 99000000);
                    int MyVerificationCode = 0;
                    bool isValidCod = false;
                    while (!isValidCod)
                    {
                        Random random1 = new Random();

                        MyVerificationCode = random1.Next(100000, 990000);

                        var userverificationcode = db.Users.FirstOrDefault(j => j.UserVerificationCode == MyVerificationCode.ToString());
                        if (userverificationcode == null)
                        {
                            isValidCod = true;
                        }
                    }

                    user.UserPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(forget.UserPassword, "MD5");
                    user.UserCode = MyUserCode.ToString();
                    user.UserVerificationCode = MyVerificationCode.ToString();
                    db.SaveChanges();

                    return RedirectToAction("RegisterWithEmail");
                }
                else
                {
                    ModelState.AddModelError("UserVerificationCode", "Your verification code is invalid");
                }
            }
            return View(forget);

        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminLoginViewModel adminLogin)
        {
            if (ModelState.IsValid)
            {
                string MyHash = FormsAuthentication.HashPasswordForStoringInConfigFile(adminLogin.UserPassword, "MD5");
                var user = db.Users.FirstOrDefault(u => u.UserEmail == adminLogin.UserEmail && u.UserPassword == MyHash);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(adminLogin.UserEmail, true);
                    if (user.IsActive)
                    {
                        if (user.Role.RoleName == "Admin")
                        {
                            return Redirect("/Admin/Dashboard/Index");
                        }
                        else
                        {
                            ModelState.AddModelError("UserEmail", "There is no user with the entered details");
                        }
                    }
                    else
                    {
                        return RedirectToAction("RegisterWithEmail");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserEmail", "There is no user with the entered details");
                }
            }

            return PartialView();
        }
    }
}