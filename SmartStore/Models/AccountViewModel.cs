using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStore.Models
{
    public class RegisterWithEmailViewModel
    {
        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
    public class RegisterWithPhoneNumberViewModel
    {
        [Display(Name = "Phone Number")]
        [MaxLength(11, ErrorMessage = "Must not exceed {1} characters")]
        public string UserPhoneNumber { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
    public class VerificationCodeViewModel
    {
        [Display(Name = "Verification Code")]
        [MaxLength(6, ErrorMessage = "Must not exceed {1} characters")]
        public string UserVerificationCode { get; set; }
    }
    public class CheckPhoneNumberViewModel
    {
        [Display(Name = "Phone Number")]
        [MaxLength(11, ErrorMessage = "Must not exceed {1} characters")]
        public string UserPhoneNumber { get; set; }
    }

    public class CheckEmailViewModel
    {
        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
    }
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
    public class AdminLoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Display(Name = "Old Password")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.Password)]
        public string UserOldPassword { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Display(Name = "RePassword")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "The selected words do not match")]
        public string UserRePassword { get; set; }
    }

    public class ForgetPasswordViewModel
    {
        [Display(Name = "Verification Code")]
        [MaxLength(6, ErrorMessage = "Must not exceed {1} characters")]
        public string VerificationCode { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Display(Name = "RePassword")]
        [Required(ErrorMessage = "Enter a value for {0}")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "The selected words do not match")]
        public string UserRePassword { get; set; }
    }
    public class ShapCartViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Img")]
        public string ProductImg { get; set; }

        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

    }

}