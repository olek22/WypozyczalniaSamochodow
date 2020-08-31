using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace projektWypozyczalnia_Gola.Models
{
    [MetadataType(typeof(UserMaetadata))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }

    public class UserMaetadata
    {
        [Display(Name = "Imię")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Imie jest wymagane")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }

        [Display(Name = "ID pracownika")]
        public int pracownik_id { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email jest wymagany")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:yyyy-MM-dd}")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Hasło")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "Podaj minimum 6 znaków")]
        public string Password { get; set; }

        [Display(Name = "Portwierdź hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "To hasło jest inne niż to wprowadzone powyżej")]
        public string ConfirmPassword { get; set; }
    }
}