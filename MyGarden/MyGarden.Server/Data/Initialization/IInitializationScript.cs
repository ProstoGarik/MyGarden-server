namespace MyGarden.Server.Data.Initialization
{
    /// <summary>
    ///     Скрипт инициализации контекста данных.
    /// </summary>
    public interface IInitializationScript
    {
        /// <summary>
        ///     Запустить инициализацию контекста данных.
        /// </summary>
        /// <returns>Операция.</returns>
        public Task Run();
    }
}
