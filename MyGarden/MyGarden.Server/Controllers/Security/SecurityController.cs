using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyGarden.Server.Configuration;
using MyGarden.Server.Data.Util;
using MyGarden.Server.Data;
using MyGarden.Server.Entities.Security;
using MyGarden.Server.Middleware.Security.Requirement;
using MyGarden.Server.Service.Security;
using System.Data;
using Microsoft.EntityFrameworkCore;
using MyGarden.Server.Data.Transfer;
using RegisterRequest = MyGarden.Server.Data.Transfer.RegisterRequest;

namespace MyGarden.Server.Controllers.Security
{

    /// <summary>
    ///     Контроллер авторизации аккаунтов.
    /// </summary>
    /// <param name="identityContext">Сессия работы с базой данных безопасности.</param>
    /// <param name="tokenService">Сервис работы с токенами.</param>
    /// <param name="accountService">Сервис работы с аккаунтами.</param>
    /// <param name="roleService">Сервис работы с ролями.</param>
    /// <param name="securityService">Сервис работы с безопасностью.</param>
    /// <param name="userManager">Менеджер работы с пользователями.</param>
    [Route("api/security")]
    [ApiController]
    public class SecurityController(
        IdentityContext identityContext,
        TokenService tokenService,
        AccountService accountService,
        SecurityService securityService,
        UserManager<IdentityUser> userManager
    ) : ControllerBase
    {
        private IdentityContext IdentityContext { get; } = identityContext;
        private TokenService TokenService { get; } = tokenService;
        private AccountService AccountService { get; } = accountService;
        private SecurityService SecurityService { get; } = securityService;
        private UserManager<IdentityUser> UserManager { get; } = userManager;

        /// <summary>
        ///     Получить базовые параметры соединения.
        /// </summary>
        /// <returns>Результат с параметрами.</returns>
        [HttpGet]
        public ActionResult<List<Account>> GetSettings()
        {
            return Ok(new HandshakeResponse
            {
                DefaultPermission = PermissionConfiguration.DefaultPermission,
                CoordinatedUniversalTime = DateTime.UtcNow
            });
        }

        /// <summary>
        ///     Авторизовать пользователя.
        /// </summary>
        /// <param name="request">Тело запроса авторизации.</param>
        /// <returns>Тело ответа модуля безопасности.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<SecurityResponse>> AuthorizeUser(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }

            var user = await UserManager.FindByEmailAsync(request.Email);

            if (user == null || !await UserManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized();
            }

            var accounts = await AccountService.GetByIdentityId([user.Id]);
            var accountIds = accounts.Select(x => x.Id.GetValueOrDefault()).ToList();

            var account = accounts.First();

            return Ok(DataSerializer.Serialize(new SecurityResponse
            {
                Account = account,
                Token = TokenService.CreateNewToken(user)
            }));
        }

        /// <summary>
        ///     Зарегистрировать нового пользователя.
        /// </summary>
        /// <param name="request">Тело запроса регистрации.</param>
        /// <returns>Тело ответа модуля безопасности.</returns>
        /// <exception cref="Exception">При неудачной попытке создания пользователя.</exception>
        [HttpPost("register")]
        public async Task<ActionResult<SecurityResponse>> RegisterNewUser([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }


            return await CreateNewUserInternal(request);
        }

        /// <summary>
        ///     Создать нового пользователя.
        /// </summary>
        /// <param name="request">Тело запроса регистрации.</param>
        /// <returns>Тело ответа модуля безопасности.</returns>
        /// <exception cref="Exception">При неудачной попытке создания пользователя.</exception>
        [HttpPost("user")]
        public async Task<ActionResult<SecurityResponse>> CreateNewUser([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }

            return await CreateNewUserInternal(request);
        }

        /// <summary>
        ///     Выпустить новый токен для текущего авторизованного пользователя.
        /// </summary>
        /// <returns>Тело ответа модуля безопасности.</returns>
        [Authorize(Policy = DefaultAuthorizationRequirement.PolicyCode)]
        [HttpPost("token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (User.Identity is null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var user = await IdentityContext.Users
                .FirstOrDefaultAsync(user => user.UserName == User.Identity.Name);

            if (user is null)
            {
                return Unauthorized();
            }

            var accounts = await AccountService.GetByIdentityId([user.Id]);
            var accountIds = accounts.Select(x => x.Id.GetValueOrDefault()).ToList();

            var account = accounts.First();

            if (account is null)
            {
                return Unauthorized();
            }

            return Ok(DataSerializer.Serialize(new SecurityResponse
            {
                Account = account,
                Token = TokenService.CreateNewToken(user)
            }));
        }

        /// <summary>
        ///     Создать нового пользователя.
        /// </summary>
        /// <param name="request">Тело запроса регистрации.</param>
        /// <returns>Тело ответа модуля безопасности.</returns>
        /// <exception cref="Exception">При неудачной попытке создания пользователя.</exception>
        private async Task<ActionResult<SecurityResponse>> CreateNewUserInternal(
            [FromBody] RegisterRequest request
        )
        {
            var result = await UserManager.CreateAsync(
                new IdentityUser
                {
                    UserName = request.Email,
                    Email = request.Email
                },
                request.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            var user = await IdentityContext.Users.FirstOrDefaultAsync(user => user.Email == request.Email);

            if (user is null)
            {
                throw new Exception("Error while creating new identity user!");
            }

            var status = await AccountService.Set([
                new Account
            {
                IdentityId = user.Id,
                Email = request.Email,
                Name = request.Account?.Name,
                Surname = request.Account?.Surname,
                Patronymic = request.Account?.Patronymic
            }
            ]);

            if (!status)
            {
                throw new Exception("Error while creating new account!");
            }

            var accounts = await AccountService.GetByIdentityId([user.Id]);
            var account = accounts.First();

            if (account is null)
            {
                return Unauthorized();
            }

            return Ok(DataSerializer.Serialize(new SecurityResponse
            {
                Account = account,
                Token = TokenService.CreateNewToken(user)
            }));
        }
    }
}
