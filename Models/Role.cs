using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class Role
    {
        [Key] public int RoleId { get; set; }
        [StringLength(50)] public string RoleName { get; set; }
    }
}
