using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Dto
{
    public class PermitReleaseConfDto
    {
        public int PermitTypeId { get; set; }
        public int RiskLevel { get; set; }

        public bool DeptInchargeRelReq { get; set; }
        public bool AreaInchargeRelReq { get; set; }
        public bool ElecTechRelReq { get; set; }
        public bool ElecInchargeRelReq { get; set; }

        public bool DeptInchargeCloseReq { get; set; }
        public bool AreaInchargeCloseReq { get; set; }
        public bool ElecTechCloseReq { get; set; }
        public bool ElecInchargeCloseReq { get; set; }

    }
}
