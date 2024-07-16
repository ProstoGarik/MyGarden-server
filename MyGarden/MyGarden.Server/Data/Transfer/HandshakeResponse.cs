namespace MyGarden.Server.Data.Transfer
{
    internal class HandshakeResponse
    {
        /// <summary>
        ///     Доступ по-умолчанию.
        /// </summary>
        public required string DefaultPermission { get; init; }
        /// <summary>
        ///     Дата и время на сервере.
        /// </summary>
        public required DateTime CoordinatedUniversalTime { get; init; }
    }
}