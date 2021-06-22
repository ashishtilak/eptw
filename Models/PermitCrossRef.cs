using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitCrossRef
    {
        [Key, Column(Order = 0)] public uint PermitId { get; set; }
        [ForeignKey("PermitId")] public Permit Permit { get; set; }

        [Key, Column(Order = 1)] public uint CrossRefPermitId { get; set; }

        public int PermitTypeId { get; set; }
        [ForeignKey("PermitTypeId")] public PermitType PermitType { get; set; }

        public int CrossRefPermitTypeId { get; set; }
        [ForeignKey("CrossRefPermitTypeId")] public PermitType CrossRefPermitType { get; set; }
    }
}
