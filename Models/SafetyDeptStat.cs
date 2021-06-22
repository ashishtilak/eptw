using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class SafetyDeptStat
    {
        // store diff safety dept/stat 
        public int Id { get; set; }
        [StringLength(2)] public string CompCode { get; set; }
        [StringLength(10)] public string WrkGrp { get; set; }
        [StringLength(3)] public string UnitCode { get; set; }
        [StringLength(3)] public string DeptCode { get; set; }
        [StringLength(3)] public string StatCode { get; set; }
    }
}