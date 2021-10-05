using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Models
{
    [Table("tb_m_manager_fills")]
    public class ManagerFill
    {
        [Key]
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime DateApproved { get; set; }
        public int LeaveRequestId { get; set; }
        public int StatusId { get; set; }

        [JsonIgnore]
        public virtual LeaveRequest LeaveRequest { get; set; }

        [JsonIgnore]
        public virtual Status Status { get; set; }

        public ManagerFill(string note, DateTime dateApproved, int leaveRequestId, int statusId)
        {
            Note = note;
            DateApproved = dateApproved;
            LeaveRequestId = leaveRequestId;
            StatusId = statusId;
        }
    }
}
