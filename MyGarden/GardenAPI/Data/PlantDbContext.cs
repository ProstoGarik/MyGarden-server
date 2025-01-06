using GardenAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenAPI.Data
{
    public class PlantDbContext : DbContext
    {
        public DbSet<Plant> Plants { get; set; }

        public PlantDbContext(DbContextOptions<PlantDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

    }
}
