using GardenAPI.Entities.Gardens;

namespace GardenAPI.Transfer.Garden
{
    public record RequestGardenDTO
    {
        public int? Id { get; init; }
        public required string UserId { get; init; }
        public List<Bed>? Beds { get; init; }
    }
}
