using System;
using System.Collections.Generic;

namespace ePTW.Dto
{
    public class ObsDto
    {
        public uint Id { get; set; }

        //UA/UC
        public string ObsCatg { get; set; }
        public DateTime ObsDate { get; set; }

        // will be fixed - '01'
        public string CompCode { get; set; }
        public string CompanyName { get; set; }

        // will be fixed - 'COMP'
        public string WrkGrp { get; set; }

        public string UnitCode { get; set; }
        public string UnitName { get; set; }

        public string DeptCode { get; set; }
        public string DeptName { get; set; }

        public string StatCode { get; set; }
        public string StatName { get; set; }


        public string Location { get; set; }
        public string ObsDetails { get; set; }
        public string CorrectiveAction { get; set; }

        public string PersonResponsible { get; set; }

        public DateTime TargetDate { get; set; }
        public DateTime? ComplianceDate { get; set; }

        public string ObsStatus { get; set; } // Open/close
        public string Remarks { get; set; }

        public string ObservedBy { get; set; }
        public string ObservedByName { get; set; }
        public DateTime? CreatedOn { get; set; }

        //following will be chagned by person resp or safety
        public DateTime? StatusUpdateDate { get; set; }

        //safety release for creation
        public string ReleaseBy { get; set; }
        public string ReleaseByName { get; set; }
        public string ReleaseStatus { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseRemarks { get; set; }

        //safety release for status change
        public string StatusUpdateReleaseBy { get; set; }
        public string StatusUpdateReleaseByName { get; set; }
        public string StatusUpdateReleaseStatus { get; set; }
        public DateTime? StatusUpdateReleaseDate { get; set; }
        public string StatusUpdateReleaseRemarks { get; set; }

        public List<ObsHistoryDto> ObsHistory { get; set; }

        public byte[] ObsImage { get; set; }
        public byte[] CompImage { get; set; }
    }
}