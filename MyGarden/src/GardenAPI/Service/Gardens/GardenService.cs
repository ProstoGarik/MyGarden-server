using GardenAPI.Data;
using GardenAPI.Entities.Gardens;

namespace GardenAPI.Service.Gardens
{
    public class GardenService(DataContext dataContext) : HasUserIdEntityService<Garden>(dataContext)
    {
    }
}
