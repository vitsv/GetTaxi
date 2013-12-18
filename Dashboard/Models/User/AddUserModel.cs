using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dashboard.Models.User
{
    public class AddUserModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Login is required!")]
        [StringLength(20)]
        public string Login { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(30)]
        public string Password { get; set; }

        [RegularExpression(@"^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$", ErrorMessage = "Wrong email")]
        [Required(ErrorMessage = "Email is required!")]
        [StringLength(40)]
        public string Email { get; set; }

        public int? RoleId { get; set; }
    }
}