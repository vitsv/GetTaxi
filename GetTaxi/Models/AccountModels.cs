using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace WebUI.Models
{

    public class LogOnModel
    {
        [Required(ErrorMessage = "Proszę podać telefon")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu powinien składać się z 9 cyfr")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Proszę podać hasło")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
