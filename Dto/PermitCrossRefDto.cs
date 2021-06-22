using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Dto
{
    public class PermitCrossRefDto
    {
        public uint PermitId { get; set; }
        public uint CrossRefPermitId { get; set; }
        public int PermitTypeId { get; set; }
        public int CrossRefPermitTypeId { get; set; }

        public int? PermitNo { get; set; }
        public string XrefJobDesc { get;set;}
        public string PermitTypeDesc {get;set;}
    }
}