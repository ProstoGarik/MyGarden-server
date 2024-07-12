
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyGarden.Server.Configuration.Repository;
using MyGarden.Server.Data;
using MyGarden.Server.Entities.Security;
using ConfigurationManager = MyGarden.Server.Configuration.ConfigurationManager;

namespace MyGarden.Server.Service.Security
{

    /// <summary>
    ///     Сервис работы с безопасностью.
    /// </summary>
    /// <param name="configurationManager">Менеджер конфигурации.</param>
    /// <param name="dataContext">Контекст данных.</param>
    /// <param name="identityContext">Контекст данных идентификации.</param>
    /// <param name="accountService">Сервис работы с ролями.</param>
    /// <param name="userManager">Менеджер пользователей.</param>
    public class SecurityService(
        ConfigurationManager configurationManager,
        DataContext dataContext,
        IdentityContext identityContext,
        UserManager<IdentityUser> userManager
    )
    {
        private SecurityConfiguration SecurityConfiguration { get; } = configurationManager.SecurityConfiguration;
        private DataContext DataContext { get; } = dataContext;
        private IdentityContext IdentityContext { get; } = identityContext;
        private UserManager<IdentityUser> UserManager { get; } = userManager;

        /// <summary>
        ///     Создать пользователя по-умолчанию.
        /// </summary>
        /// <returns>Операция.</returns>
        /// <exception cref="Exception">При ошибке создания.</exception>
        public async Task CreateDefaultUser()
        {
            var userEmail = SecurityConfiguration.GetDefaultUserEmail();
            var userPassword = SecurityConfiguration.GetDefaultUserPassword();
            var user = await IdentityContext.Users
                .FirstOrDefaultAsync(user => user.Email == userEmail);

            if (user is null)
            {
                var result = await UserManager.CreateAsync(
                    new IdentityUser
                    {
                        UserName = userEmail,
                        Email = userEmail
                    },
                    userPassword);

                if (!result.Succeeded)
                {
                    throw new Exception("Data initialization failed while creating default user!");
                }

                user = await IdentityContext.Users
                    .FirstOrDefaultAsync(user => user.Email == userEmail);
            }

            if (user is null)
            {
                throw new Exception("Data initialization failed while creating default user!");
            }

            var accountName = SecurityConfiguration.GetDefaultAccountName();
            var accountSurname = SecurityConfiguration.GetDefaultAccountSurname();
            var accountPatronymic = SecurityConfiguration.GetDefaultAccountPatronymic();

            if (!await DataContext.Accounts.AnyAsync(account => account.IdentityId == user.Id))
            {
                await DataContext.Accounts.AddAsync(new Account
                {
                    IdentityId = user.Id,
                    Email = userEmail,
                    Name = accountName,
                    Surname = accountSurname,
                    Patronymic = accountPatronymic
                });

                await DataContext.SaveChangesAsync();
            }
        }
    }
}
