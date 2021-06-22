using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class EmpType
    {
        [Key, Column(Order = 0)]
        [StringLength(2)]
        public string CompCode { get; set; }

        public Company Company { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(10)]
        public string WrkGrp { get; set; }

        public Workgroup WorkGroup { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        [StringLength(3)]
        public string EmpTypeCode { get; set; }

        [StringLength(50)] public string EmpTypeName { get; set; }

        [StringLength(5)] public string Location { get; set; }
    }
}
