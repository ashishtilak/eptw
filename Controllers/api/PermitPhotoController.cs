using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using ePTW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using MediaTypeHeaderValue = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace ePTW.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitPhotoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PermitPhotoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPhoto(int id)
        {
            List<PermitPhotos> permit = _context.PermitPhotos.Where(p => p.Id == id).ToList();
            if (permit.Count == 0)
                return BadRequest("No photos!");

            foreach (PermitPhotos permitPhoto in permit)
            {
                return new FileContentResult(permitPhoto.PermitImage, MediaTypeHeaderValue.Parse("application/octet-stream"));
            }

            return BadRequest("No photos!");
        }

        [HttpPost]
        public IActionResult PostPhoto(uint id)
        {
            bool found = _context.Permits.Any(p => p.Id == id);
            if (!found)
                return BadRequest("Invalid permit id.");

            if(Request.Form.Files.Count <= 0 )
                return BadRequest("No files found!");

            foreach (IFormFile formFile in Request.Form.Files)
            {
                if (formFile.Length <= 0) continue;
                
                Stream stream = formFile.OpenReadStream();
                
                var image = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(image, 0, image.Length);

                PermitPhotos permitPhoto = _context.PermitPhotos
                    .FirstOrDefault(p=> p.Id == id);

                if(permitPhoto == null)
                {
                    permitPhoto = new PermitPhotos {Id = id, Sr = 1, PermitImage = image};
                    _context.PermitPhotos.Add(permitPhoto);
                }
                else
                {
                    permitPhoto.Id = id;
                    permitPhoto.Sr = 1;
                    permitPhoto.PermitImage = image;
                }
                _context.SaveChanges();
                return Ok();
            }

            return Ok();
        }
    }
}