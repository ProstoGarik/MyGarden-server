using MyGarden.Server.Entities.Security;

namespace MyGarden.Server.Data.Transfer
{
    /// <summary>
    ///     Тело запроса регистрации.
    /// </summary>
    public class RegisterRequest : SecurityRequest
    {
        /// <summary>
        ///     Аккаунт.
        /// </summary>
        public required Account Account { get; init; }
    }
}
