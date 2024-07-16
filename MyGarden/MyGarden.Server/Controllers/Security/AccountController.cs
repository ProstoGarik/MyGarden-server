using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGarden.Server.Entities.Security;
using MyGarden.Server.Service.Security;

namespace MyGarden.Server.Controllers.Security
{
    [Route("api/security/account")]
    [ApiController]
    public class AccountController(AccountService accountService) : ControllerBase
    {
        private AccountService AccountService { get; } = accountService;

        /// <summary>
        ///     Получить список аккаунтов.
        ///     Если идентификаторы не указаны, возвращается список со всеми аккаунтами.
        ///     Иначе возвращается список с указанными аккаунтами, либо пустой список.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Результат операции со списком аккаунтов.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get([FromQuery] List<int> ids)
        {
            return await AccountService.Get(AccountService.DataContext.Accounts, ids);
        }


        /// <summary>
        ///     Сохранить аккаунты.
        /// </summary>
        /// <param name="entities">Список аккаунтов.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<Account> entities)
        {
            var status = await AccountService.Set(entities);

            if (!status)
            {
                return BadRequest("No accounts were saved!");
            }

            return Ok();
        }

    }
}
