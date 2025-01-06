using GardenAPI.Entities.Common;
using MyGarden.Server.Entity.Common;

namespace GardenAPI.Transfer.Common
{
    public static class CommonMapper
    {
        public static T ToEntity<T>(this CommonDTO game) where T : CommonEntity, new()
        {
            return new T 
            { 
                Id = game.Id, 
                Title = game.Title, 
                CreatedAt = game.CreatedAt, 
                UpdatedAt = game.UpdatedAt 
            };
        }


        public static T ToDTO<T>(this CommonEntity game) where T:CommonDTO,new()
        {
            return new T
            {
                Id = game.Id,
                Title = game.Title!,
                CreatedAt = game.CreatedAt,
                UpdatedAt = game.UpdatedAt
            };
        }
    }
}
}
