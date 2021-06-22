using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePTW.Models
{
    public class Employee
    {
        [Key] [StringLength(10)] public string EmpUnqId { get; set; }

        [StringLength(2)] public string CompCode { get; set; }
        [ForeignKey("CompCode")] public Company Company { get; set; }

        [StringLength(10)] public string WrkGrp { get; set; }
        [ForeignKey("CompCode, WrkGrp")] public Workgroup WorkGroup { get; set; }

        [StringLength(3)] public string EmpTypeCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, EmpTypeCode")]
        public EmpType EmpTypes { get; set; }

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

        [StringLength(3)] public string DesgCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, DesgCode")]
        public Designation Designations { get; set; }

        [StringLength(3)] public string GradeCode { get; set; }

        [ForeignKey("CompCode, WrkGrp, GradeCode")]
        public Grade Grades { get; set; }

        [StringLength(50)] public string EmpName { get; set; }

        [StringLength(50)] public string FatherName { get; set; }

        public bool Active { get; set; }

        [StringLength(20)] public string Pass { get; set; }

        public string Email { get; set; }

        [StringLength(5)] public string Location { get; set; }

        [StringLength(12)] public string SapId { get; set; }

        public bool CompanyAcc { get; set; }

        [StringLength(3)]
        public string ContCode {get; set;}

        [Column(TypeName = "datetime2")] public DateTime? BirthDate { get; set; }
        [StringLength(10)] public string Pan { get; set; }

        [Column(TypeName = "datetime2")] public DateTime? JoinDate { get; set; }
    }
}