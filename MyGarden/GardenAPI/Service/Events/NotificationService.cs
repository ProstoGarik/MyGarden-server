using GardenAPI.Data;

namespace GardenAPI.Service.Common
{
    public class NotificationService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
