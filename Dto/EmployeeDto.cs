using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePTW.Dto
{
    public class EmployeeDto
    {
        public string EmpUnqId { get; set; }
        public string CompCode { get; set; }
        public string WrkGrp { get; set; }
        public string EmpTypeCode { get; set; }
        public string UnitCode { get; set; }
        public string DeptCode { get; set; }
        public string StatCode { get; set; }
        public string CatCode { get; set; }
        public string DesgCode { get; set; }
        public string GradeCode { get; set; }
        public string EmpName { get; set; }
        public string FatherName { get; set; }
        public bool Active { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string SapId { get; set; }
        public bool CompanyAcc { get; set; }
        public string ContCode {get;set;}
        public DateTime? BirthDate { get; set; }
        public string Pan { get; set; }
        public DateTime? JoinDate { get; set; }

        public string CompName { get; set; }
        public string WrkGrpDesc { get; set; }
        public string UnitName { get; set; }
        public string DeptName { get; set; }
        public string StatName { get; set; }
        public string CatName { get; set; }
        public string EmpTypeName { get; set; }
        public string GradeName { get; set; }
        public string DesgName { get; set; }

        public List<string> Releasers { get; set; }
    }
}