using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.ViewModels
{
    public class RegistrationVM
    {
        [Required(ErrorMessage = "Id must be filled out")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name must be filled out")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address must be filled out")]
        public string Address { get; set; }
        public enum Gender
        {
            Male,
            Female
        }

        [Required(ErrorMessage = "Gender must be filled out")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }

        [Required(ErrorMessage = "Phone must be filled out")]
        [Phone(ErrorMessage = "Phone is not valid")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Birthdate must be filled out")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Salary must be filled out")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Join date must be filled out")]
        public DateTime JoinDate { get; set; }

        [Required(ErrorMessage = "Religion Id must be filled out")]
        public int ReligionId { get; set; }

        [Required(ErrorMessage = "Job Id must be filled out")]
        public int JobId { get; set; }

        [Required(ErrorMessage = "Email must be filled out")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be filled out")]
        [MinLength(8, ErrorMessage = "Password must have minimum 8 characters")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Password must contains numbers, lowercase, and uppercase")]
        public string Password { get; set; }
    }
}
