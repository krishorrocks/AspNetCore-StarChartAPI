using Microsoft.EntityFrameworkCore;
using StarChart.Models;

namespace StarChart.Data
{
    public class ApplicationDbContext : DbContext
    {
        private DbSet<CelestialObject> celestialObjects;

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CelestialObject> CelestialObjects { get => celestialObjects; set => celestialObjects = value; }
    }
}
