using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StarChart.Models
{
    public class CelestialObject
    {
        private int id;

        private int? orbitedObjectId;

        private List<CelestialObject> satellites;

        private TimeSpan orbitalPeriod;

        public int Id { get => id; set => id = value; }

        [Required]
        public string Name { get; set; }

        [NotMapped]
        public List<CelestialObject> Satellites { get => satellites; set => satellites = value; }

        public TimeSpan OrbitalPeriod { get => orbitalPeriod; set => orbitalPeriod = value; }
        public int? OrbitedObjectId { get => orbitedObjectId; set => orbitedObjectId = value; }
    }
}
