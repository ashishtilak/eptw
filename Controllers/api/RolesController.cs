using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ePTW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePTW.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RolesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetRoleAuthById")]
        public IActionResult GetRoleAuth(int roleId)
        {
            List<RoleAuth> roleAuth = _context.RoleAuths.Where(r => r.RoleId == roleId).ToList();
            return Ok(roleAuth);
        }

        [HttpGet]
        [Route("GetRoleAuth")]
        public IActionResult GetRoleAuth(string empUnqId)
        {
            int[] rolesOfEmp = _context.RoleUsers
                .Where(e => e.EmpUnqId == empUnqId)
                .Select(r => r.RoleId)
                .ToArray();
            
            List<RoleAuth> roleAuth = _context.RoleAuths
                .Where(r => r.RoleId == 1 || rolesOfEmp.Contains(r.RoleId))
                .ToList();
            return Ok(roleAuth);
        }
    }
}
