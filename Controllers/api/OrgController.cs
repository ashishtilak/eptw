using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using ePTW.Dto;
using ePTW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ePTW.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrgController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("getdept")]
        public IActionResult GetDept()
        {
            List<Department> dept = _context.Departments.Where(
                    d => d.CompCode == "01" && d.WrkGrp == "COMP")
                .ToList();

            return Ok(_mapper.Map<List<Department>, List<DepartmentDto>>(dept));
        }

        [HttpGet("getstation")]
        public IActionResult GetStation(string deptCode)
        {
            List<Station> stat = _context.Stations.Where(
                    d => d.CompCode == "01" && d.WrkGrp == "COMP" && d.DeptCode == deptCode)
                .ToList();

            return Ok(_mapper.Map<List<Station>, List<StationDto>>(stat));
        }
    }
}