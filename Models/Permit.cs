using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class Permit
    {
        [Key] public uint Id { get; set; }

        public int PermitTypeId { get; set; }
        [ForeignKey("PermitTypeId")] public PermitType PermitType { get; set; }

        public int PermitNo { get; set; }

        [StringLength(1)] public string CurrentState { get; set; }
        [ForeignKey("CurrentState")] public PermitState PermitState { get; set; }

        [Column(TypeName = "datetime2")] public DateTime? FromDt { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? ToDt { get; set; }

        [StringLength(2)] public string CompCode { get; set; }
        [ForeignKey("CompCode")] public Company Company { get; set; }

        [StringLength(10)] public string WrkGrp { get; set; }
        [ForeignKey("CompCode, WrkGrp")] public Workgroup WorkGroup { get; set; }

        [StringLength(3)] public string UnitCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, UnitCode")]
        public Unit Units { get; set; }

        [StringLength(3)] public string DeptCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, UnitCode, DeptCode")]
        public Department Departments { get; set; }

        [StringLength(3)] public string StatCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, UnitCode, DeptCode, StatCode")]
        public Station Stations { get; set; }

        [StringLength(50)] public string WorkLocation { get; set; }
        [StringLength(255)] public string JobDescription { get; set; }

        [StringLength(10)] public string CreatedByEmpId { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? CreatedOn { get; set; }

        [StringLength(10)] public string ChangedByEmpId { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? LastChangeOn { get; set; }

        [StringLength(1)] public string SelfRelStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? SelfRelDate { get; set; }


        [StringLength(10)] public string ResponsiblePerson { get; set; }

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
        [StringLength(10)] public string DeptInchEmpId { get; set; }
        [StringLength(1)] public string DeptInchRelStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? DeptInchRelDate { get; set; }
        [StringLength(50)] public string HodRemarks { get; set; }

        //Area Incharge
        [StringLength(10)] public string AreaInchargeEmpId { get; set; }
        [StringLength(1)] public string AreaInchRelStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? AreaInchRelDate { get; set; }
        [StringLength(50)] public string AreaInchRemarks { get; set; }

        //Elec Tech 
        [StringLength(10)] public string ElecTechEmpId { get; set; }
        [StringLength(1)] public string ElecTechRelStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? ElecTechRelDate { get; set; }
        [StringLength(50)] public string ElecTechRemarks { get; set; }

        //Elec Incharge
        [StringLength(10)] public string ElecInchargeEmpId { get; set; }
        [StringLength(1)] public string ElecInchRelStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? ElecInchRelDate { get; set; }
        [StringLength(50)] public string ElecInchRemarks { get; set; }

        //Safety Incharge
        [StringLength(10)] public string SafetyInchargeEmpId { get; set; }
        [StringLength(1)] public string SafetyInchargeRelStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? SafetyInchRelDate { get; set; }
        [StringLength(50)] public string SafetyRemarks { get; set; }
        [StringLength(50)] public string SafetyRejectionRemarks { get; set; }


        // VP Release in case of permit extend
        //Dept Incharge
        [StringLength(10)] public string VpEmpId { get; set; }
        [StringLength(1)] public string VpRelStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? VpRelDate { get; set; }
        [StringLength(50)] public string VpRemarks { get; set; }

        //Date same as Safety Inch Rel date
        [Column(TypeName = "datetime2")] public DateTime? FullyReleasedOn { get; set; }

        // CLOSURE 
        [StringLength(10)] public string SelfCloseEmpId { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? SelfCloseDate { get; set; }
        [StringLength(1)] public string SelfCloseRelStatus { get; set; }
        [StringLength(50)] public string SelfCloseRemarks { get; set; }

        [StringLength(10)] public string CloseDeptInchEmpId { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? DeptInchCloseDate { get; set; }
        [StringLength(1)] public string DeptInchCloseRelStatus { get; set; }
        [StringLength(50)] public string DeptInchCloseRemarks { get; set; }

        [StringLength(10)] public string CloseAreaInchEmpId { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? AreaInchCloseDate { get; set; }
        [StringLength(1)] public string AreaInchCloseRelStatus { get; set; }
        [StringLength(50)] public string AreaInchCloseRemarks { get; set; }

        [StringLength(10)] public string CloseElecTechEmpId { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? ElecTechCloseDate { get; set; }
        [StringLength(1)] public string ElecTechCloseRelStatus { get; set; }
        [StringLength(50)] public string ElecTechCloseRemarks { get; set; }

        [StringLength(10)] public string CloseElecInchEmpId { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? ElecInchCloseDate { get; set; }
        [StringLength(1)] public string ElecInchCloseRelStatus { get; set; }
        [StringLength(50)] public string ElecInchCloseRemarks { get; set; }

        [StringLength(10)] public string CloseSafetyInchEmpId { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? SafetyInchCloseDate { get; set; }
        [StringLength(1)] public string SafetyInchCloseRelStatus { get; set; }
        [StringLength(50)] public string SafetyInchCloseRemarks { get; set; }

        //Close Date same as Safety Inch close date
        [Column(TypeName = "datetime2")] public DateTime? ClosedOn { get; set; }

        // flags
        public bool AllowUserEdit { get; set; }
        public bool AllowSafetyEdit { get; set; }
        public bool AllowClose { get; set; }
        public bool AllowFinalRelease { get; set; }

        // details tables
        public ICollection<PermitCrossRef> CrossRefs { get; set; }
        public ICollection<PermitPerson> PermitPersons { get; set; }

        // object of specific permit type
        public PermitHeight HeightPermit { get; set; }
        public PermitHotWork HotWorkPermit { get; set; }
        public PermitElecIsolation ElecIsolationPermit { get; set; }
        public PermitColdWork ColdWorkPermit { get; set; }
        public PermitVesselEntry VesselEntryPermit { get; set; }

        [Column(TypeName = "datetime2")] public DateTime? OriginalToDate { get; set; }
        public bool ExtendFlag { get; set; }
    }

    public enum Choices
    {
        Yes = 1,
        No = 2,
        NotApplicable = 3
    }

    public enum RiskLevel
    {
        High = 1,
        Medium = 2,
        Low = 3
    }
}