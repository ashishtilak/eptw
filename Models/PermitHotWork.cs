using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitHotWork
    {
        [Key] public uint Id { get; set; }
        [ForeignKey("Id")] public Permit Permit { get; set; }

        public int PermitTypeId { get; set; }
        [ForeignKey("PermitTypeId")] public PermitType PermitType { get; set; }

        public int PermitNo { get; set; }

        [StringLength(100)] public string Contractor { get; set; }

        public Choices IsolationDone { get; set; }
        public Choices FreeFromOrgVapour { get; set; }
        public Choices VesselFlushed { get; set; }
        public Choices VesselPurged { get; set; }
        public Choices NearEquipmentCovered { get; set; }
        public Choices ElectricalIsolationReq { get; set; }
        public Choices FlushCleanContainer { get; set; }
        public Choices SurroundingAreaCleaned { get; set; }
        public Choices SparkArrangement { get; set; }
        public Choices EquipmentInGoodCondition { get; set; }
        public Choices LineBlinded { get; set; }
        public Choices ManHolesOpen { get; set; }

        public Choices Helmet { get; set; }
        public Choices Shoes { get; set; }
        public Choices Belt { get; set; }
        public Choices Goggles { get; set; }
        public Choices HandGloves { get; set; }
        public Choices EarPlug { get; set; }
        public Choices NoseMask { get; set; }
        
        public Choices ToolsTackles { get; set; }
        public Choices ExtinguisherAvailable { get; set; }
        public Choices UseOfDrum { get; set; }
        public Choices ContinuousSupervision { get; set; }
        public Choices SewersCovered { get; set; }
        public double? LelLevel { get; set; }
        public Choices LelLevelChecked { get; set; }
        public double? CoLevel { get; set; }
        public Choices CoLevelChecked { get; set; }
        [StringLength(20)] public string WatcherName {get;set;}
        public Choices EquipmentIsolated { get; set; }
        public Choices EquipmentEarthen { get; set; }
        public Choices WeldMachineConnection { get; set; }
        public Choices PersonTrained { get; set; }
        public Choices PpeProvided { get; set; }
        public Choices ToolsProvided { get; set; }

    }
}