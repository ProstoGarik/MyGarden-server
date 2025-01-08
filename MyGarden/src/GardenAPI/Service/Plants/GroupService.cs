using GardenAPI.Data;
using GardenAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GardenAPI.Service.Plants
{
    public class GroupService(DataContext dataContext) : HasUserIdEntityService(dataContext)
    {
    }
}
