using MyGarden.Server.Service.Security;

namespace MyGarden.Server.Data.Initialization
{

    /// <summary>
    ///     Скрипт инициализации контекста данных.
    /// </summary>
    /// <param name="identityContext">Контекст данных идентификации.</param>
    /// <param name="securityService">Сервис работы с безопасностью.</param>
    class IdentityInitializationScript(
        IdentityContext identityContext,
        SecurityService securityService
    ) : IInitializationScript
    {
        private IdentityContext IdentityContext { get; } = identityContext;
        private SecurityService SecurityService { get; } = securityService;

        /// <summary>
        ///     Запустить инициализацию контекста данных.
        /// </summary>
        /// <returns>Операция.</returns>
        /// <exception cref="Exception">При ошибке инициализации.</exception>
        public async Task Run()
        {
            if (!await IdentityContext.TryInitializeAsync())
            {
                throw new Exception("Data initialization failed!");
            }

            await SecurityService.CreateDefaultUser();
        }
    }
}
