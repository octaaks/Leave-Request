using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Models
{
    [Table("tb_m_leave_requests")]
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LeaveDuration { get; set; }

        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int StatusId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<ManagerFill> ManagerFills { get; set; }
    }
}
