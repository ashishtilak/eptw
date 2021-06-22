using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePTW.Models;

namespace ePTW.Dto
{
    public class PermitVesselEntryDto
    {
        public uint Id { get; set; }
        public int PermitTypeId { get; set; }
        public int PermitNo { get; set; }

        public string Contractor { get; set; }
        public string ContractorName {get;set;}

        public Choices VesselFreeFromToxicMaterial { get; set; }
        public Choices LinesBlinded { get; set; }
        public Choices ElectricalIsolationReq { get; set; }
        public Choices HandLampProvided { get; set; }
        public double? OxygenLevel { get; set; }
        public Choices OxygenLevelCheck { get; set; }
        public Choices WorkAreaSafe { get; set; }
        public Choices ExplosiveLevelTestReq { get; set; }

        public Choices Helmet { get; set; }
        public Choices Shoes { get; set; }
        public Choices Belt { get; set; }
        public Choices Goggles { get; set; }
        public Choices HandGloves { get; set; }
        public Choices EarPlug { get; set; }
        public Choices NoseMask { get; set; }

        public Choices Ladder { get; set; }
        public Choices RespiratoryProtectionReq { get; set; }
        public Choices LockOutArrangement { get; set; }
        public double? ExplosiveLevel { get; set; }
        public string WatcherName { get; set; }
        public string WatcherEmpName { get; set; }
        public Choices EquipmentIsolated { get; set; }
        public Choices EquipmentEarthen { get; set; }
        public Choices HandLampExtension { get; set; }
        public Choices PersonTrained { get; set; }
        public Choices PpeProvided { get; set; }
        public Choices ToolsProvided { get; set; }
    }
}