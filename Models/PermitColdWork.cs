using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitColdWork
    {
        [Key] public uint Id { get; set; }
        [ForeignKey("Id")] public Permit Permit { get; set; }

        public int PermitTypeId { get; set; }
        [ForeignKey("PermitTypeId")] public PermitType PermitType { get; set; }

        public int PermitNo { get; set; }

        [StringLength(100)] public string Contractor { get; set; }

        public Choices Isolationdone { get; set; }
        public Choices SafetyTags { get; set; }
        public Choices PipeConnectionBlanked { get; set; }
        [StringLength(50)] public string PipeConnectionDetail { get; set; }
        public Choices WorkAreaCleaned { get; set; }
        public Choices PossibilityOfFlammable { get; set; }
        public Choices PossibilityOfGasPressure { get; set; }
        public Choices ElectricalIsolationDone { get; set; }
        public Choices AreaBarricated { get; set; }
        [StringLength(100)] public string OtherCheck { get; set; }
        public Choices Helmet { get; set; }
        public Choices Shoes { get; set; }
        public Choices Belt { get; set; }
        public Choices Goggles { get; set; }
        public Choices HandGloves { get; set; }
        public Choices EarPlug { get; set; }
        public Choices NoseMask { get; set; }
    }
}