using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VLTECH.Models
{
    public class LoginModel
    {
        [Key]
        [Required(ErrorMessage = "Sai tai khoan hoac mat khau")]
        [Display(Name = "Email")]
        public string userMail { get; set; }
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}