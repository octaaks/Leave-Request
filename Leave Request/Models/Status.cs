using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Models
{
    [Table("tb_m_statuses")]
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<ManagerFill> ManagerFills { get; set; }
    }
}
