using GardenAPI.Data;

namespace GardenAPI.Service.Common
{
    public class GrowStageServive(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
