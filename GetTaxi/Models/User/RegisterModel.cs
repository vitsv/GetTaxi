using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.User
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        [StringLength(40)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu powinien składać się z 9 cyfr, bez spacii")]
        [StringLength(40)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Hasło musi zawierac od 5 do 25 znaków")]
        public string Password { get; set; }
    }
}