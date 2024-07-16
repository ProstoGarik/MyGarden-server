namespace MyGarden.Server.Data.Transfer
{

    /// <summary>
    ///     Тело запроса модуля безопасности.
    /// </summary>
    public class SecurityRequest
    {
        /// <summary>
        ///     Почта.
        /// </summary>
        public required string Email { get; init; }

        /// <summary>
        ///     Пароль.
        /// </summary>
        public required string Password { get; init; }
    }
}
