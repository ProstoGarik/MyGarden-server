using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MyGarden.Server.Data.Configuration
{

    /// <summary>
    ///     Конфигурация контекста для работы с базой данных.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <param name="isDebugMode">Статус конфигурации для разработки.</param>
    public abstract class ContextConfiguration(string connectionString, bool isDebugMode)
    {
        protected string ConnectionString { get; } = connectionString;
        private bool IsDebugMode { get; } = isDebugMode;

        /// <summary>
        ///     Тип полей даты и времени в базе данных.
        /// </summary>
        internal abstract string DateTimeType { get; }

        /// <summary>
        ///     Указатель использования текущих даты и времени
        ///     для полей типа <see cref="DateTimeType" /> в базе данных.
        /// </summary>
        internal abstract string DateTimeValueCurrent { get; }

        /// <summary>
        ///     Применить настройки к сессии.
        /// </summary>
        /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
        public virtual void ConfigureContext(DbContextOptionsBuilder optionsBuilder)
        {
            if (!IsDebugMode) return;

            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.ConfigureWarnings(builder => builder.Throw(RelationalEventId.MultipleCollectionIncludeWarning));

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
