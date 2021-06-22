using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class RoleAuth
    {
        [Key, Column(Order = 0)] public int RoleId { get; set; }
        [Key, Column(Order = 1)]
        [StringLength(100)]
        public string MenuId { get; set; }
        [StringLength(100)] public string MenuName { get; set; }

    }
}
