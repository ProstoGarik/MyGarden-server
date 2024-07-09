using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using MyGarden.Server.Data.Configuration;
using System.Data;
using System.Xml.Linq;
using MyGarden.Server.Entity;
using MyGarden.Server.Entity.Events;
using MyGarden.Server.Entity.Common;
using MyGarden.Server.Entities;
using MyGarden.Server.Entities.Events;

namespace MyGarden.Server.Data
{
    /// <summary>
    ///     Сессия работы с базой данных.
    ///     Памятка для работы с кешем:
    ///     - context.Add() для запроса INSERT.
    ///     Объекты вставляются со статусом Added.
    ///     При коммите изменений произойдет попытка вставки.
    ///     - context.Update() для UPDATE.
    ///     Объекты вставляются со статусом Modified.
    ///     При коммите изменений произойдет попытка обновления.
    ///     - context.Attach() для вставки в кеш.
    ///     Объекты вставляются со статусом Unchanged.
    ///     При коммите изменений ничего не произойдет.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    public class DataContext(ContextConfiguration configuration) : DbContext
    {
        private ContextConfiguration Configuration { get; } = configuration;

        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Plant> Plants => Set<Plant>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<GrowStage> GrowStages => Set<GrowStage>();
        public DbSet<LightNeed> LightNeeds => Set<LightNeed>();
        public DbSet<WateringNeed> WateringNeeds => Set<WateringNeed>();

        /// <summary>
        ///     Попытаться инициализировать сессию.
        ///     Используется для проверки подключения
        ///     и инициализации структуры таблиц.
        /// </summary>
        /// <returns>Статус успешности инициализации.</returns>
        public bool TryInitialize()
        {
            var canConnect = Database.CanConnect();
            var isCreated = Database.EnsureCreated();

            return canConnect || isCreated;
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
        ///     Обработать инициализацию модели.
        ///     Используется для дополнительной настройки модели.
        /// </summary>
        /// <param name="modelBuilder">Набор интерфейсов настройки модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Безопасность.
            modelBuilder.ApplyConfiguration(new Group.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new Plant.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new Event.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new Notification.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new GrowStage.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new LightNeed.Configuration(Configuration));
            modelBuilder.ApplyConfiguration(new WateringNeed.Configuration(Configuration));

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        ///     Сохранить все изменения сессии в базу данных.
        ///     Используется для обновления метаданных модели.
        /// </summary>
        /// <returns>Количество затронутых записей.</returns>
        public override int SaveChanges()
        {
            UpdateTrackedEntityMetadata();

            return base.SaveChanges();
        }

        /// <summary>
        ///     Асинхронно сохранить все изменения сессии в базу данных.
        ///     Используется для обновления метаданных модели.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Флаг принятия всех изменений при успехе операции.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Таск, представляющий операцию асинхронного сохранения с количеством затронутых записей.</returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            UpdateTrackedEntityMetadata();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        ///     Обновить метаданные всех модифицироанных моделей
        ///     в кеше сессии.
        ///     Такой подход накладывает дополнительные ограничения
        ///     при работе с сессиями. Необходимо учитывать, что
        ///     для обновления записей нужно сперва загрузить эти
        ///     записи в кеш сессии, чтобы трекер корректно
        ///     зафиксировал изменения.
        /// </summary>
        private void UpdateTrackedEntityMetadata()
        {
            var entries = ChangeTracker.Entries().Where(IsModifiedEntity);

            foreach (var entry in entries)
            {
                if (entry.Entity is not Entities.Entity entity)
                {
                    continue;
                }

                // Текущая дата и время на устройстве.
                // Нельзя допустить, чтобы эти данные передавались во внешние хранилища.
                entity.UpdatedAt = DateTime.Now.ToUniversalTime();
            }
        }

        /// <summary>
        ///     Определить, является ли вхождение трекера обновленной моделью.
        /// </summary>
        /// <param name="entry">Вхождение из трекера изменений.</param>
        /// <returns>Статус проверки.</returns>
        private static bool IsModifiedEntity(EntityEntry entry)
        {
            return entry is
            {
                Entity: Entities.Entity, State: EntityState.Modified
            };
        }
    }
}
