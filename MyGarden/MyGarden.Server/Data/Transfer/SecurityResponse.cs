using MyGarden.Server.Entities.Security;

namespace MyGarden.Server.Data.Transfer
{
    public class SecurityResponse
    {
        /// <summary>
        ///     Аккаунт.
        /// </summary>
        public required Account Account { get; init; }

        /// <summary>
        ///     Токен.
        /// </summary>
        public required string Token { get; init; }
    }
}