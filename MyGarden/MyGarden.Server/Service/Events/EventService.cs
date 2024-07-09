using MyGarden.Server.Data;

namespace MyGarden.Server.Service.Common
{
    public class EventService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
