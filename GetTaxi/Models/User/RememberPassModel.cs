using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.User
{
    public class RemeberPassModel
    {
        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu powinien składać się z 9 cyfr, bez spacii")]
        [StringLength(40)]
        public string Phone { get; set; }
    }
}