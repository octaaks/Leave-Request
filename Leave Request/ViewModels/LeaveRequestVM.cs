using Leave_Request.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Leave_Request.ViewModels
{
    public class LeaveRequestVM
    {
        [Key]
        public int Id { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int LeaveDuration { get; set; }
        public int EmployeeId { get; set; }
        public int StatusId { get; set; }
    }
}
