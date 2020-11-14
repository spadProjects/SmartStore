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
using System.IO;

namespace SmartStore.Controllers
{
    public class ReceiveIdeaController : Controller
    {
        // GET: ReceiveIdea

        SmartStoreContext db = new SmartStoreContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "Id,AliasName,DegreeOfEducation,Email,Job,IdeaTitle,AreaOfExpertise,FeaturesOftheIdea,Description,BenefitsOftheIdea,ThePurposeOftheIdea,IdeaCosts,WorkExperience,Skill,DateOfBirth")] Idea idea)
        {
            if (!db.Ideas.Any(u => u.Email == idea.Email))
            {
                Idea ide = new Idea()
                {
                    Id = idea.Id,
                    AliasName = idea.AliasName,
                    DegreeOfEducation = idea.DegreeOfEducation,
                    Email = idea.Email,
                    Job = idea.Job,
                    IdeaTitle = idea.IdeaTitle,
                    AreaOfExpertise = idea.AreaOfExpertise,
                    FeaturesOftheIdea = idea.FeaturesOftheIdea,
                    Description = idea.Description,
                    BenefitsOftheIdea = idea.BenefitsOftheIdea,
                    ThePurposeOftheIdea = idea.ThePurposeOftheIdea,
                    IdeaCosts = idea.IdeaCosts,
                    Skill = idea.Skill,
                    WorkExperience = idea.WorkExperience,
                    DateOfBirth = idea.DateOfBirth.Date,
                    File = idea.File
                };

                db.Ideas.Add(ide);
                db.SaveChanges();

                SendEmail.Send(idea.Email, "Profile of the idea owner", "Your request has been successfully submitted");

                return Redirect("/Home/Index");

            }
            else
            {
                ModelState.AddModelError("Email", "You are already Registered");
            }

            return View(idea);
        }

        public ActionResult IdeaHeader()
        {
            return PartialView(db.IdeaHeaders.Where(i => i.NotShow == false).ToList());
        }

        [HttpPost]
        public ActionResult Upload(List<HttpPostedFileBase> fileData)
        {
            string path = Server.MapPath("~/Uploads/");
            foreach (HttpPostedFileBase postedFile in fileData)
            {
                if (postedFile != null)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(path + fileName);
                }
            }

            return Content("Success");
        }
    }
}