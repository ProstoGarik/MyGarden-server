using GardenAPI.Data;

namespace GardenAPI.Service.Plants
{
    public class GroupService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
