using GardenAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace GardenAPI.Data
{
    public class PlantDbContext : DbContext
    {
        public DbSet<Plant> Plants { get; set; }

        public PlantDbContext(DbContextOptions<PlantDbContext> dbContextOptions) : base(dbContextOptions) {
            Database.EnsureCreated();
            //try
            //{
            //    var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            //    if (databaseCreator != null)
            //    {
            //        if (!databaseCreator.CanConnect()) databaseCreator.Create();
            //        if (!databaseCreator.HasTables()) databaseCreator.CreateTables();

            //    }
            //}
            //catch (Exception e) 
            //{
            //    Console.WriteLine(e.Message);
            //}
        }
        
    }
}
