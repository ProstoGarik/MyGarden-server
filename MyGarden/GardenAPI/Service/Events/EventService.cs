using GardenAPI.Data;

namespace GardenAPI.Service.Common
{
    public class EventService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
