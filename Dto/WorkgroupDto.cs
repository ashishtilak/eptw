using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Dto
{
    public class WorkgroupDto
    {
        public string CompCode { get; set; }
        public string WrkGrp { get; set; }
        public string WrkGrpDesc { get; set; }
        public string Location { get; set; }
        public DateTime? AddDt { get; set; }
        public string AddUser { get; set; }
    }
}