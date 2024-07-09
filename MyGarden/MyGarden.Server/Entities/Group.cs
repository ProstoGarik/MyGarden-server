using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGarden.Server.Data.Configuration;
using MyGarden.Server.Entity.Events;

namespace MyGarden.Server.Entities
{
    public class Group : IdentifiableEntity
    {
        /*                   __ _                       _   _
        *   ___ ___  _ __  / _(_) __ _ _   _ _ __ __ _| |_(_) ___  _ __
        *  / __/ _ \| '_ \| |_| |/ _` | | | | '__/ _` | __| |/ _ \| '_ \
        * | (_| (_) | | | |  _| | (_| | |_| | | | (_| | |_| | (_) | | | |
        *  \___\___/|_| |_|_| |_|\__, |\__,_|_|  \__,_|\__|_|\___/|_| |_|
        *                        |___/
        * Константы, задающие базовые конфигурации полей
        * и ограничения модели.
        */

        #region Configuration

        public const bool IsTitleRequired = true;
        public const int TitleLengthMax = 256;

        /// <summary>
        ///     Конфигурация модели <see cref="Subject" />.
        /// </summary>
        /// <param name="configuration">Конфигурация базы данных.</param>
        internal class Configuration(ContextConfiguration configuration) : Configuration<Group>(configuration)
        {
            /// <summary>
            ///     Задать конфигурацию для модели.
            /// </summary>
            /// <param name="builder">Набор интерфейсов настройки модели.</param>
            public override void Configure(EntityTypeBuilder<Group> builder)
            {
                builder.Property(group => group.Title)
                    .HasMaxLength(TitleLengthMax)
                    .IsRequired(IsTitleRequired);

                base.Configure(builder);
            }
        }

        #endregion


        public string? Title { get; set; }


        public List<Plant> Plants { get; set; } = [];
    }
}
