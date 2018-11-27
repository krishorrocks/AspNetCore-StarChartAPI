using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

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

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var foundObjects = _context.CelestialObjects.Where(c => c.Name == name).ToList();

            if (foundObjects.Count == 0)
                return NotFound();
            else
            {
                foreach(CelestialObject c in foundObjects)
                {
                    c.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == c.Id).ToList();
                }
                return Ok(foundObjects);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var foundObjects = _context.CelestialObjects.ToList();

            foreach (CelestialObject c in foundObjects)
            {
                c.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == c.Id).ToList();
            }

            return Ok(_context.CelestialObjects.ToList());
        }
    }
}
