using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Dto
{
    public class PermitHistoryDto
    {
        public int Id { get; set; }
        public DateTime ExtendDate { get; set; }

        public uint PermitId { get; set; }
        public PermitDto Permit { get; set; }

        public DateTime? FromDt { get; set; }
        public DateTime? ToDt { get; set; }

        public string WorkLocation { get; set; }
        public string JobDescription { get; set; }

        public string CreatedByEmpId { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string ChangedByEmpId { get; set; }
        public DateTime? LastChangeOn { get; set; }

        public string SelfRelStatus { get; set; }
        public DateTime? SelfRelDate { get; set; }

        public string ResponsiblePerson { get; set; }

        //Dept Incharge
        public string DeptInchEmpId { get; set; }
        public string DeptInchRelStatus { get; set; }
        public DateTime? DeptInchRelDate { get; set; }
        public string HodRemarks { get; set; }

        //Area Incharge
        public string AreaInchargeEmpId { get; set; }
        public string AreaInchRelStatus { get; set; }
        public DateTime? AreaInchRelDate { get; set; }
        public string AreaInchRemarks { get; set; }

        //Elec Tech 
        public string ElecTechEmpId { get; set; }
        public string ElecTechRelStatus { get; set; }
        public DateTime? ElecTechRelDate { get; set; }
        public string ElecTechRemarks { get; set; }

        //Elec Incharge
        public string ElecInchargeEmpId { get; set; }
        public string ElecInchRelStatus { get; set; }
        public DateTime? ElecInchRelDate { get; set; }
        public string ElecInchRemarks { get; set; }

        //Safety Incharge
        public string SafetyInchargeEmpId { get; set; }
        public string SafetyInchargeRelStatus { get; set; }
        public DateTime? SafetyInchRelDate { get; set; }
        public string SafetyRemarks { get; set; }
        public string SafetyRejectionRemarks { get; set; }

        //Date same as Safety Inch Rel date
        public DateTime? FullyReleasedOn { get; set; }
    }
}