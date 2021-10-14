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
        public string LeaveTypeName { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int LeaveDuration { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Notes { get; set; }
        public int ManagerFillId { get; set; }
    }
}
