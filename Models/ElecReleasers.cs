using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Models
{
    public class ElecReleasers
    {
        [Key] public int Id { get; set; }

        [StringLength(4)] public string Releaser { get; set; }

        [StringLength(2)] public string CompCode { get; set; }
        [ForeignKey("CompCode")] public Company Company { get; set; }

        [StringLength(10)] public string WrkGrp { get; set; }
        [ForeignKey("CompCode, WrkGrp")] public Workgroup WorkGroup { get; set; }

        [StringLength(3)] public string UnitCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, UnitCode")]
        public Unit Units { get; set; }

        [StringLength(3)] public string DeptCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, UnitCode, DeptCode")]
        public Department Departments { get; set; }

        [StringLength(3)] public string StatCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, UnitCode, DeptCode, StatCode")]
        public Station Stations { get; set; }

        [StringLength(3)] public string CatCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, CatCode")]
        public Category Categories { get; set; }

        [StringLength(3)] public string GradeCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, GradeCode")]
        public Grade Grades { get; set; }
    }
}