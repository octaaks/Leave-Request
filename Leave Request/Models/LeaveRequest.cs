using Newtonsoft.Json;
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
        public int ApproverId { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        public virtual LeaveType LeaveType { get; set; }

        [JsonIgnore]
        public virtual ICollection<ManagerFill> ManagerFills { get; set; }

        public LeaveRequest(DateTime requestDate, DateTime startDate, DateTime endDate, int leaveDuration, int employeeId, int approverId, int leaveTypeId)
        {
            RequestDate = requestDate;
            StartDate = startDate;
            EndDate = endDate;
            LeaveDuration = leaveDuration;
            EmployeeId = employeeId;
            ApproverId = approverId;
            LeaveTypeId = leaveTypeId;
        }
    }
}
