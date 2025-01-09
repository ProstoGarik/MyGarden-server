using GardenAPI.Data;
using MyGarden.Server.Entity.Common;

namespace GardenAPI.Service.Common
{
    public class GrowStageServive(DataContext dataContext) : DataEntityService<GrowStage>(dataContext)
    {
    }
}
