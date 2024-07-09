using MyGarden.Server.Data;

namespace MyGarden.Server.Service.Plants
{
    public class PlantService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
