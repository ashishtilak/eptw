using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Dto
{
    public class PermitTypeDto
    {
        public int Id { get; set; }
        public string PermitTypeDesc { get; set; }
        public string DocNo { get; set; }
        public string Version { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool PersonEntryRequired { get; set; }
        public bool Active { get; set; }
    }
}
