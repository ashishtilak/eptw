using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class RoleUser
    {
        [Key, Column(Order = 0)] public int RoleId { get; set; }
        [Key, Column(Order = 1)]
        [StringLength(10)]
        public string EmpUnqId { get; set; }

        [StringLength(10)] public string UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
