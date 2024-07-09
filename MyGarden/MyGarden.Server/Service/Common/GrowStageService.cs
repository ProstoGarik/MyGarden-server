using MyGarden.Server.Data;

namespace MyGarden.Server.Service.Common
{
    public class GrowStageServive(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
