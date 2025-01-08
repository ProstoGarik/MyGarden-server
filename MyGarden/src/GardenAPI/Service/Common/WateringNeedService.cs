using GardenAPI.Data;

namespace GardenAPI.Service.Common
{
    public class WateringNeedService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
