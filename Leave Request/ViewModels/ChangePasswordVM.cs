using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.ViewModels
{
    public class ChangePasswordVM
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email format not correct!")]
        public string Email { get; set; }

        [Required]
        public string OldPassword { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must have minimum 8 characters")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Password must contains numbers, lowercase, and uppercase")]
        public string NewPassword { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must have minimum 8 characters")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Password must contains numbers, lowercase, and uppercase")]
        public string ConfirmNewPassword { get; set; }
    }
}
