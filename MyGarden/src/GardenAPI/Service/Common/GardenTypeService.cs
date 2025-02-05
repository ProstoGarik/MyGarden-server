using GardenAPI.Data;
using GardenAPI.Entities.Common;

namespace GardenAPI.Service.Common
{
    public class GardenTypeService(DataContext dataContext) : DataEntityService<GardenType>(dataContext)
    {
    }
}
