using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using ePTW.Dto;
using ePTW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace ePTW.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ObsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private uint GetMaxObsId()
        {
            uint obsId = 0;
            try
            {
                obsId = _context.Observations.Max(e => e.Id);
                obsId++;
            }
            catch (Exception)
            {
                obsId = 1;
            }

            return obsId;
        }

        private ObsDto FillData(ObsDto dto)
        {
            dto.CompanyName = _context.Companies.FirstOrDefault(c => c.CompCode == dto.CompCode)?.CompName;
            dto.UnitName = _context.Units.FirstOrDefault(u =>
                u.CompCode == dto.CompCode &&
                u.WrkGrp == dto.WrkGrp &&
                u.UnitCode == dto.UnitCode)?.UnitName;
            dto.DeptName = _context.Departments.FirstOrDefault(d =>
                d.CompCode == dto.CompCode &&
                d.WrkGrp == dto.WrkGrp &&
                d.UnitCode == dto.UnitCode &&
                d.DeptCode == dto.DeptCode)?.DeptName;
            dto.StatName = _context.Stations.FirstOrDefault(s =>
                s.CompCode == dto.CompCode &&
                s.WrkGrp == dto.WrkGrp &&
                s.UnitCode == dto.UnitCode &&
                s.DeptCode == dto.DeptCode &&
                s.StatCode == dto.StatCode)?.StatName;
            dto.ObservedByName = _context.Employees.FirstOrDefault(e => e.EmpUnqId == dto.ObservedBy)?.EmpName;

            if (dto.ReleaseBy != null)
                dto.ReleaseByName = _context.Employees.FirstOrDefault(e => e.EmpUnqId == dto.ReleaseBy)?.EmpName;

            if (dto.StatusUpdateReleaseBy != null)
                dto.StatusUpdateReleaseByName = _context.Employees
                    .FirstOrDefault(e => e.EmpUnqId == dto.StatusUpdateReleaseBy)?.EmpName;

            return dto;
        }

        private ObsHistory CreateHistory(ObsDto dto, string empUnqId)
        {
            var history = new ObsHistory
            {
                Id = 0,
                ObsId = dto.Id,
                ObsCatg = dto.ObsCatg,
                ObsDate = dto.ObsDate,
                PersonResponsible = dto.PersonResponsible,
                TargetDate = dto.TargetDate,
                ComplianceDate = dto.ComplianceDate,
                ObsStatus = dto.ObsStatus,
                ObservedBy = dto.ObservedBy,
                StatusUpdateDate = dto.StatusUpdateDate,
                ReleaseBy = dto.ReleaseBy,
                ReleaseStatus = dto.ReleaseStatus,
                ReleaseDate = dto.ReleaseDate,
                StatusUpdateReleaseBy = dto.StatusUpdateReleaseBy,
                StatusUpdateReleaseStatus = dto.StatusUpdateReleaseStatus,
                StatusUpdateReleaseDate = dto.StatusUpdateReleaseDate,
                AddDt = DateTime.Now,
                UserId = empUnqId
            };

            return history;
        }

        //get by id for image upload
        [HttpGet("getbyid")]
        public IActionResult GetObservation(int id)
        {
            Observation obs = _context.Observations
                .FirstOrDefault(o => o.Id == id);

            if (obs == null)
                return BadRequest("Observation not found.");

            var obsDto = _mapper.Map<ObsDto>(obs);

            // fill details
            obsDto = FillData(obsDto);

            return Ok(obsDto);
        }

        [HttpGet("getRelease")]
        public IActionResult GetObsForRelease(string empUnqId, bool safety)
        {
            List<Observation> obs;

            if (safety)
            {
                Employee emp = _context.Employees.FirstOrDefault(e => e.EmpUnqId == empUnqId);

                // check in safety dept stat table if employee belongs to safety dept
                bool bFound = _context.SafetyDeptStats
                    .Any(stat =>
                        emp.UnitCode == stat.UnitCode &&
                        emp.DeptCode == stat.DeptCode &&
                        emp.StatCode == stat.StatCode);

                if (!bFound)
                    return BadRequest("Empcode is not safety user");

                obs = _context.Observations
                    .Where(o => o.StatusUpdateReleaseStatus == ReleaseStatus.InRelease)
                    .ToList();
            }
            else
            {
                obs = _context.Observations
                    .Where(o => o.PersonResponsible == empUnqId && o.ReleaseStatus == ReleaseStatus.InRelease)
                    .ToList();
            }

            var obsDto = _mapper.Map<List<ObsDto>>(obs);

            // fill details
            var i = 0;
            foreach (ObsDto dto in obsDto.ToList())
            {
                obsDto[i] = FillData(dto);
                i++;
            }

            return Ok(obsDto);
        }

        [HttpGet("getbyemp")]
        public IActionResult GetObsByEmp(DateTime fromDt, DateTime toDt, string empUnqId)
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

            List<Observation> obs = _context.Observations
                .Where(p =>
                    p.ObsDate >= fromDt && p.ObsDate <= toDt &&
                    emps.Contains(p.ObservedBy))
                .ToList();

            if (obs.Count == 0)
                return BadRequest("No records found!");

            var obsDto = _mapper.Map<List<ObsDto>>(obs);


            // fill details
            var i = 0;
            foreach (ObsDto dto in obsDto.ToList())
            {
                obsDto[i] = FillData(dto);
                i++;
            }

            return Ok(obsDto);
        }

        [HttpGet("getobs")]
        public IActionResult GetObs(DateTime fromDt, DateTime toDt)
        {
            List<Observation> obs = _context.Observations
                .Where(p =>
                    p.ObsDate >= fromDt && p.ObsDate <= toDt)
                .ToList();

            if (obs.Count == 0)
                return BadRequest("No records found!");

            var obsDto = _mapper.Map<List<ObsDto>>(obs);

            // fill details
            var i = 0;
            foreach (ObsDto dto in obsDto.ToList())
            {
                obsDto[i] = FillData(dto);
                i++;
                var obsImage = _context.ObsPhotos.FirstOrDefault(o => o.Id == dto.Id && o.ObsStatus == "O");
                if (obsImage != null) obsDto[i].ObsImage = obsImage.PermitImage;

                var compImage = _context.ObsPhotos.FirstOrDefault(o => o.Id == dto.Id && o.ObsStatus == "C");
                if (compImage != null) obsDto[i].CompImage = compImage.PermitImage;
            }


            return Ok(obsDto);
        }

        [HttpGet("getphoto")]
        public IActionResult GetPhoto(uint id, string status)
        {
            ObsPhotos obsImage = _context.ObsPhotos.FirstOrDefault(o => o.Id == id && o.ObsStatus == status);
            if (obsImage != null) 
                return new FileContentResult(obsImage.PermitImage, MediaTypeHeaderValue.Parse("application/octet-stream"));
            return BadRequest("Photo not found.");
        }

        //create
        [HttpPost("CreateObs")]
        public IActionResult CreateObs([FromBody] object requestData)
        {
            var obs = JsonConvert.DeserializeObject<ObsDto>(requestData.ToString());
            if (obs.Id != 0)
                return BadRequest("Id must be zero for creation.");

            obs.Id = GetMaxObsId();

            var newObs = _mapper.Map<Observation>(obs);

            newObs.ReleaseStatus = ReleaseStatus.InRelease;

            newObs.StatusUpdateReleaseStatus = newObs.ObsStatus == "O"
                ? ReleaseStatus.InRelease
                : ReleaseStatus.NotReleased;

            newObs.CreatedOn = DateTime.Now;

            _context.Observations.Add(newObs);
            _context.SaveChanges();

            return Ok(_mapper.Map<ObsDto>(newObs));
        }

        [HttpPost("UploadImage")]
        public IActionResult UploadImage(uint id, char status)
        {
            bool found = _context.Observations.Any(p => p.Id == id);
            if (!found)
                return BadRequest("Invalid observation id.");

            if (Request.Form.Files.Count <= 0)
                return BadRequest("No files found!");

            foreach (IFormFile formFile in Request.Form.Files)
            {
                if (formFile.Length <= 0) continue;

                Stream stream = formFile.OpenReadStream();

                var image = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(image, 0, image.Length);

                ObsPhotos obsPhoto = _context.ObsPhotos
                    .FirstOrDefault(p => p.Id == id && p.ObsStatus == status.ToString());

                if (obsPhoto == null)
                {
                    obsPhoto = new ObsPhotos { Id = id, ObsStatus = status.ToString(), PermitImage = image };
                    _context.ObsPhotos.Add(obsPhoto);
                }
                else
                {
                    obsPhoto.Id = id;
                    obsPhoto.ObsStatus = status.ToString();
                    obsPhoto.PermitImage = image;
                }

                _context.SaveChanges();
                return Ok();
            }

            return Ok();
        }

        // release by safety or status change by pers resp
        [HttpPut("releaseobs")]
        public IActionResult ReleaseObs([FromBody] object requestData, string empUnqId, bool safety)
        {
            Employee emp = _context.Employees.FirstOrDefault(e => e.EmpUnqId == empUnqId);
            if (emp == null)
                return BadRequest("Invalid employee.");

            var dto = JsonConvert.DeserializeObject<ObsDto>(requestData.ToString());
            if (dto.Id == 0)
                return BadRequest("Invalid ID");

            using (var transaction = _context.Database.BeginTransaction())
            {
                Observation obs = _context.Observations.FirstOrDefault(o => o.Id == dto.Id);
                if (obs == null)
                    return BadRequest("Observation not found with id " + dto.Id);


                //check for safety dept
                if (safety)
                {
                    bool bFound = _context.SafetyDeptStats
                        .Any(stat =>
                            emp.UnitCode == stat.UnitCode &&
                            emp.DeptCode == stat.DeptCode &&
                            emp.StatCode == stat.StatCode);

                    if (!bFound)
                        return BadRequest("Empcode is not safety user");

                    if (obs.ReleaseStatus != ReleaseStatus.FullyReleased)
                        return BadRequest("Release pending from person responsible.");

                    obs.StatusUpdateReleaseStatus = ReleaseStatus.FullyReleased;
                    obs.StatusUpdateDate = DateTime.Now;
                    obs.StatusUpdateReleaseBy = empUnqId;
                    obs.StatusUpdateReleaseRemarks = dto.StatusUpdateReleaseRemarks;
                }
                else
                {
                    if (obs.PersonResponsible != empUnqId)
                        return BadRequest("Invalid person responsible id.");

                    if (obs.ReleaseStatus != ReleaseStatus.InRelease)
                        return BadRequest("Invalid release status, must be 'I'");

                    obs.ObsStatus = dto.ObsStatus;
                    obs.ComplianceDate = dto.ComplianceDate;
                    obs.ReleaseStatus = ReleaseStatus.FullyReleased;
                    obs.ReleaseDate = DateTime.Now;
                    obs.ReleaseBy = empUnqId;
                    obs.ReleaseRemarks = dto.ReleaseRemarks;
                    obs.StatusUpdateReleaseStatus = ReleaseStatus.InRelease;
                }

                ObsHistory history = CreateHistory(dto, empUnqId);

                _context.ObsHistories.Add(history);

                _context.SaveChanges();
                transaction.Commit();
            }

            return Ok();
        }

        [HttpPut("changestatus")]
        public IActionResult ChangeStatus([FromBody] object requestData, string empUnqId, bool safety)
        {
            Employee emp = _context.Employees.FirstOrDefault(e => e.EmpUnqId == empUnqId);
            if (emp == null)
                return BadRequest("Invalid employee.");

            var dto = JsonConvert.DeserializeObject<ObsDto>(requestData.ToString());
            if (dto.Id == 0)
                return BadRequest("Invalid ID");

            using (var transaction = _context.Database.BeginTransaction())
            {
                Observation obs = _context.Observations.FirstOrDefault(o => o.Id == dto.Id);
                if (obs == null)
                    return BadRequest("Observation not found with id " + dto.Id);


                //check for safety dept
                if (safety)
                {
                    bool bFound = _context.SafetyDeptStats
                        .Any(stat =>
                            emp.UnitCode == stat.UnitCode &&
                            emp.DeptCode == stat.DeptCode &&
                            emp.StatCode == stat.StatCode);

                    if (!bFound)
                        return BadRequest("Empcode is not safety user");

                    obs.ObsStatus = dto.ObsStatus;

                    obs.ReleaseStatus = ReleaseStatus.InRelease;
                    obs.StatusUpdateReleaseStatus = ReleaseStatus.InRelease;
                    obs.StatusUpdateReleaseRemarks = dto.StatusUpdateReleaseRemarks;
                }
                else
                {
                    if (obs.PersonResponsible != empUnqId)
                        return BadRequest("Invalid person responsible id.");

                    if (obs.ReleaseStatus != ReleaseStatus.InRelease)
                        return BadRequest("Invalid release status, must be 'I'");

                    obs.ObsStatus = dto.ObsStatus;
                    obs.StatusUpdateReleaseStatus = ReleaseStatus.InRelease;
                }

                ObsHistory history = CreateHistory(dto, empUnqId);

                _context.ObsHistories.Add(history);

                _context.SaveChanges();
            }


            return Ok(dto.Id);
        }
    }
}