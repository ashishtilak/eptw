using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitPerson
    {
        [Key, Column(Order = 0)] public uint PermitId { get; set; }
        [ForeignKey("PermitId")] public Permit Permit { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(10)]
        public string EmpUnqId { get; set; }

        [StringLength(50)] public string Remarks { get; set; }
    }
}