using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Models
{
    public class ManagerFill
    {
        [Key]
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime DateApproved { get; set; }
        public int LeaveRequestId { get; set; }
        public int StatusId { get; set; }
    }
}
