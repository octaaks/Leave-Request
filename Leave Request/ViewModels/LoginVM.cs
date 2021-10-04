using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.ViewModels
{
    public class LoginVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email must be filled out")]
        [Email(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be filled out")]
        [MinLength(8, ErrorMessage = "Password must have minimum 8 characters")]
        public string Password { get; set; }

        public string Phone { get; set; }
    }
}