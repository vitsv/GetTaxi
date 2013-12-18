using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.User
{
    public class ActivateModel
    {
        [Required(ErrorMessage = "Proszę podać kod")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Kod powinien składać się z 5 cyfr, bez spacii")]
        public string Code { get; set; }
    }
}