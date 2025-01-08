using GardenAPI.Data;
using GardenAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GardenAPI.Service
{
    public class HasUserIdEntityService(DataContext dataContext) : DataEntityService(dataContext)
    {
        /// <summary>
        ///     Получить модели по списку идентификаторов.
        /// </summary>
        /// <param name="dbSet">Набор объектов <see cref="DbSet{TEntity}" />.</param>
        /// <param name="ids">Список идентификаторов.</param>
        /// <typeparam name="TSource">Тип модели.</typeparam>
        /// <returns>Список моделей.</returns>
        public async Task<List<TSource>> Get<TSource>(DbSet<TSource> dbSet, string userId, List<int>? ids = null) where TSource:IdentifiableEntity,IHasUserId
        {
            ids ??= [];
            if (ids.Count <= 0)
            {
                return await dbSet.Where(entity => entity.UserId == userId).ToListAsync();
            }
            return await dbSet.Where(entity => entity.UserId == userId && ids.Contains(entity.Id.GetValueOrDefault())).ToListAsync();
        }
    }
}
