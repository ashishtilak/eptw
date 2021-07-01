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
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

namespace ePTW.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitExtend : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PermitExtend(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        private PermitDto FillDetails(PermitDto permitDto)
        {
            permitDto.DeptInchEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.DeptInchEmpId)?.EmpName;
            permitDto.AreaInchargeEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.AreaInchargeEmpId)?.EmpName;
            permitDto.ElecTechEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.ElecTechEmpId)?.EmpName;
            permitDto.ElecInchargeEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.ElecInchargeEmpId)?.EmpName;
            permitDto.SafetyInchargeEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.SafetyInchargeEmpId)?.EmpName;

            permitDto.CloseDeptInchEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.CloseDeptInchEmpId)?.EmpName;
            permitDto.CloseAreaInchEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.CloseAreaInchEmpId)?.EmpName;
            permitDto.CloseElecTechEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.CloseElecTechEmpId)?.EmpName;
            permitDto.CloseElecInchEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.CloseElecInchEmpId)?.EmpName;
            permitDto.CloseSafetyInchEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.CloseSafetyInchEmpId)?.EmpName;

            foreach (PermitCrossRefDto xref in permitDto.CrossRefs)
                xref.PermitNo = _context.Permits.FirstOrDefault(p => p.Id == xref.CrossRefPermitId)?.PermitNo;

            return permitDto;
        }

        [HttpGet("vp")]
        public IActionResult GetVpReleasers()
        {
            var vp = _context.VpReleasers.Select(
                    e => new {e.EmpUnqId, e.Employee.EmpName})
                .ToList();

            return Ok(vp);
        }


        [HttpGet]
        public IActionResult GetPermits(string empUnqId)
        {
            if (empUnqId == "") return BadRequest("Invalid employee id.");


            Employee findEmp = _context.Employees.FirstOrDefault(e => e.EmpUnqId == empUnqId);
            if (findEmp == null)
                return BadRequest("Invalid employee");

            string[] emps = _context.Employees.Where(
                    e => e.CompCode == findEmp.CompCode &&
                         e.WrkGrp == findEmp.WrkGrp &&
                         e.UnitCode == findEmp.UnitCode &&
                         e.DeptCode == findEmp.DeptCode &&
                         e.StatCode == findEmp.StatCode
                )
                .Select(e => e.EmpUnqId)
                .ToArray();

            DateTime today = DateTime.Today;

            List<Permit> permits = _context.Permits
                .Include(p => p.PermitPersons)
                .Include(x => x.CrossRefs)
                .Include(p => p.HeightPermit)
                .Include(p => p.HotWorkPermit)
                .Include(p => p.ColdWorkPermit)
                .Include(p => p.VesselEntryPermit)
                .Include(p => p.ElecIsolationPermit)
                .Where(p =>
                    p.FromDt >= today && p.ToDt <= today.AddDays(1).AddMinutes(-1) &&
                    emps.Contains(p.CreatedByEmpId) &&
                    p.SafetyInchargeRelStatus == ReleaseStatus.FullyReleased)
                .ToList();

            if (permits.Count == 0)
                return BadRequest("Permit not found.");

            List<PermitDto> result = _mapper.Map<List<Permit>, List<PermitDto>>(permits);

            foreach (PermitDto permitDto in result)
            {
                PermitDto dto = FillDetails(permitDto);

                permitDto.DeptInchEmpName = dto.DeptInchEmpName;
                permitDto.AreaInchargeEmpName = dto.AreaInchargeEmpName;
                permitDto.ElecTechEmpName = dto.ElecTechEmpName;
                permitDto.ElecInchargeEmpName = dto.ElecInchargeEmpName;
                permitDto.SafetyInchargeEmpName = dto.SafetyInchargeEmpName;

                permitDto.CloseDeptInchEmpName = dto.CloseDeptInchEmpName;
                permitDto.CloseAreaInchEmpName = dto.CloseAreaInchEmpName;
                permitDto.CloseElecTechEmpName = dto.CloseElecTechEmpName;
                permitDto.CloseElecInchEmpName = dto.CloseElecInchEmpName;
                permitDto.CloseSafetyInchEmpName = dto.CloseSafetyInchEmpName;

                foreach (PermitCrossRefDto xref in permitDto.CrossRefs)
                    xref.PermitNo = dto.CrossRefs.FirstOrDefault(p => p.CrossRefPermitId == xref.CrossRefPermitId)
                        ?.PermitNo;
            }


            return Ok(result);
        }


        [HttpPost]
        public IActionResult ExtendPermit([FromBody] object requestData)
        {
            PermitDto dto;

            try
            {
                dto = JsonConvert.DeserializeObject<PermitDto>(requestData.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex);
            }

            if (dto.Id == 0) return BadRequest("Invalid permit.");


            try
            {
                using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    Permit permit = _context.Permits.FirstOrDefault(p => p.Id == dto.Id);
                    if (permit == null)
                        return BadRequest("Permit not found!");

                    var history = new PermitHistory();

                    history = _mapper.Map<Permit, PermitHistory>(permit);
                    history.PermitId = permit.Id;
                    history.ExtendDate = DateTime.Now;
                    history.Id = 0;

                    _context.PermitHistories.Add(history);
                    _context.SaveChanges();

                    permit.OriginalToDate = permit.ToDt;
                    permit.ExtendFlag = true;

                    permit.ToDt = dto.ToDt;

                    permit.AllowUserEdit = true;
                    permit.AllowSafetyEdit = true;
                    permit.AllowClose = false;

                    permit.DeptInchRelStatus = ReleaseStatus.InRelease;
                    permit.AreaInchRelStatus = ReleaseStatus.InRelease;
                    permit.ElecTechRelStatus = ReleaseStatus.InRelease;
                    permit.ElecInchRelStatus = ReleaseStatus.InRelease;
                    permit.SafetyInchargeRelStatus = ReleaseStatus.InRelease;

                    permit.DeptInchEmpId = dto.DeptInchEmpId;
                    permit.AreaInchargeEmpId = dto.AreaInchargeEmpId;
                    permit.VpEmpId = dto.VpEmpId;

                    //validate: Dept inch must be VP releaser for permit extend.

                    if (dto.ToDt != null && dto.ToDt.Value.TimeOfDay > new TimeSpan(19, 00, 00))
                    {
                        if (dto.VpEmpId != null)
                        {
                            if (!_context.VpReleasers.Any(e => e.EmpUnqId == dto.VpEmpId))
                                return BadRequest("Dept releaser Id must be of Vice President");

                            permit.VpRelStatus = ReleaseStatus.InRelease;
                        }
                        else
                        {
                            return BadRequest("Vice President release id must be set for permit extend > 7:00 pm.");
                        }
                    }

                    permit.CurrentState = PermitState.PartiallyReleased;

                    _context.SaveChanges();

                    transaction.Commit();
                }

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }
    }
}