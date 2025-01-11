using Microsoft.EntityFrameworkCore;

namespace GardenAPI.Data
{
    public abstract class BaseConfiguration
    {
        internal abstract string DateTimeType { get; }

        internal abstract string DateTimeValueCurrent { get; }

        public abstract void ConfigureContext(DbContextOptionsBuilder optionsBuilder);
    }
}
