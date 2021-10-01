using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Leave_Request.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        public Gender gender { get; set; }
        [Phone]
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public int TotalLeave { get; set; }
        public int ReligionId { get; set; }
        public int JobId { get; set; }
    }
}
