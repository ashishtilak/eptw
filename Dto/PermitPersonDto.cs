using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Dto
{
    public class PermitPersonDto
    {
        public uint PermitId { get; set; }
        public string EmpUnqId { get; set; }
        public string Remarks { get; set; }
        public string ContCode { get; set; }
    }
}