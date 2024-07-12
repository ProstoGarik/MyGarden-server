using Microsoft.AspNetCore.Authorization;

namespace MyGarden.Server.Middleware.Security.Requirement
{
    /// <summary>
    ///     Требование авторизации для доступов.
    /// </summary>
    public interface IPermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        ///     Получить требуемые доступы.
        /// </summary>
        /// <returns>Список доступов.</returns>
        public IEnumerable<string> GetRequiredPermissions();
    }
}
