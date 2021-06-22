using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class Station
    {
        [Key, Column(Order = 0)]
        [StringLength(2)]
        public string CompCode { get; set; }

        public virtual Company Company { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(10)]
        public string WrkGrp { get; set; }

        public virtual Workgroup WorkGroup { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(3)]
        public string UnitCode { get; set; }

        public virtual Unit Unit { get; set; }

        [Key, Column(Order = 3)]
        [StringLength(3)]
        public string DeptCode { get; set; }

        public virtual Department Department { get; set; }

        [Key, Column(Order = 4)]
        [Required]
        [StringLength(3)]
        public string StatCode { get; set; }

        [StringLength(100)] public string StatName { get; set; }

        [StringLength(5)] public string Location { get; set; }
    }
}
