using GardenAPI.Entities;
using GardenAPI.Entities.Events;
using Microsoft.EntityFrameworkCore;
using MyGarden.Server.Entity.Common;

namespace GardenAPI.Data
{
    public class DataContext(ContextConfiguration configuration) : DbContext
    {
        private ContextConfiguration Configuration { get; } = configuration;

        /// <summary>
        ///     Обработать настройку сессии.
        /// </summary>
        /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
        /// <exception cref="Exception">При ошибке подключения.</exception>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Configuration.ConfigureContext(optionsBuilder);

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        ///     Попытаться асинхронно инициализировать сессию.
        ///     Используется для проверки подключения
        ///     и инициализации структуры таблиц.
        /// </summary>
        /// <returns>Статус успешности инициализации.</returns>
        public async Task<bool> TryInitializeAsync()
        {
            var canConnect = await Database.CanConnectAsync();
            var isCreated = await Database.EnsureCreatedAsync();

            return canConnect || isCreated;
        }

        /// <summary>
        ///     Обработать инициализацию модели.
        ///     Используется для дополнительной настройки модели.
        /// </summary>
        /// <param name="modelBuilder">Набор интерфейсов настройки модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Group.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new Plant.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new Event.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new Notification.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new GrowStage.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new LightNeed.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new WateringNeed.Configuration(Configuration));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Plant> Plants => Set<Plant>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<GrowStage> GrowStages => Set<GrowStage>();
        public DbSet<LightNeed> LightNeeds => Set<LightNeed>();
        public DbSet<WateringNeed> WateringNeeds => Set<WateringNeed>();
    }
}




