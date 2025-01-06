using GardenAPI.Data;

namespace GardenAPI.Service.Plants
{
    public class PlantService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
