using GardenAPI.Data;
using MyGarden.Server.Entity.Common;

namespace GardenAPI.Service.Common
{
    public class WateringNeedService(DataContext dataContext) : DataEntityService<WateringNeed>(dataContext)
    {
    }
}
