using MyGarden.Server.Data;

namespace MyGarden.Server.Service.Common
{
    public class WateringNeedService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
