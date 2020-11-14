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
   public class User 
    {
        public int Id { get; set; }
        public int RoleId { get; set; }

        [Display(Name = "User Code")]
        [MaxLength(8, ErrorMessage = "Must not exceed {1} characters")]
        public string UserCode { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        public string UserFirstName { get; set; }
         
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        public string UserLastName { get; set; }

        [Display(Name = "User Image")]
        public string UserImage { get; set; }

        [Display(Name = "Father Name")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        public string UserFatherName { get; set; }

        [Display(Name = "Date of Birth")]
        [MaxLength(10, ErrorMessage = "Must not exceed {1} characters")]
        public string UserDateofBirth { get; set; }

        //[Display(Name = "Gender")]
        //[Required(ErrorMessage = "Enter a value for {0}")]
        //public string UserGender { get; set; }

        [Display(Name = "National Code")]
        [MaxLength(10, ErrorMessage = "Must not exceed {1} characters")]
        public string UserNationalCode { get; set; }

        [Display(Name = "Postal Code")]
        [MaxLength(10, ErrorMessage = "Must not exceed {1} characters")]
        public string UserPostalCode { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(11, ErrorMessage = "Must not exceed {1} characters")]
        public string UserPhoneNumber { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Display(Name = "Password")]
        //[Required(ErrorMessage = "Enter a value for {0}")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Display(Name = "Verification Code")]
        [MaxLength(6, ErrorMessage = "Must not exceed {1} characters")]
        public string UserVerificationCode { get; set; }

        [Display(Name = "Identifier Code")]
        [MaxLength(8, ErrorMessage = "Must not exceed {1} characters")]
        public string UserIdentifierCode { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Address")]
        [MaxLength(200, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string UserAddress { get; set; }

        [Display(Name = "Landline Phone")]
        [MaxLength(11, ErrorMessage = "Must not exceed {1} characters")]
        public string UserLandlinePhone { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}
