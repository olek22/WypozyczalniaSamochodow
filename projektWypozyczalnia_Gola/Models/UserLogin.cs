using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projektWypozyczalnia_Gola.Models
{
    public class UserLogin
    {
        [Display(Name ="Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Login jest wymagany")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}