namespace GardenAPI.Transfer.Group
{
    public static class GroupMapper
    {
        public static Entities.Plants.Group ToEntity(this RequestGroupDTO requestGroup)
        {
            return new Entities.Plants.Group
            {
                Id = requestGroup.Id,
                UserId = requestGroup.UserId,
                Title = requestGroup.Title,
            };
        }


        public static GroupDTO ToDTO(this Entities.Plants.Group group)
        {
            return new GroupDTO
            {
                Id = group.Id,
                UserId = group.UserId,
                Title = group.Title,
                CreatedAt = group.CreatedAt,
                UpdatedAt = group.UpdatedAt
            };
        }
    }
}
