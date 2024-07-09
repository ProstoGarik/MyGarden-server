using MyGarden.Server.Data;

namespace MyGarden.Server.Service.Plants
{
    public class GroupService(DataContext dataContext) : DataEntityService(dataContext)
    {
    }
}
