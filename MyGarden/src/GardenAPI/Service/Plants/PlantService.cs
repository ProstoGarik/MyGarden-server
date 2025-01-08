using GardenAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace GardenAPI.Service.Plants
{
    public class PlantService(DataContext dataContext) : HasUserIdEntityService(dataContext)
    {
    }
}
