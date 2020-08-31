using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projektWypozyczalnia_Gola.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Nowe hasło jest wymagane", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage="Nowe hasło różni się od tego podanego w patwierdzeniu")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}