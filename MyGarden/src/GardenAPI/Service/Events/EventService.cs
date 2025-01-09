using GardenAPI.Data;
using GardenAPI.Entities.Events;

namespace GardenAPI.Service.Common
{
    public class EventService(DataContext dataContext) : HasUserIdEntityService<Event>(dataContext)
    {
    }
}
