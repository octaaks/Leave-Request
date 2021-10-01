using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Models
{
    [Table("tb_m_Statuses")]
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
