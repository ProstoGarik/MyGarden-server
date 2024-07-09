using MyGarden.Server.Data;

namespace MyGarden.Server.Service.Common
{
    public class LightNeedService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
