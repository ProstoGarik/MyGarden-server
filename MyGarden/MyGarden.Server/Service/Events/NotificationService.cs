using MyGarden.Server.Data;

namespace MyGarden.Server.Service.Common
{
    public class NotificationService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
