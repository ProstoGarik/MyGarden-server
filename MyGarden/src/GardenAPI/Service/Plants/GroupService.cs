using GardenAPI.Data;
using GardenAPI.Entities.Plants;

namespace GardenAPI.Service.Plants
{
    public class GroupService(DataContext dataContext) : HasUserIdEntityService<Group>(dataContext)
    {
    }
}
