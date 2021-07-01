using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitHistory
    {
        [Key] public int Id { get; set; }
        [Column(TypeName = "datetime2")] public DateTime ExtendDate { get; set; }
        
        public uint PermitId { get; set; }
        [ForeignKey("PermitId")] public Permit Permit { get; set; }

        [Column(TypeName = "datetime2")] public DateTime? FromDt { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? ToDt { get; set; }

        [StringLength(50)] public string WorkLocation { get; set; }
        [StringLength(255)] public string JobDescription { get; set; }

        [StringLength(10)] public string CreatedByEmpId { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? CreatedOn { get; set; }

        [StringLength(10)] public string ChangedByEmpId { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? LastChangeOn { get; set; }

        [StringLength(1)] public string SelfRelStatus { get; set; }
        [Column(TypeName = "datetime2")] public DateTime? SelfRelDate { get; set; }

        [StringLength(10)] public string ResponsiblePerson { get; set; }

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

        //Date same as Safety Inch Rel date
        [Column(TypeName = "datetime2")] public DateTime? FullyReleasedOn { get; set; }

    }
}