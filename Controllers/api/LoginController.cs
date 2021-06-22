using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ePTW.Dto;
using ePTW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ePTW.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LoginController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Login([FromBody] object requestData)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state.");

            var loginData = JsonConvert.DeserializeObject<LoginDto>(requestData.ToString());

            if (string.IsNullOrEmpty(loginData.EmpUnqId) ||
                string.IsNullOrEmpty(loginData.EmpUnqId))
                return BadRequest("Provide user id and password.");

            Employee emp = _context.Employees
                .Include(e => e.Company)
                .Include(e => e.WorkGroup)
                .Include(e => e.Units)
                .Include(e => e.Departments)
                .Include(e => e.Stations)
                .Include(e => e.Categories)
                .Include(e => e.Designations)
                .Include(e => e.Grades)
                .Include(e => e.EmpTypes)
                .FirstOrDefault(e => e.EmpUnqId == loginData.EmpUnqId);

            if (emp == null)
                return BadRequest("Invalid EmpUnqId.");
            if (emp.Pass != loginData.Password)
                return BadRequest("Invalid Password.");

            var result = _mapper.Map<EmployeeDto>(emp);
            result.CompName = emp.Company.CompName;
            result.WrkGrpDesc = emp.WorkGroup.WrkGrpDesc;
            result.UnitName = emp.Units.UnitName;
            result.DeptName = emp.Departments.DeptName;
            result.StatName = emp.Stations.StatName;
            result.CatName = emp.Categories.CatName;
            result.DesgName = emp.Designations.DesgName;
            result.GradeName = emp.Grades.GradeName;
            result.EmpTypeName = emp.EmpTypes.EmpTypeName;


            List<string> relCodes = _context.ReleaseStrategyLevels
                .Where(e => e.ReleaseStrategy == emp.EmpUnqId)
                .Select(r => r.ReleaseCode)
                .ToList();

            // IF releasers are not found, return object w/o them
            if (relCodes.Count == 0) return Ok(result);

            List<string> releasers = _context.ReleaseAuths
                    .Where(e => relCodes.Contains(e.ReleaseCode))
                    .Select(e => e.EmpUnqId)
                    .ToList();
                result.Releasers = new List<string>();
                result.Releasers.AddRange(releasers);

            return Ok(result);
        }

        private class LoginDto
        {
            public string EmpUnqId { get; set; }
            public string Password { get; set; }
        }

        [HttpPut]
        public IActionResult ChangePassword([FromBody] object requestData)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state.");

            var loginData = JsonConvert.DeserializeObject<LoginDto>(requestData.ToString());

            if (string.IsNullOrEmpty(loginData.EmpUnqId) ||
                string.IsNullOrEmpty(loginData.EmpUnqId))
                return BadRequest("Provide user id and password.");
            try
            {
                Employee emp = _context.Employees.FirstOrDefault(e => e.EmpUnqId == loginData.EmpUnqId);

                if (emp == null)
                    return BadRequest("Invalid employee");

                emp.Pass = loginData.Password;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex);
            }

            return Ok();
        }
    }
}