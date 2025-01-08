namespace GardenAPI.Transfer.Group
{
    public static class GroupMapper
    {
        public static Entities.Group ToEntity(this RequestGroupDTO request_group)
        {
            return new Entities.Group
            {
                Id = request_group.Id,
                UserId = request_group.UserId,
                Title = request_group.Title,
            };
        }


        public static GroupDTO ToDTO(this Entities.Group plant)
        {
            return new GroupDTO
            {
                Id = plant.Id,
                UserId = plant.UserId,
                Title = plant.Title,
                CreatedAt = plant.CreatedAt,
                UpdatedAt = plant.UpdatedAt
            };
        }
    }
}
