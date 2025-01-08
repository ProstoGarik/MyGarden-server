using GardenAPI.Data;

namespace GardenAPI.Service.Common
{
    public class LightNeedService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
