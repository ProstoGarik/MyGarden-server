using GardenAPI.Data;
using GardenAPI.Entities.Common;

namespace GardenAPI.Service.Common
{
    public class GrowStageServive(DataContext dataContext) : DataEntityService<GrowStage>(dataContext)
    {
    }
}
