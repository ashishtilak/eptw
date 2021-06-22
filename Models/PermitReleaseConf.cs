using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class PermitReleaseConf
    {
        [Key, Column(Order = 0)] public int PermitTypeId { get; set; }
        [ForeignKey("PermitTypeId")] public PermitType PermitType { get; set; }

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