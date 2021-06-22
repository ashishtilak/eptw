using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitHeight
    {
        [Key] public uint Id { get; set; }
        [ForeignKey("Id")] public Permit Permit {get;set;}

        public int PermitTypeId { get; set; }
        [ForeignKey("PermitTypeId")] public PermitType PermitType { get; set; }

        public int PermitNo { get; set; }

        [StringLength(100)] public string Contractor {get;set;}
        public Choices Scaffolding {get;set;}
        public Choices SafetyBelt {get;set;}
        public Choices SafetyLadder {get;set;}
        public Choices FallArrestor {get;set;}
        public Choices LifeLine {get;set;}
        public Choices InstructionsGiven {get;set;}
        [StringLength(10)] public string ApproxHeight {get;set;}
        public Choices LiftingTools {get;set;}
        public Choices SafetyNet {get;set;}
        public Choices AreaBarricated {get;set;}
        public Choices Helmet { get; set; }
        public Choices Shoes { get; set; }
        public Choices Belt { get; set; }
        public Choices Goggles { get; set; }
        public Choices HandGloves { get; set; }
        public Choices EarPlug { get; set; }
        public Choices NoseMask { get; set; }
        
    }
}
