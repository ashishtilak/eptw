using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using ePTW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;

namespace ePTW.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomailController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

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


        public AutomailController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult SendMail(uint id, string mode)
        {
            try
            {
                Permit permit = _context.Permits
                    .Where(p => p.Id == id)
                    .Include(p => p.Departments)
                    .Include(p => p.Stations)
                    .Include(p => p.PermitType)
                    .Include(p => p.CrossRefs)
                    .Include(p => p.PermitPersons)
                    .Include(p => p.HeightPermit)
                    .Include(p => p.HotWorkPermit)
                    .Include(p => p.ElecIsolationPermit)
                    .Include(p => p.ColdWorkPermit)
                    .Include(p => p.VesselEntryPermit)
                    .FirstOrDefault();

                if (permit == null)
                    return BadRequest("Invalid permit.");

                // get details from locations table

                var location = _context.Locations.FirstOrDefault();
                if(location == null)
                    return BadRequest("Location not configured.");

                var smtpServer = location.SmtpClient;
                var mailAddress = location.MailAddress;

                var empname = _context.Employees.FirstOrDefault(e=>e.EmpUnqId == permit.CreatedByEmpId)?.EmpName;

                const string header = @"
                <html lang=""en"">
                    <head>    
                        <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
                        <title>
                            e-PTW Portal - automail
                        </title>
                        <style type=""text/css"">
                            body { font-family: arial, sans-serif; }
                            table {
                                font-family: arial, sans-serif;
                                border-collapse: collapse;
                                width: 80%;
                            }

                            td, th {
                                border: 1px solid #dddddd;
                                text-align: left;
                                padding: 8px;
                            }

                            tr:nth-child(even) {
                                background-color: #dddddd;
                            }
                        </style>
                    </head>
                    <body>
                ";

                bool isDev = Startup.ConnectionString.Contains("ash");

                string sentTo = mode switch
                {
                    Modes.AreaInch => _context.Employees.FirstOrDefault(e => e.EmpUnqId == permit.AreaInchargeEmpId)
                        ?.Email,
                    Modes.DeptInch => _context.Employees.FirstOrDefault(e => e.EmpUnqId == permit.DeptInchEmpId)
                        ?.Email,
                    Modes.ElecInch => _context.Employees.FirstOrDefault(e => e.EmpUnqId == permit.ElecInchargeEmpId)
                        ?.Email,
                    Modes.FinalRelease => "IPUGroup.Safety@jindalsaw.com",
                    _ => ""
                };

                if(isDev && mode == Modes.FinalRelease)
                {
                    sentTo = "mohit.parmar@jindalsaw.com";
                }

                string body = "Dear Sir, <br /><br /> " +
                              "Following permit requires your attention: <br/> <br />" +
                              "Permit Number: " + permit.PermitNo + " <br/>" +
                              "Permit date: " + permit.FromDt?.ToString("yyyy-MM-dd") + " <br/> " +
                              "Permit Type: " + permit.PermitType.PermitTypeDesc + " <br/>" +
                              "Dept/Station: " + permit.Departments.DeptName + "/" + permit.Stations.StatName +
                              " <br />" +
                              "Job Description: " + permit.JobDescription + " <br />" +
                              "Location: " + permit.WorkLocation + " <br />" +
                              "Created by: " + permit.CreatedByEmpId + ": " + empname + 
                              "<br/></br>";

                body = header + body;
                 

                var smtpClient = new SmtpClient(smtpServer, 25)
                {
                    UseDefaultCredentials = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = false
                };

                string strSubject = "ePTW Notification for " + permit.PermitType.PermitTypeDesc + " Permit No " +
                                    permit.PermitNo + " from " + permit.Departments.DeptName + " / " +
                                    permit.Stations.StatName;

                var mail = new MailMessage
                {
                    From = new MailAddress(mailAddress, "ePTW Portal"),
                    Subject = strSubject,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true,
                    Body = body
                };

                if(!string.IsNullOrEmpty(sentTo))
                {
                    mail.To.Add(new MailAddress(sentTo));
                    smtpClient.Send(mail);
                    mail.To.Remove(new MailAddress(sentTo));
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex);
            }

            return Ok();
        }
    }
}