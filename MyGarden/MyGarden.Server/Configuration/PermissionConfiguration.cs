namespace MyGarden.Server.Configuration
{

    /// <summary>
    ///     Статичная конфигурация доступов.
    /// </summary>
    public static class PermissionConfiguration
    {
        /// <summary>
        ///     Идентификатор доступа по-умолчанию.
        /// </summary>
        public const string DefaultPermission = "ru.arkham.permission.default";

        /// <summary>
        ///     Получить доступные идентификаторы доступа.
        /// </summary>
        /// <returns>Список идентификаторов доступа.</returns>
        public static List<string> GetPermissions()
        {
            return
            [
                DefaultPermission,
            ];
        }
    }
}
