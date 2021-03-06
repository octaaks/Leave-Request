using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Models
{
    [Table("tb_m_employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public int TotalLeave { get; set; }
        public string StatusEmplo { get; set; }
        public int ReligionId { get; set; }
        public int JobId { get; set; }
        

        [JsonIgnore]
        public virtual Account Account { get; set; }

        [JsonIgnore]
        public virtual Religion Religion { get; set; }

        [JsonIgnore]
        public virtual Job Job { get; set; }

        [JsonIgnore]
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
