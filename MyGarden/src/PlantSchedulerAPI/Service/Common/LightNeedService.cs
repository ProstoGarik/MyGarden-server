using GardenAPI.Data;
using MyGarden.Server.Entity.Common;

namespace GardenAPI.Service.Common
{
    public class LightNeedService(DataContext dataContext) : DataEntityService<LightNeed>(dataContext)
    {
    }
}
