namespace MyGarden.Server.Data.Initialization
{

    /// <summary>
    ///     Скрипт инициализации контекста данных.
    /// </summary>
    /// <param name="dataContext">Контекст данных.</param>
    class DataInitializationScript(DataContext dataContext) : IInitializationScript
    {
        private DataContext DataContext { get; } = dataContext;

        /// <summary>
        ///     Запустить инициализацию контекста данных.
        /// </summary>
        /// <returns>Операция.</returns>
        /// <exception cref="Exception">При ошибке инициализации.</exception>
        public async Task Run()
        {
            if (!await DataContext.TryInitializeAsync())
            {
                throw new Exception("Data initialization failed!");
            }
        }
    }
}
