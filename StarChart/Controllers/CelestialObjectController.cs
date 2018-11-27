using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var foundObject = _context.CelestialObjects.Where(c => c.Id == id).FirstOrDefault();

            if (foundObject == null)
                return NotFound();
            else
            {
                foundObject.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();
                return Ok(foundObject);
            }
        }
    }
}
