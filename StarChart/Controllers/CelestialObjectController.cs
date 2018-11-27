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

        [HttpPost]
        public IActionResult Create([FromBody]CelestialObject c)
        {
            _context.CelestialObjects.Add(c);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = c.Id }, c);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject c)
        {
            var foundObject = _context.CelestialObjects.Where(x => x.Id == id).FirstOrDefault();

            if (foundObject == null)
                return NotFound();
            else
            {
                foundObject.Name = c.Name;
                foundObject.OrbitalPeriod = c.OrbitalPeriod;
                foundObject.OrbitedObjectId = c.OrbitedObjectId;

                _context.Update(foundObject);
                _context.SaveChanges();

                return NoContent();
            }
        }

        [HttpPatch("{id}/name")]
        public IActionResult RenameObject(int id, string name)
        {
            var findResult = GetById(id);

            if(findResult is NotFoundResult)
            {
                return NotFound();
            }
            else
            {
                var foundObject = (CelestialObject)(findResult as OkObjectResult).Value;
                foundObject.Name = name;

                _context.Update(foundObject);
                _context.SaveChanges();

                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var foundObjects = _context.CelestialObjects.Where(c => c.Id == id || c.OrbitedObjectId == id).ToList();

            if (foundObjects.Count == 0)
                return NotFound();
            else
            {
                _context.CelestialObjects.RemoveRange(foundObjects);
                _context.SaveChanges();

                return NoContent();
            }
        }
    }
}
