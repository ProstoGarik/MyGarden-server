using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyGarden.Server.Data;
using MyGarden.Server.Entities.Security;

namespace MyGarden.Server.Service.Security
{

    /// <summary>
    ///     Сервис для работы с <see cref="Account" />.
    /// </summary>
    /// <param name="dataContext">Контекст данных.</param>
    /// <param name="userManager">Менеджер пользователей.</param>
    public class AccountService(
        DataContext dataContext,
        UserManager<IdentityUser> userManager
    ) : DataEntityService(dataContext)
    {
        private UserManager<IdentityUser> UserManager { get; } = userManager;


        /// <summary>
        ///     Получить список аккаунтов по идентификаторам пользователей.
        /// </summary>
        /// <param name="identityIds">Идентификаторы пользователей.</param>
        /// <returns>Список аккаунтов.</returns>
        /// <exception cref="ArgumentException">При пустом списке идентификаторов.</exception>
        public async Task<List<Account>> GetByIdentityId(List<string> identityIds)
        {
            if (identityIds.Count <= 0)
            {
                throw new ArgumentException("Invalid arguments!");
            }

            return await DataContext.Accounts
                .Where(account => identityIds.Contains(account.IdentityId!))
                .ToListAsync();
        }

        /// <summary>
        ///     Сохранить аккаунты.
        /// </summary>
        /// <param name="accounts">Аккаунты.</param>
        /// <returns>Статус операции.</returns>
        public async Task<bool> Set(List<Account> accounts)
        {
            var identityIds = accounts
                .Where(account => !string.IsNullOrEmpty(account.IdentityId))
                .Select(account => account.IdentityId)
                .ToList();
            var sameUsers = await DataContext.Accounts
                .Where(account => identityIds.Contains(account.IdentityId))
                .ToListAsync();

            if (HasSameIdentities(sameUsers, accounts))
            {
                // Попытка создать аккаунт пользователю,
                // который уже имеет аккаунт.
                return false;
            }

            return await Set(DataContext.Accounts, accounts);
        }

        /// <summary>
        ///     Удалить аккаунты.
        /// </summary>
        /// <param name="accounts">Идентификаторы.</param>
        /// <returns>Статус операции.</returns>
        public async Task<bool> Remove(List<int> ids)
        {
            var identityIds = await DataContext.Accounts
                .Where(account => ids.Contains(account.Id.GetValueOrDefault()))
                .Select(account => account.IdentityId)
                .ToListAsync();
            var users = await UserManager.Users
                .Where(user => identityIds.Contains(user.Id))
                .ToListAsync();

            foreach (var identityId in identityIds)
            {
                if (string.IsNullOrEmpty(identityId))
                {
                    continue;
                }

                var user = users.Where(user => user.Id == identityId).FirstOrDefault();

                if (user is not null)
                {
                    // Дополнительно удаляем связанных пользователей.
                    await UserManager.DeleteAsync(user);
                }
            }

            return await Remove(DataContext.Accounts, ids);
        }

        /// <summary>
        ///     Проверить, что аккаунты для сохранения в базу данных
        ///     не противоречат существующим аккаунтам пользователей.
        ///     Проверяется соответствие идентификаторов аккаунтов
        ///     и идентификаторов пользователей.
        /// </summary>
        /// <param name="existingAccounts">Существующие аккаунты.</param>
        /// <param name="accountsToSave">Аккаунта для сохранения.</param>
        /// <returns>Статус проверки.</returns>
        private static bool HasSameIdentities(List<Account> existingAccounts, List<Account> accountsToSave)
        {
            foreach (var account in existingAccounts)
            {
                var identityId = account.IdentityId;

                if (string.IsNullOrEmpty(identityId))
                {
                    continue;
                }

                var sample = accountsToSave.First(x => x.IdentityId == identityId);

                if (sample is null || sample.Id == account.Id)
                {
                    continue;
                }

                return true;
            }

            return false;
        }
    }
}
