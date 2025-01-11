using GardenAPI.Data;
using GardenAPI.Entities.Common;

namespace GardenAPI.Service.Common
{
    public class LightNeedService(DataContext dataContext) : DataEntityService<LightNeed>(dataContext)
    {
    }
}
