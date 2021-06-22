using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePTW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePTW.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SyncController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult SyncDb(string location)
        {
            try
            {
                Helper.Helper.Sync(location);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex);
            }

            return Ok("Done...");
        }

    }
}
