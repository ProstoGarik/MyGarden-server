using GardenAPI.Data;

namespace GardenAPI.Service.Common
{
    public class EventService(DataContext dataContext) : HasUserIdEntityService(dataContext)
    {
    }
}
