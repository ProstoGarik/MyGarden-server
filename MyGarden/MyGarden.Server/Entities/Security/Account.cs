using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGarden.Server.Data.Configuration;

namespace MyGarden.Server.Entities.Security
{

    /// <summary>
    ///     Аккаунт пользователя.
    /// </summary>
    public class Account : IdentifiableEntity
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

        public const int IdentityIdLengthMax = 128;
        public const int EmailLengthMax = 64;
        public const int NameLengthMax = 32;
        public const int SurnameLengthMax = 32;
        public const int PatronymicLengthMax = 32;
        public const bool IsGroupIdRequired = false;
        public const bool IsIdentityIdRequired = false;
        public const bool IsEmailRequired = true;
        public const bool IsNameRequired = false;
        public const bool IsSurnameRequired = false;
        public const bool IsPatronymicRequired = false;

        /// <summary>
        ///     Конфигурация модели <see cref="Account" />.
        /// </summary>
        /// <param name="configuration">Конфигурация базы данных.</param>
        internal class Configuration(ContextConfiguration configuration) : Configuration<Account>(configuration)
        {
            /// <summary>
            ///     Задать конфигурацию для модели.
            /// </summary>
            /// <param name="builder">Набор интерфейсов настройки модели.</param>
            public override void Configure(EntityTypeBuilder<Account> builder)
            {
                builder.Property(account => account.IdentityId)
                    .HasMaxLength(IdentityIdLengthMax)
                    .IsRequired(IsIdentityIdRequired);

                builder.Property(account => account.Email)
                    .HasMaxLength(EmailLengthMax)
                    .IsRequired();

                builder.Property(account => account.Name)
                    .HasMaxLength(NameLengthMax)
                    .IsRequired(IsNameRequired);

                builder.Property(account => account.Surname)
                    .HasMaxLength(SurnameLengthMax)
                    .IsRequired(IsSurnameRequired);

                builder.Property(account => account.Patronymic)
                    .HasMaxLength(PatronymicLengthMax)
                    .IsRequired(IsPatronymicRequired);

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
        ///     Идентификатор в системе безопасности.
        ///     Необязательное поле.
        /// </summary>
        public string? IdentityId { get; set; }

        /// <summary>
        ///     Почта.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        ///     Имя.
        ///     Необязательное поле.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     Фамилия.
        ///     Необязательное поле.
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        ///     Отчество.
        ///     Необязательное поле.
        /// </summary>
        public string? Patronymic { get; set; }

        #endregion
    }
}
