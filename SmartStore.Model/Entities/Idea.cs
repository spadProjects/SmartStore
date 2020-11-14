using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartStore.Models.Basement;

namespace SmartStore.Model.Entities
{

    public class Idea
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "AliasName or FullName")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        public string AliasName { get; set; }


        [Display(Name = "File")]
        [FileExtensions(Extensions = "jpg,jpeg,docx,png,txt,pdf")]
        public IFormFile File { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MMM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Skills")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string Skill { get; set; }

        [Display(Name = "Work Experience")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string WorkExperience { get; set; }

        [Display(Name = "Degree of Education")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string DegreeOfEducation { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Job(Student,science Committee,Professor,etc)")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string Job { get; set; }

        [Display(Name = "Idea Title")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        public string IdeaTitle { get; set; }

        [Display(Name = "Area Of​ Expertise")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string AreaOf​Expertise { get; set; }

        [Display(Name = "Features Of the Idea")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string FeaturesOftheIdea { get; set; }

        [Display(Name = "Description")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Benefits Of theIdea")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string BenefitsOftheIdea { get; set; }

        [Display(Name = "ThePurpose Of the Idea")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string ThePurposeOftheIdea { get; set; }

        [Display(Name = "Idea Costs")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string IdeaCosts { get; set; }

    }

    public interface IFormFile
    {
    }
}
