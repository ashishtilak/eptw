using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePTW.Models;

namespace ePTW.Dto
{
    public class PermitDto
    {
        public uint Id { get; set; }
        public int PermitTypeId { get; set; }
        public int PermitNo { get; set; }
        public string CurrentState { get; set; }

        public DateTime? FromDt { get; set; }
        public DateTime? ToDt { get; set; }

        public string CompCode { get; set; }
        public string CompName { get; set; }

        public string WrkGrp { get; set; }

        public string UnitCode { get; set; }
        public string UnitName { get; set; }

        public string DeptCode { get; set; }
        public string DeptName { get; set; }

        public string StatCode { get; set; }
        public string StatName { get; set; }

        public string WorkLocation { get; set; }
        public string JobDescription { get; set; }

        public string CreatedByEmpId { get; set; }
        public string CreatedByEmpName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string SelfRelStatus { get; set; }
        public DateTime? SelfRelDate { get; set; }
        public string ChangedByEmpId { get; set; }
        public DateTime? LastChangeOn { get; set; }

        public string ResponsiblePerson { get; set; }
        public string ResponsiblePersonName { get; set; }

        //Release Proc
        public bool DeptInchargeRelReq { get; set; }
        public bool AreaInchargeRelReq { get; set; }
        public bool ElecTechRelReq { get; set; }
        public bool ElecInchargeRelReq { get; set; }

        public bool DeptInchargeCloseReq { get; set; }
        public bool AreaInchargeCloseReq { get; set; }
        public bool ElecTechCloseReq { get; set; }
        public bool ElecInchargeCloseReq { get; set; }

        //Dept Incharge
        public string DeptInchEmpId { get; set; }
        public string DeptInchEmpName { get; set; }
        public string DeptInchRelStatus { get; set; }
        public DateTime? DeptInchRelDate { get; set; }
        public string HodRemarks { get; set; }

        //Area Incharge
        public string AreaInchargeEmpId { get; set; }
        public string AreaInchargeEmpName { get; set; }
        public string AreaInchRelStatus { get; set; }
        public DateTime? AreaInchRelDate { get; set; }

        //Elec Tech 
        public string ElecTechEmpId { get; set; }
        public string ElecTechEmpName { get; set; }
        public string ElecTechRelStatus { get; set; }
        public DateTime? ElecTechRelDate { get; set; }

        //Elec Incharge
        public string ElecInchargeEmpId { get; set; }
        public string ElecInchargeEmpName { get; set; }
        public string ElecInchRelStatus { get; set; }
        public DateTime? ElecInchRelDate { get; set; }

        //Safety Incharge
        public string SafetyInchargeEmpId { get; set; }
        public string SafetyInchargeEmpName { get; set; }
        public string SafetyInchargeRelStatus { get; set; }
        public DateTime? SafetyInchRelDate { get; set; }
        public string SafetyRemarks { get; set; }

        public string SafetyRejectionRemarks { get; set; }

        // VP Release in case of permit extend
        public string VpEmpId { get; set; }
        public string VpRelStatus { get; set; }
        public DateTime? VpRelDate { get; set; }
        public string VpRemarks { get; set; }

        //Date same as Safety Inch Rel date
        public DateTime? FullyReleasedOn { get; set; }

        // CLOSURE 
        public string SelfCloseEmpId { get; set; }
        public string SelfCloseEmpName { get; set; }
        public DateTime? SelfCloseDate { get; set; }
        public string SelfCloseRelStatus { get; set; }

        public string CloseDeptInchEmpId { get; set; }
        public string CloseDeptInchEmpName { get; set; }
        public DateTime? DeptInchCloseDate { get; set; }
        public string DeptInchCloseRelStatus { get; set; }

        public string CloseAreaInchEmpId { get; set; }
        public string CloseAreaInchEmpName { get; set; }
        public DateTime? AreaInchCloseDate { get; set; }
        public string AreaInchCloseRelStatus { get; set; }

        public string CloseElecTechEmpId { get; set; }
        public string CloseElecTechEmpName { get; set; }
        public DateTime? ElecTechCloseDate { get; set; }
        public string ElecTechCloseRelStatus { get; set; }

        public string CloseElecInchEmpId { get; set; }
        public string CloseElecInchEmpName { get; set; }
        public DateTime? ElecInchCloseDate { get; set; }
        public string ElecInchCloseRelStatus { get; set; }

        public string CloseSafetyInchEmpId { get; set; }
        public string CloseSafetyInchEmpName { get; set; }
        public DateTime? SafetyInchCloseDate { get; set; }
        public string SafetyInchCloseRelStatus { get; set; }

        //Close Date same as Safety Inch close date
        public DateTime? ClosedOn { get; set; }

        // flags
        public bool AllowUserEdit { get; set; }
        public bool AllowSafetyEdit { get; set; }
        public bool AllowClose { get; set; }
        public bool AllowFinalRelease { get; set; }

        // details tables
        public List<PermitCrossRefDto> CrossRefs { get; set; }
        public List<PermitPersonDto> PermitPersons { get; set; }

        // object of specific permit type
        public PermitHeightDto HeightPermit { get; set; }
        public PermitHotWorkDto HotWorkPermit { get; set; }
        public PermitElecIsolationDto ElecIsolationPermit { get; set; }
        public PermitColdWorkDto ColdWorkPermit { get; set; }
        public PermitVesselEntryDto VesselEntryPermit { get; set; }
    }
}