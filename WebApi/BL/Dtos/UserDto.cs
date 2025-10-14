using AppResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class UserDto : BaseDto
    {
            [Required(ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "EmailRequired")]
            [EmailAddress(ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "InvalidEmail")]
            public string Email { get; set; }

            [Required(ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "PasswordRequired")]
            [MinLength(6, ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "PasswordMinLength")]
            public string Password { get; set; }

            [Required(ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "ConfirmPasswordRequired")]
            [Compare("Password", ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "PasswordMismatch")]
            public string? ConfirmPassword { get; set; }

            [Required(ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "FirstNameRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "FirstNameMaxLength")]
            public string FirstName { get; set; }

            [Required(ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "LastNameRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "LastNameMaxLength")]
            public string LastName { get; set; }

            [Required(ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "PhoneRequired")]
            [Phone(ErrorMessageResourceType = typeof(Shiping), ErrorMessageResourceName = "InvalidPhone")]
            public string Phone { get; set; }

            public string? Role { get; set; }

            public string? ReturnUrl { get; set; }
     }
    
}


