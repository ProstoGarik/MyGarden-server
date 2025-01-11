using GardenAPI.Data;
using GardenAPI.Entities.Common;

namespace GardenAPI.Service.Common
{
    public class WateringNeedService(DataContext dataContext) : DataEntityService<WateringNeed>(dataContext)
    {
    }
}
