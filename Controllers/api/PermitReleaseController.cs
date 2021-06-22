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
    public class PermitReleaseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PermitReleaseController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public static class Modes
        {
            public const string DeptInch = "DI";
            public const string AreaInch = "AI";
            public const string ElecTech = "ET";
            public const string ElecInch = "EI";
            public const string VpRelease = "VP";
            public const string FinalRelease = "F";
            public const string CloseSelf = "CS";
            public const string CloseDept = "CD";
            public const string CloseAreaInch = "CA";
            public const string CloseElecTech = "CE";
            public const string CloseElecInch = "CI";
            public const string FinalClose = "C";
            public const string Reject = "R";
        }

        // Modes:
        // DI - Dept Incharge
        // AI - Area Incharge
        // ET - Elect Tech
        // EI - Elect Inch
        // F - Final safety release
        // CD - Dept Closure
        // CA - Area inch close
        // CE - Elec Tech Close
        // CI - Elec Inch Close
        // C - Final Close


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
                     Convert.ToInt32(e.GradeCode) <= Convert.ToInt32(emp.GradeCode)
            );
        }


        [HttpGet("GetPermit")]
        public IActionResult GetPermit(string empUnqId, string mode)
        {
            Employee emp = _context.Employees.FirstOrDefault(e => e.EmpUnqId == empUnqId);
            if (emp == null)
                return BadRequest("Invalid employee.");

            var permit = new List<PermitDto>();

            switch (mode)
            {
                case Modes.DeptInch: // Dept inch
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.DeptInchEmpId == empUnqId &&
                            p.DeptInchRelStatus == ReleaseStatus.InRelease).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;
                case Modes.AreaInch: // Area inch
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.DeptInchRelStatus == ReleaseStatus.FullyReleased &&
                            p.AreaInchargeEmpId == empUnqId &&
                            p.AreaInchRelStatus == ReleaseStatus.InRelease).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;
                case Modes.ElecTech: // Elec Tech rel
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.ElecTechEmpId == empUnqId &&
                            p.ElecTechRelStatus == ReleaseStatus.InRelease).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;
                case Modes.ElecInch: // Elec Inch Rel
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.ElecInchargeEmpId == empUnqId &&
                            p.ElecInchRelStatus == ReleaseStatus.InRelease).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;
                case Modes.VpRelease: // Area inch
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.DeptInchRelStatus == ReleaseStatus.FullyReleased &&
                            p.VpEmpId == empUnqId &&
                            p.VpRelStatus == ReleaseStatus.InRelease).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;

                case Modes.FinalRelease:
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.SafetyInchargeRelStatus == ReleaseStatus.InRelease).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;

                case Modes.CloseSelf:
                    // Get self created or open permits of dept/stat
                    // and it should be fully released
                    // and dept inch id is null and dept inch close date is null
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.CompCode == emp.CompCode &&
                            p.WrkGrp == emp.WrkGrp &&
                            p.UnitCode == emp.UnitCode &&
                            p.DeptCode == emp.DeptCode &&
                            p.StatCode == emp.StatCode &&
                            p.SafetyInchargeRelStatus == ReleaseStatus.FullyReleased &&
                            p.SelfCloseRelStatus == ReleaseStatus.InRelease
                        ).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;

                case Modes.CloseDept:
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.SafetyInchargeRelStatus == ReleaseStatus.FullyReleased &&
                            p.CloseDeptInchEmpId == empUnqId &&
                            p.DeptInchCloseRelStatus == ReleaseStatus.InRelease).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;

                case Modes.CloseAreaInch:
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.SafetyInchargeRelStatus == ReleaseStatus.FullyReleased &&
                            p.CloseAreaInchEmpId == empUnqId &&
                            p.AreaInchCloseRelStatus == ReleaseStatus.InRelease).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;

                case Modes.CloseElecTech:
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.SafetyInchargeRelStatus == ReleaseStatus.FullyReleased &&
                            p.CloseElecTechEmpId == empUnqId &&
                            p.ElecTechCloseRelStatus == ReleaseStatus.InRelease).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;
                case Modes.CloseElecInch:
                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.SafetyInchargeRelStatus == ReleaseStatus.FullyReleased &&
                            p.CloseElecInchEmpId == empUnqId &&
                            p.ElecInchCloseRelStatus == ReleaseStatus.InRelease).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;

                case Modes.FinalClose:

                    // see if this is safety user
                    // show him all permits pending for closure at any stage

                    permit = _context.Permits
                        .Include(p => p.PermitPersons)
                        .Include(p => p.CrossRefs)
                        .Include(p => p.HeightPermit)
                        .Include(p => p.HotWorkPermit)
                        .Include(p => p.ColdWorkPermit)
                        .Include(p => p.ElecIsolationPermit)
                        .Include(p => p.VesselEntryPermit)
                        .Where(p =>
                            p.SafetyInchargeRelStatus == ReleaseStatus.FullyReleased &&
                            p.SelfCloseRelStatus == ReleaseStatus.FullyReleased &&
                            p.SafetyInchCloseRelStatus == ReleaseStatus.InRelease
                        ).AsEnumerable()
                        .Select(_mapper.Map<Permit, PermitDto>)
                        .ToList();
                    break;
            }


            foreach (PermitDto permitDto in permit)
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
            }

            return Ok(permit);
        }


        public class ReleaseInfo
        {
            public string Mode { get; set; }
            public uint Id { get; set; }
            public string EmpUnqid { get; set; }
            public string Remarks { get; set; }
        }


        [HttpPost("PermitRelease")]
        public IActionResult PermitRelease([FromBody] object requestdata)
        {
            var data = JsonConvert.DeserializeObject<ReleaseInfo>(requestdata.ToString());
            var errors = new List<string>();

            Employee emp = _context.Employees.FirstOrDefault(e => e.EmpUnqId == data.EmpUnqid);
            if (emp == null)
                return BadRequest("Invalid employee.");

            Permit permit = _context.Permits.FirstOrDefault(p => p.Id == data.Id);
            if (permit == null)
                return BadRequest("Permit not found!");

            switch (data.Mode)
            {
                case Modes.DeptInch:
                    if (permit.DeptInchEmpId != data.EmpUnqid)
                        errors.Add("Invalid dept inch Id.");

                    if (permit.DeptInchRelStatus != ReleaseStatus.InRelease)
                        errors.Add("Permit is not in dept inch release.");

                    permit.DeptInchRelStatus = ReleaseStatus.FullyReleased;
                    permit.DeptInchRelDate = DateTime.Now;
                    permit.HodRemarks = data.Remarks;
                    permit.CurrentState = PermitState.PartiallyReleased;

                    // if safety officer perviously rejected this,
                    // then set it in release again.
                    if (permit.SafetyInchargeRelStatus == ReleaseStatus.ReleaseRejected)
                        permit.SafetyInchargeRelStatus = ReleaseStatus.InRelease;

                    //TODO: set appropriate flags
                    permit.AllowUserEdit = false;

                    break;

                case Modes.AreaInch:
                    if (permit.AreaInchargeEmpId != data.EmpUnqid)
                        errors.Add("Invalid area inch Id.");

                    if (permit.AreaInchRelStatus != ReleaseStatus.InRelease)
                        errors.Add("Permit is not in area inch release.");

                    permit.AreaInchRelStatus = ReleaseStatus.FullyReleased;
                    permit.AreaInchRelDate = DateTime.Now;
                    permit.AreaInchRemarks = data.Remarks;

                    // if safety officer perviously rejected this,
                    // then set it in release again.
                    if (permit.SafetyInchargeRelStatus == ReleaseStatus.ReleaseRejected)
                        permit.SafetyInchargeRelStatus = ReleaseStatus.InRelease;

                    break;

                case Modes.ElecTech:
                    if (permit.ElecTechEmpId != data.EmpUnqid)
                        errors.Add("Invalid Elec tech Id.");

                    if (permit.ElecTechRelStatus != ReleaseStatus.InRelease)
                        errors.Add("Permit is not in elec tech release.");

                    permit.ElecTechRelStatus = ReleaseStatus.FullyReleased;
                    permit.ElecTechRelDate = DateTime.Now;
                    permit.ElecTechRemarks = data.Remarks;

                    // if safety officer perviously rejected this,
                    // then set it in release again.
                    if (permit.SafetyInchargeRelStatus == ReleaseStatus.ReleaseRejected)
                        permit.SafetyInchargeRelStatus = ReleaseStatus.InRelease;

                    break;

                case Modes.ElecInch:
                    if (permit.ElecInchargeEmpId != data.EmpUnqid)
                        errors.Add("Invalid Elec inch Id.");

                    if (permit.ElecInchRelStatus != ReleaseStatus.InRelease)
                        errors.Add("Permit is not in elec inch release.");

                    permit.ElecInchRelStatus = ReleaseStatus.FullyReleased;
                    permit.ElecInchRelDate = DateTime.Now;
                    permit.ElecInchRemarks = data.Remarks;

                    // if safety officer perviously rejected this,
                    // then set it in release again.
                    if (permit.SafetyInchargeRelStatus == ReleaseStatus.ReleaseRejected)
                        permit.SafetyInchargeRelStatus = ReleaseStatus.InRelease;

                    break;

                case Modes.VpRelease:
                    if (permit.VpEmpId != data.EmpUnqid)
                        errors.Add("Invalid VP releaser employee Id.");

                    if (permit.VpRelStatus != ReleaseStatus.InRelease)
                        errors.Add("Permit is not in Vp release.");

                    permit.VpRelStatus = ReleaseStatus.FullyReleased;
                    permit.VpRelDate = DateTime.Now;
                    permit.VpRemarks = data.Remarks;

                    break;
                case Modes.FinalRelease:
                    bool bFound = _context.SafetyDeptStats
                        .Any(stat =>
                            emp.UnitCode == stat.UnitCode &&
                            emp.DeptCode == stat.DeptCode &&
                            emp.StatCode == stat.StatCode);

                    if (!bFound)
                        return BadRequest("Employee not from safety dept.");

                    // TODO: Check flags
                    PermitReleaseConf relConfig = _context.PermitReleaseConfs
                        .FirstOrDefault(p => p.PermitTypeId == permit.PermitTypeId);

                    if (relConfig != null)
                    {
                        if (relConfig.DeptInchargeRelReq &&
                            permit.DeptInchRelStatus != ReleaseStatus.FullyReleased)
                            errors.Add("Dept incharge release is pending.");

                        if (relConfig.AreaInchargeRelReq &&
                            permit.AreaInchRelStatus != ReleaseStatus.FullyReleased)
                            errors.Add("Area incharge release is pending.");

                        if (relConfig.ElecTechRelReq &&
                            permit.ElecTechRelStatus != ReleaseStatus.FullyReleased)
                            errors.Add("Electric technician release is pending.");

                        if (relConfig.ElecInchargeRelReq &&
                            permit.ElecInchRelStatus != ReleaseStatus.FullyReleased)
                            errors.Add("Electric incharge release is pending.");
                    }


                    // check if photo uploaded

                    if (!_context.PermitPhotos.Any(p => p.Id == permit.Id))
                        errors.Add("Must upload photo before release.");


                    // Release
                    permit.SafetyInchargeEmpId = data.EmpUnqid;
                    permit.SafetyInchargeRelStatus = ReleaseStatus.FullyReleased;
                    permit.SafetyInchRelDate = DateTime.Now;
                    permit.FullyReleasedOn = DateTime.Now;
                    permit.SafetyRemarks = data.Remarks;

                    // TODO: Set flags
                    permit.CurrentState = PermitState.FullyReleased;
                    permit.AllowUserEdit = false;
                    permit.AllowSafetyEdit = false;
                    permit.AllowClose = true;

                    break;

                case Modes.Reject:
                    bool bFoundR = _context.SafetyDeptStats
                        .Any(stat =>
                            emp.UnitCode == stat.UnitCode &&
                            emp.DeptCode == stat.DeptCode &&
                            emp.StatCode == stat.StatCode);

                    if (!bFoundR)
                        return BadRequest("Employee not from safety dept.");

                    permit.SafetyInchargeEmpId = data.EmpUnqid;
                    permit.SafetyInchargeRelStatus = ReleaseStatus.ReleaseRejected;
                    permit.SafetyInchRelDate = DateTime.Now;
                    permit.SafetyRemarks = data.Remarks;

                    permit.DeptInchRelStatus = ReleaseStatus.InRelease;
                    permit.AreaInchRelStatus = ReleaseStatus.InRelease;
                    permit.ElecTechRelStatus = ReleaseStatus.InRelease;
                    permit.ElecInchRelStatus = ReleaseStatus.InRelease;

                    permit.CurrentState = PermitState.PartiallyReleased;
                    permit.AllowUserEdit = true;
                    permit.AllowSafetyEdit = true;
                    permit.AllowClose = false;
                    
                    break;

                case Modes.CloseSelf:

                    if (permit.CreatedByEmpId != data.EmpUnqid)
                    {
                        if (!(permit.CompCode == emp.CompCode &&
                              permit.WrkGrp == emp.WrkGrp &&
                              permit.UnitCode == emp.UnitCode &&
                              permit.DeptCode == emp.DeptCode &&
                              permit.StatCode == emp.StatCode))
                        {
                            errors.Add("You're not authorized to close this permit.");
                            break;
                        }
                    }

                    if (!(permit.SafetyInchargeRelStatus == ReleaseStatus.FullyReleased &&
                          permit.SelfCloseRelStatus == ReleaseStatus.InRelease))
                    {
                        errors.Add("Permit is not fully released/already closed");
                        break;
                    }

                    permit.SelfCloseEmpId = data.EmpUnqid;
                    permit.SelfCloseDate = DateTime.Now;
                    permit.SelfCloseRelStatus = ReleaseStatus.FullyReleased;
                    permit.SelfCloseRemarks = data.Remarks;

                    permit.CurrentState = PermitState.ClosureStarted;

                    permit.CloseDeptInchEmpId = permit.DeptInchEmpId;
                    permit.DeptInchCloseRelStatus = ReleaseStatus.InRelease;
                    permit.CloseAreaInchEmpId = permit.AreaInchargeEmpId;
                    permit.AreaInchCloseRelStatus = ReleaseStatus.InRelease;
                    permit.CloseElecTechEmpId = permit.ElecTechEmpId;
                    permit.ElecTechCloseRelStatus = ReleaseStatus.InRelease;
                    permit.CloseElecInchEmpId = permit.ElecInchargeEmpId;
                    permit.ElecInchCloseRelStatus = ReleaseStatus.InRelease;
                    permit.SafetyInchCloseRelStatus = ReleaseStatus.InRelease;

                    break;

                case Modes.CloseDept:
                    if (permit.CloseDeptInchEmpId != data.EmpUnqid)
                        errors.Add("Invalid Dept incharge Id.");

                    if (permit.DeptInchCloseRelStatus != ReleaseStatus.InRelease)
                        errors.Add("Permit is not in closure.");

                    permit.DeptInchCloseRelStatus = ReleaseStatus.FullyReleased;
                    permit.DeptInchCloseDate = DateTime.Now;
                    permit.DeptInchCloseRemarks = data.Remarks;
                    permit.CurrentState = PermitState.PartiallyClosed;
                    break;

                case Modes.CloseAreaInch:
                    if (permit.CloseAreaInchEmpId != data.EmpUnqid)
                        errors.Add("Invalid Area incharge Id.");

                    if (permit.AreaInchCloseRelStatus != ReleaseStatus.InRelease)
                        errors.Add("Permit is not in closure.");

                    permit.AreaInchCloseRelStatus = ReleaseStatus.FullyReleased;
                    permit.AreaInchCloseDate = DateTime.Now;
                    permit.AreaInchCloseRemarks = data.Remarks;
                    break;

                case Modes.CloseElecTech:
                    if (permit.CloseElecTechEmpId != data.EmpUnqid)
                        errors.Add("Invalid Elec tech Id.");

                    if (permit.ElecTechCloseRelStatus != ReleaseStatus.InRelease)
                        errors.Add("Permit is not in closure.");

                    permit.ElecTechCloseRelStatus = ReleaseStatus.FullyReleased;
                    permit.ElecTechCloseDate = DateTime.Now;
                    permit.ElecTechCloseRemarks = data.Remarks;
                    break;

                case Modes.CloseElecInch:
                    if (permit.CloseElecInchEmpId != data.EmpUnqid)
                        errors.Add("Invalid Elec incharge Id.");

                    if (permit.ElecInchCloseRelStatus != ReleaseStatus.InRelease)
                        errors.Add("Permit is not in closure.");

                    permit.ElecInchCloseRelStatus = ReleaseStatus.FullyReleased;
                    permit.ElecInchCloseDate = DateTime.Now;
                    permit.ElecInchCloseRemarks = data.Remarks;
                    break;

                case Modes.FinalClose:

                    if (!_context.SafetyDeptStats
                        .Any(stat =>
                            emp.UnitCode == stat.UnitCode &&
                            emp.DeptCode == stat.DeptCode &&
                            emp.StatCode == stat.StatCode))
                        return BadRequest("Employee not from safety dept.");

                    // TODO: Check flags
                    PermitReleaseConf closeRelConfig = _context.PermitReleaseConfs
                        .FirstOrDefault(p => p.PermitType == permit.PermitType);

                    if (closeRelConfig != null)
                    {
                        if (closeRelConfig.DeptInchargeCloseReq &&
                            permit.DeptInchCloseRelStatus != ReleaseStatus.FullyReleased)
                            errors.Add("Dept incharge close is pending.");

                        if (closeRelConfig.AreaInchargeCloseReq &&
                            permit.AreaInchCloseRelStatus != ReleaseStatus.FullyReleased)
                            errors.Add("Area incharge close is pending.");

                        if (closeRelConfig.ElecTechCloseReq &&
                            permit.ElecTechCloseRelStatus != ReleaseStatus.FullyReleased)
                            errors.Add("Electric technician close is pending.");

                        if (closeRelConfig.ElecInchargeCloseReq &&
                            permit.ElecInchCloseRelStatus != ReleaseStatus.FullyReleased)
                            errors.Add("Electric incharge close is pending.");
                    }

                    // Release
                    permit.CloseSafetyInchEmpId = data.EmpUnqid;
                    permit.SafetyInchCloseRelStatus = ReleaseStatus.FullyReleased;
                    permit.SafetyInchCloseDate = DateTime.Now;
                    permit.SafetyInchCloseRemarks = data.Remarks;
                    permit.ClosedOn = DateTime.Now;

                    // TODO: Set flags
                    permit.CurrentState = PermitState.Closed;
                    permit.AllowUserEdit = false;
                    permit.AllowSafetyEdit = false;
                    permit.AllowClose = false;

                    break;

                default:
                    break;
            }


            if (errors.Count > 0)
                return BadRequest(errors);

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