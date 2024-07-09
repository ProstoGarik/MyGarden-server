using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGarden.Server.Data.Configuration;
using MyGarden.Server.Entities;

namespace MyGarden.Server.Entity.Common
{
    public class LightNeed : IdentifiableEntity
    {
        //None,
        //Low,
        //Medium,
        //High

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

        public const int TitleLengthMax = 256;
        public const bool IsTitleRequired = false;

        /// <summary>
        ///     Конфигурация модели <see cref="Subject" />.
        /// </summary>
        /// <param name="configuration">Конфигурация базы данных.</param>
        internal class Configuration(ContextConfiguration configuration) : Configuration<LightNeed>(configuration)
        {
            /// <summary>
            ///     Задать конфигурацию для модели.
            /// </summary>
            /// <param name="builder">Набор интерфейсов настройки модели.</param>
            public override void Configure(EntityTypeBuilder<LightNeed> builder)
            {
                builder.Property(wateringNeed => wateringNeed.Title)
                    .HasMaxLength(TitleLengthMax)
                    .IsRequired(IsTitleRequired);
                base.Configure(builder);
            }
        }

        #endregion

        /*             _   _ _
         *   ___ _ __ | |_(_) |_ _   _
         *  / _ \ '_ \| __| | __| | | |
         * |  __/ | | | |_| | |_| |_| |
         *  \___|_| |_|\__|_|\__|\__, |
         *                       |___/
         * Поля данных, соответствующие таковым в таблице
         * модели в базе данных.
         */

        #region Entity

        /// <summary>
        ///     Название уровня потребности растения в свете.
        /// </summary>
        public string? Title { get; set; }

        #endregion


        public List<Plant> Plants { get; set; } = [];
    }
}
