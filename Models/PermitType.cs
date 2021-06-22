using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitType
    {
        [Key] public int Id { get; set; }
        [StringLength(50)] public string PermitTypeDesc { get; set; }
        [StringLength(50)] public string DocNo { get; set; }
        [StringLength(50)] public string Version { get; set; }
        [Column(TypeName = "datetime2")] public DateTime EffectiveDate { get; set; }
        public bool PersonEntryRequired { get; set; }
        public bool Active { get; set; }

        public const int HeightPermit = 1;
        public const int HotWorkPermit = 2;
        public const int ElectricalIsolationPermit = 3;
        public const int ColdWorkPermit = 4;
        public const int VesselEntryPermit = 5;
    }
}

