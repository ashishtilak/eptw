using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using ePTW.Dto;
using ePTW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

namespace ePTW.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PermitController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private bool ElecReleaser(string elecRel, string empUnqId)
        {
            Employee emp = _context.Employees.FirstOrDefault(e => e.EmpUnqId == empUnqId);
            if (emp == null) return false;

            return _context.ElecReleasers.Any(
                e => e.Releaser == elecRel &
                     e.CompCode == emp.CompCode &&
                     e.WrkGrp == emp.WrkGrp &&
                     e.UnitCode == emp.UnitCode &&
                     e.DeptCode == emp.DeptCode &&
                     e.StatCode == emp.StatCode &&
                     e.CatCode == emp.CatCode &&
                     Convert.ToInt32(e.GradeCode) >= Convert.ToInt32(emp.GradeCode)
            );
        }

        private PermitDto FillDetails(PermitDto permitDto)
        {
            permitDto.CreatedByEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.CreatedByEmpId)?.EmpName;
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

            permitDto.CreatedByEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.CreatedByEmpId)?.EmpName;            
            permitDto.SelfCloseEmpName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.SelfCloseEmpId)?.EmpName;

            permitDto.ResponsiblePersonName = _context.Employees
                .FirstOrDefault(e => e.EmpUnqId == permitDto.ResponsiblePerson)?.EmpName;

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


            switch (permitDto.PermitTypeId)
            {
                case PermitType.HotWorkPermit:
                    permitDto.HotWorkPermit.WatcherEmpName = _context.Employees
                        .FirstOrDefault(e => e.EmpUnqId == permitDto.HotWorkPermit.WatcherName)?.EmpName;

                    permitDto.HotWorkPermit.ContractorName = _context.Employees
                        .FirstOrDefault(e => e.EmpUnqId == permitDto.HotWorkPermit.Contractor)?.EmpName;
                    break;

                case PermitType.ColdWorkPermit:
                    permitDto.ColdWorkPermit.ContractorName = _context.Employees
                        .FirstOrDefault(e => e.EmpUnqId == permitDto.ColdWorkPermit.Contractor)?.EmpName;
                    break;

                case PermitType.HeightPermit:
                    permitDto.HeightPermit.ContractorName = _context.Employees
                        .FirstOrDefault(e => e.EmpUnqId == permitDto.HeightPermit.Contractor)?.EmpName;
                    break;

                case PermitType.VesselEntryPermit:
                    permitDto.VesselEntryPermit.WatcherEmpName = _context.Employees
                        .FirstOrDefault(e => e.EmpUnqId == permitDto.VesselEntryPermit.WatcherName)?.EmpName;

                    permitDto.VesselEntryPermit.ContractorName = _context.Employees
                        .FirstOrDefault(e => e.EmpUnqId == permitDto.VesselEntryPermit.Contractor)?.EmpName;
                    break;
            }

            foreach (PermitCrossRefDto xref in permitDto.CrossRefs)
            {
                Permit tmp = _context.Permits
                    .Include(p => p.PermitType)
                    .FirstOrDefault(p =>
                        p.PermitNo == xref.CrossRefPermitId && p.PermitTypeId == xref.CrossRefPermitTypeId);
                if (tmp == null) continue;

                xref.PermitNo = tmp.PermitNo;
                xref.XrefJobDesc = tmp.JobDescription;
                xref.PermitTypeDesc = tmp.PermitType.PermitTypeDesc;
            }

            foreach (PermitPersonDto person in permitDto.PermitPersons)
            {
                person.ContCode = _context.Employees
                    .FirstOrDefault(e => e.EmpUnqId == person.EmpUnqId)?.ContCode;
            }

            permitDto.UnitName = _context.Units
                .FirstOrDefault(p =>
                    p.CompCode == permitDto.CompCode && p.UnitCode == permitDto.UnitCode)
                ?.UnitName;
            permitDto.DeptName = _context.Departments.FirstOrDefault(p =>
                p.CompCode == permitDto.CompCode &&
                p.UnitCode == permitDto.UnitCode &&
                p.WrkGrp == permitDto.WrkGrp &&
                p.DeptCode == permitDto.DeptCode)?.DeptName;
            permitDto.StatName = _context.Stations.FirstOrDefault(p =>
                p.CompCode == permitDto.CompCode &&
                p.UnitCode == permitDto.UnitCode &&
                p.WrkGrp == permitDto.WrkGrp &&
                p.DeptCode == permitDto.DeptCode &&
                p.StatCode == permitDto.StatCode)?.StatName;

            return permitDto;
        }

        [HttpGet("ElecCheck")]
        public IActionResult ElecCheck(string flag, string empUnqId)
        {
            if (ElecReleaser(flag, empUnqId))
                return Ok();

            return BadRequest("Not authorized.");
        }


        [HttpGet("GetPermitById")]
        public IActionResult GetPermitById(uint id)
        {
            Permit permit = _context.Permits
                .Where(p => p.Id == id)
                .Include(p => p.CrossRefs)
                .Include(p => p.PermitType)
                .Include(p => p.PermitPersons)
                .Include(p => p.HeightPermit)
                .Include(p => p.HotWorkPermit)
                .Include(p => p.ElecIsolationPermit)
                .Include(p => p.ColdWorkPermit)
                .Include(p => p.VesselEntryPermit)
                .FirstOrDefault();

            if (permit == null)
                return BadRequest("Invalid permit.");

            PermitDto permitDto = _mapper.Map<Permit, PermitDto>(permit);

            permitDto = FillDetails(permitDto);

            return Ok(permitDto);
        }

        [HttpGet("GetPermitByNumber")]
        public IActionResult GetPermitByNumber(int permitType, int permitNo)
        {
            Permit permit = _context.Permits
                .Where(p => p.PermitTypeId == permitType && p.PermitNo == permitNo)
                .Include(p => p.CrossRefs)
                .Include(p => p.PermitType)
                .Include(p => p.PermitPersons)
                .Include(p => p.HeightPermit)
                .Include(p => p.HotWorkPermit)
                .Include(p => p.ElecIsolationPermit)
                .Include(p => p.ColdWorkPermit)
                .Include(p => p.VesselEntryPermit)
                .FirstOrDefault();

            if (permit == null)
                return BadRequest("Invalid permit.");


            PermitDto permitDto = _mapper.Map<Permit, PermitDto>(permit);

            permitDto = FillDetails(permitDto);

            return Ok(permitDto);
        }

        [HttpGet("GetPermit")]
        public IActionResult GetPermit(DateTime fromDt, DateTime toDt, string empUnqId = "")
        {
            List<Permit> permits;

            if (empUnqId != "")
            {
                Employee findEmp = _context.Employees.FirstOrDefault(e => e.EmpUnqId == empUnqId);
                if (findEmp == null)
                    return BadRequest("Invalid employee");

                // TODO: Get all employees under the dept incharge of this employee
                // Get releaser of this employee...

                List<string> relCode = _context.ReleaseStrategyLevels
                    .Where(e => e.ReleaseStrategy == empUnqId)
                    .Select(r => r.ReleaseCode).ToList();

                List<string> relEmp = _context.ReleaseAuths
                    .Where(r => relCode.Contains(r.ReleaseCode) && r.Active)
                    .Select(e => e.EmpUnqId).ToList();

                relEmp.Add(empUnqId);

                List<string> relCodes = _context.ReleaseAuths
                    .Where(e => relEmp.Contains(e.EmpUnqId))
                    .Select(e => e.ReleaseCode).ToList();

                List<string> emps = _context.ReleaseStrategyLevels
                    .Where(r => relCodes.Contains(r.ReleaseCode))
                    .Select(e => e.ReleaseStrategy).ToList();

                //string[] emps = _context.Employees.Where(
                //        e => e.CompCode == findEmp.CompCode &&
                //             e.WrkGrp == findEmp.WrkGrp &&
                //             e.UnitCode == findEmp.UnitCode &&
                //             e.DeptCode == findEmp.DeptCode &&
                //             e.StatCode == findEmp.StatCode
                //    )
                //    .Select(e => e.EmpUnqId)
                //    .ToArray();

                permits = _context.Permits
                    .Include(p => p.PermitPersons)
                    .Include(p => p.PermitType)
                    .Include(x => x.CrossRefs)
                    .Include(p => p.HeightPermit)
                    .Include(p => p.HotWorkPermit)
                    .Include(p => p.ColdWorkPermit)
                    .Include(p => p.VesselEntryPermit)
                    .Include(p => p.ElecIsolationPermit)
                    .Where(p =>
                        p.FromDt >= fromDt && p.ToDt <= toDt.AddDays(1).AddMinutes(-1) &&
                        emps.Contains(p.CreatedByEmpId))
                    .ToList();
            }
            else
            {
                permits = _context.Permits
                    .Include(p => p.PermitPersons)
                    .Include(p => p.PermitType)
                    .Include(x => x.CrossRefs)
                    .Include(p => p.HeightPermit)
                    .Include(p => p.HotWorkPermit)
                    .Include(p => p.ColdWorkPermit)
                    .Include(p => p.VesselEntryPermit)
                    .Include(p => p.ElecIsolationPermit)
                    .Where(p => p.FromDt >= fromDt && p.ToDt <= toDt.AddDays(1).AddMinutes(-1))
                    .ToList();
            }

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

                permitDto.SelfCloseEmpName = dto.SelfCloseEmpName;
                permitDto.CloseDeptInchEmpName = dto.CloseDeptInchEmpName;
                permitDto.CloseAreaInchEmpName = dto.CloseAreaInchEmpName;
                permitDto.CloseElecTechEmpName = dto.CloseElecTechEmpName;
                permitDto.CloseElecInchEmpName = dto.CloseElecInchEmpName;
                permitDto.CloseSafetyInchEmpName = dto.CloseSafetyInchEmpName;

                //foreach (PermitCrossRefDto xref in permitDto.CrossRefs)
                //{

                //    xref.PermitNo = dto.CrossRefs.FirstOrDefault(p => p.CrossRefPermitId == xref.CrossRefPermitId)
                //        ?.PermitNo;
                //}
            }


            return Ok(result);
        }

        [HttpPost("CreatePermit")]
        public IActionResult CreatePermit([FromBody] object requestData)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state.");

            PermitDto permit;

            try
            {
                permit = JsonConvert.DeserializeObject<PermitDto>(requestData.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex);
            }

            // Check if any previous open permit exist
            bool exists = _context.Permits.Any(p => p.CreatedByEmpId == permit.CreatedByEmpId &&
                                                    p.FromDt < DateTime.Today &&
                                                    (p.SelfRelStatus == ReleaseStatus.FullyReleased &&
                                                     p.DeptInchRelStatus == ReleaseStatus.FullyReleased &&
                                                     p.SafetyInchargeRelStatus == ReleaseStatus.FullyReleased &&
                                                     p.SelfCloseRelStatus != ReleaseStatus.FullyReleased &&
                                                     p.SafetyInchCloseRelStatus != ReleaseStatus.FullyReleased));
            if (exists)
                return BadRequest("Previous open permits exist. Close them first to create new one.");


            // Get max ids for permit and permit no
            permit.Id = GetMaxPermitId();
            permit.PermitNo = GetMaxPermitNo(permit.PermitTypeId);

            permit.ExtendFlag = false;

            if (!ValidateEmp(permit.CreatedByEmpId))
                return BadRequest("Invalid creator employee id.");

            if (permit.DeptInchEmpId != null && !ValidateEmp(permit.DeptInchEmpId))
                return BadRequest("Invalid department incharge id.");

            if (permit.AreaInchargeEmpId != null && !ValidateEmp(permit.AreaInchargeEmpId))
                return BadRequest("Invalid area incharge id.");

            if (permit.ElecTechEmpId != null && !ValidateEmp(permit.ElecTechEmpId))
                return BadRequest("Invalid elec. technician id.");

            if (permit.ElecInchargeEmpId != null && !ValidateEmp(permit.ElecInchargeEmpId))
                return BadRequest("Invalid elec. incharge id.");

            // CHECK IF ELECTECH AND ELECINCH EMPLOYEE ARE ELIGIBLE
            if (permit.ElecTechEmpId != null && !ElecReleaser("TECH", permit.ElecTechEmpId))
                return BadRequest("Elec. technician id not eligible for release");

            if (permit.ElecInchargeEmpId != null && !ElecReleaser("INCH", permit.ElecInchargeEmpId))
                return BadRequest("Elec. incharge id not eligible for release");

            // If Permit Release Config, set flags accordingly

            PermitReleaseConf flags = _context.PermitReleaseConfs.FirstOrDefault(
                p => p.PermitTypeId == permit.PermitTypeId
            );
            if (flags != null)
            {
                permit.DeptInchargeRelReq = flags.DeptInchargeRelReq;
                permit.AreaInchargeRelReq = flags.AreaInchargeRelReq;
                permit.ElecTechRelReq = flags.ElecTechRelReq;
                permit.ElecInchargeRelReq = flags.ElecInchargeRelReq;
                permit.DeptInchargeCloseReq = flags.DeptInchargeCloseReq;
                permit.AreaInchargeCloseReq = flags.AreaInchargeCloseReq;
                permit.ElecTechCloseReq = flags.ElecTechCloseReq;
                permit.ElecInchargeCloseReq = flags.ElecInchargeCloseReq;
            }

            permit.CurrentState = PermitState.Created;

            permit.AllowUserEdit = true;
            permit.AllowSafetyEdit = true;
            permit.AllowClose = false;

            // Set release status to "I" for all ....

            permit.DeptInchRelStatus = ReleaseStatus.InRelease;
            permit.AreaInchRelStatus = ReleaseStatus.InRelease;
            permit.ElecTechRelStatus = ReleaseStatus.InRelease;
            permit.ElecInchRelStatus = ReleaseStatus.InRelease;
            permit.SafetyInchargeRelStatus = ReleaseStatus.InRelease;
            permit.SelfCloseRelStatus = ReleaseStatus.InRelease;
            permit.VpRelStatus = ReleaseStatus.InRelease;

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                //Permit Cross Reference
                if (permit.CrossRefs != null)
                {
                    foreach (PermitCrossRefDto dto in permit.CrossRefs)
                    {
                        dto.PermitId = permit.Id;

                        // TODO: GET PERMIT ID FROM PERMIT NO
                    }
                }

                // Permit persons
                if (permit.PermitPersons != null)
                {
                    foreach (PermitPersonDto dto in permit.PermitPersons)
                    {
                        dto.PermitId = permit.Id;
                    }
                }

                switch (permit.PermitTypeId)
                {
                    case PermitType.ColdWorkPermit:
                        permit.ColdWorkPermit.Id = permit.Id;
                        permit.ColdWorkPermit.PermitTypeId = PermitType.ColdWorkPermit;
                        permit.ColdWorkPermit.PermitNo = permit.PermitNo;
                        break;
                    case PermitType.ElectricalIsolationPermit:
                        permit.ElecIsolationPermit.Id = permit.Id;
                        permit.ElecIsolationPermit.PermitTypeId = PermitType.ElectricalIsolationPermit;
                        permit.ElecIsolationPermit.PermitNo = permit.PermitNo;
                        break;
                    case PermitType.HeightPermit:
                        permit.HeightPermit.Id = permit.Id;
                        permit.HeightPermit.PermitTypeId = PermitType.HeightPermit;
                        permit.HeightPermit.PermitNo = permit.PermitNo;
                        break;
                    case PermitType.HotWorkPermit:
                        permit.HotWorkPermit.Id = permit.Id;
                        permit.HotWorkPermit.PermitTypeId = PermitType.HotWorkPermit;
                        permit.HotWorkPermit.PermitNo = permit.PermitNo;
                        break;
                    case PermitType.VesselEntryPermit:
                        permit.VesselEntryPermit.Id = permit.Id;
                        permit.VesselEntryPermit.PermitTypeId = PermitType.VesselEntryPermit;
                        permit.VesselEntryPermit.PermitNo = permit.PermitNo;
                        break;
                }

                _context.Permits.Add(_mapper.Map<PermitDto, Permit>(permit));

                _context.SaveChanges();
                transaction.Commit();
            }

            return Ok(permit);
        }

        private uint GetMaxPermitId()
        {
            uint permitId = 0;
            try
            {
                permitId = _context.Permits.Max(e => e.Id);
                permitId++;
            }
            catch (Exception)
            {
                permitId = 1;
            }

            return permitId;
        }

        private int GetMaxPermitNo(int permitTypeId)
        {
            var maxPermitNo = 0;
            try
            {
                maxPermitNo = _context.Permits
                    .Where(p => p.PermitTypeId == permitTypeId)
                    .Max(e => e.PermitNo);
                maxPermitNo++;
            }
            catch (Exception)
            {
                maxPermitNo = 1;
            }

            return maxPermitNo;
        }

        private bool ValidateEmp(string empUnqId)
        {
            return _context.Employees.Any(e => e.EmpUnqId == empUnqId);
        }

        public class FlagsData
        {
            public uint PermitId { get; set; }
            public bool DeptInchargeRelReq { get; set; }
            public bool AreaInchargeRelReq { get; set; }
            public bool ElecTechRelReq { get; set; }
            public bool ElecInchargeRelReq { get; set; }
            public bool DeptInchargeCloseReq { get; set; }
            public bool AreaInchargeCloseReq { get; set; }
            public bool ElecTechCloseReq { get; set; }
            public bool ElecInchargeCloseReq { get; set; }
            public string ChangedBy { get; set; }
        }

        [HttpPut("SetFlags")]
        public IActionResult SetFlags([FromBody] object requestData)
        {
            FlagsData flags;
            try
            {
                flags = JsonConvert.DeserializeObject<FlagsData>(requestData.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex);
            }

            Permit permit = _context.Permits.FirstOrDefault(p => p.Id == flags.PermitId);
            if (permit == null)
                return BadRequest("Invlaid permit id.");

            permit.DeptInchargeRelReq = flags.DeptInchargeRelReq;
            permit.AreaInchargeRelReq = flags.AreaInchargeRelReq;
            permit.ElecTechRelReq = flags.ElecTechRelReq;
            permit.ElecInchargeRelReq = flags.ElecInchargeRelReq;
            permit.DeptInchargeCloseReq = flags.DeptInchargeCloseReq;
            permit.AreaInchargeCloseReq = flags.AreaInchargeCloseReq;
            permit.ElecTechCloseReq = flags.ElecTechCloseReq;
            permit.ElecInchargeCloseReq = flags.ElecInchargeCloseReq;
            permit.ChangedByEmpId = flags.ChangedBy;
            permit.LastChangeOn = DateTime.Now;

            _context.SaveChanges();

            return Ok(_mapper.Map<Permit, PermitDto>(permit));
        }

        [HttpPut("ChangePermit")]
        public IActionResult ChangePermit([FromBody] object requestData)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state.");

            PermitDto dto;

            try
            {
                dto = JsonConvert.DeserializeObject<PermitDto>(requestData.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex);
            }

            Permit permit = _context.Permits
                .Include(p => p.PermitPersons)
                .Include(x => x.CrossRefs)
                .Include(p => p.HeightPermit)
                .Include(p => p.HotWorkPermit)
                .Include(p => p.ColdWorkPermit)
                .Include(p => p.VesselEntryPermit)
                .Include(p => p.ElecIsolationPermit)
                .Single(p => p.Id == dto.Id);
            if (permit == null)
                return BadRequest("Invalid permit.");


            // User validation if user himself is chaning
            if (dto.ChangedByEmpId != permit.CreatedByEmpId)
            {
                Employee emp = _context.Employees.Single(e => e.EmpUnqId == dto.ChangedByEmpId);

                // check in safety dept stat table if employee belongs to safety dept
                bool bFound = _context.SafetyDeptStats
                    .Any(stat =>
                        emp.UnitCode == stat.UnitCode &&
                        emp.DeptCode == stat.DeptCode &&
                        emp.StatCode == stat.StatCode);

                if (!bFound)
                {
                    if (!ElecReleaser("INCH", dto.ChangedByEmpId))
                        if (!ElecReleaser("TECH", dto.ChangedByEmpId))
                            return BadRequest("Changes not allowed.");
                }
            }
            else
            {
                if (!permit.AllowUserEdit)
                {
                    return BadRequest("Permit changes are not allowed now.");
                }
            }

            // set all values from dto to object

            permit.FromDt = dto.FromDt;
            permit.ToDt = dto.ToDt;
            permit.WorkLocation = dto.WorkLocation;
            permit.JobDescription = dto.JobDescription;
            // permit.CreatedByEmpId = dto.CreatedByEmpId;
            permit.ChangedByEmpId = dto.ChangedByEmpId;
            permit.LastChangeOn = DateTime.Now;
            permit.DeptInchEmpId = dto.DeptInchEmpId;
            permit.AreaInchargeEmpId = dto.AreaInchargeEmpId;
            permit.ElecTechEmpId = dto.ElecTechEmpId;
            permit.ElecInchargeEmpId = dto.ElecInchargeEmpId;
            permit.ResponsiblePerson = dto.ResponsiblePerson;
            permit.SafetyInchargeEmpId = dto.SafetyInchargeEmpId;
            permit.SafetyRemarks = dto.SafetyRemarks;

            // validations

            //if (!ValidateEmp(permit.CreatedByEmpId))
            //    return BadRequest("Invalid creator employee id.");

            //if (permit.DeptInchEmpId != null && !ValidateEmp(permit.DeptInchEmpId))
            //    return BadRequest("Invalid department incharge id.");

            //if (permit.AreaInchargeEmpId != null && !ValidateEmp(permit.AreaInchargeEmpId))
            //    return BadRequest("Invalid area incharge id.");

            if (permit.ElecTechEmpId != null && !ValidateEmp(permit.ElecTechEmpId))
                return BadRequest("Invalid elec. technician id.");

            if (permit.ElecInchargeEmpId != null && !ValidateEmp(permit.ElecInchargeEmpId))
                return BadRequest("Invalid elec. incharge id.");

            // CHECK IF ELECTECH AND ELECINCH EMPLOYEE ARE ELIGIBLE
            if (permit.ElecTechEmpId != null && !ElecReleaser("TECH", permit.ElecTechEmpId))
                return BadRequest("Elec. technician id not eligible for release");

            if (permit.ElecInchargeEmpId != null && !ElecReleaser("INCH", permit.ElecInchargeEmpId))
                return BadRequest("Elec. incharge id not eligible for release");


            // permit persons
            foreach (PermitPerson person in permit.PermitPersons.ToList())
            {
                permit.PermitPersons.Remove(person);
            }

            if (dto.PermitPersons != null && dto.PermitPersons.Count > 0)
            {
                foreach (PermitPersonDto person in dto.PermitPersons)
                {
                    permit.PermitPersons.Add(
                        _mapper.Map<PermitPersonDto, PermitPerson>(person));
                }
            }


            // cross ref
            foreach (PermitCrossRef crossRef in permit.CrossRefs.ToList())
            {
                permit.CrossRefs.Remove(crossRef);
            }

            if (dto.CrossRefs != null && dto.CrossRefs.Count > 0)
            {
                foreach (PermitCrossRefDto crossRef in dto.CrossRefs)
                {
                    permit.CrossRefs.Add(
                        _mapper.Map<PermitCrossRefDto, PermitCrossRef>(crossRef));
                }
            }


            // permit wise object
            switch (permit.PermitTypeId)
            {
                case PermitType.ColdWorkPermit:
                    permit.ColdWorkPermit = _mapper.Map<PermitColdWorkDto, PermitColdWork>(dto.ColdWorkPermit);
                    break;
                case PermitType.ElectricalIsolationPermit:
                    permit.ElecIsolationPermit =
                        _mapper.Map<PermitElecIsolationDto, PermitElecIsolation>(dto.ElecIsolationPermit);
                    break;
                case PermitType.HeightPermit:
                    permit.HeightPermit = _mapper.Map<PermitHeightDto, PermitHeight>(dto.HeightPermit);
                    break;
                case PermitType.HotWorkPermit:
                    permit.HotWorkPermit = _mapper.Map<PermitHotWorkDto, PermitHotWork>(dto.HotWorkPermit);
                    break;
                case PermitType.VesselEntryPermit:
                    permit.VesselEntryPermit =
                        _mapper.Map<PermitVesselEntryDto, PermitVesselEntry>(dto.VesselEntryPermit);
                    break;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex);
            }

            return Ok(_mapper.Map<Permit, PermitDto>(permit));
        }
    }
}