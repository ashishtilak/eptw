using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;

namespace ePTW.Models
{
    public class Observation
    {
        [Key] public uint Id { get; set; }

        //UA/UC
        [StringLength(2)] public string ObsCatg { get; set; }
        [Column(TypeName = "datetime2")] public DateTime ObsDate { get; set; }

        // will be fixed - '01'
        [StringLength(2)] public string CompCode { get; set; }
        [ForeignKey("CompCode")] public Company Company { get; set; }

        // will be fixed - 'COMP'
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

        [StringLength(50)] public string Location { get; set; }
        [StringLength(500)] public string ObsDetails { get; set; }
        [StringLength(500)] public string CorrectiveAction { get; set; }

        [StringLength(10)] public string PersonResponsible { get; set; }

        [Column(TypeName = "datetime2")] public DateTime TargetDate { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? ComplianceDate { get; set; }

        [StringLength(1)] public string ObsStatus { get; set; } // Open/close
        [StringLength(255)] public string Remarks { get; set; }

        [StringLength(10)] public string ObservedBy { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? CreatedOn { get; set; }

        //following will be chagned by person resp or safety
        [Column(TypeName = "datetime2")] public DateTime? StatusUpdateDate { get; set; }

        //safety release for creation
        [StringLength(10)] public string ReleaseBy { get; set; }
        [StringLength(1)] public string ReleaseStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? ReleaseDate { get; set; }
        [StringLength(50)] public string ReleaseRemarks { get; set; }

        //safety release for status change
        [StringLength(10)] public string StatusUpdateReleaseBy { get; set; }
        [StringLength(1)] public string StatusUpdateReleaseStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? StatusUpdateReleaseDate { get; set; }
        [StringLength(50)] public string StatusUpdateReleaseRemarks { get; set; }
    }
}