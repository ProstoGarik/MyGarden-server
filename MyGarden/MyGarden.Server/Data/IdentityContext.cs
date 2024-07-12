using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyGarden.Server.Data.Configuration;

namespace MyGarden.Server.Data
{

    /// <summary>
    ///     Контекст данных идентификации.
    /// </summary>
    /// <param name="configuration">Менеджер конфигурации.</param>
    public class IdentityContext(ContextConfiguration configuration) : IdentityDbContext<IdentityUser>
    {
        private ContextConfiguration Configuration { get; } = configuration;

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

            if (!isCreated)
            {
                Database.OpenConnection();
            }

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

            if (!isCreated)
            {
                await Database.OpenConnectionAsync();
            }

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
    }
}
