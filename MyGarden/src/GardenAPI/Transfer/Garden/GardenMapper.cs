namespace GardenAPI.Transfer.Garden
{
    public static class GardenMapper
    {
        public static Entities.Gardens.Garden ToEntity(this RequestGardenDTO requestGarden)
        {
            return new Entities.Gardens.Garden
            {
                Id = requestGarden.Id,
                UserId = requestGarden.UserId,
                Beds = requestGarden.Beds ?? []
            };
        }


        public static GardenDTO ToDTO(this Entities.Gardens.Garden garden)
        {
            return new GardenDTO
            {
                Id = garden.Id,
                UserId = garden.UserId,
                Beds = garden.Beds,
                CreatedAt = garden.CreatedAt,
                UpdatedAt = garden.UpdatedAt
            };
        }
    }
}
