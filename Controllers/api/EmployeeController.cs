using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ePTW.Dto;
using ePTW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePTW.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployee(string empUnqId)
        {
            Employee emp = _context.Employees
                .FirstOrDefault(e=>e.EmpUnqId == empUnqId);
            if(emp==null)
                return BadRequest("Employee not found");

            EmployeeDto empDto = _mapper.Map<Employee, EmployeeDto>(emp);
            return Ok(empDto);
        }

        // Helper function to return name

        public string GetEmpName(string empUnqId)
        {
            return _context.Employees.FirstOrDefault(e=>e.EmpUnqId == empUnqId)?.EmpName;
        }
    }
}
