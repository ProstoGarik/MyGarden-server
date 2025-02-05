using GardenAPI.Entities.Gardens;

namespace GardenAPI.Transfer.Garden
{
    public record GardenDTO : IdentifiableEntityDTO
    {
        public required string UserId { get; init; }
        public List<Bed>? Beds { get; init; }
    }
}
