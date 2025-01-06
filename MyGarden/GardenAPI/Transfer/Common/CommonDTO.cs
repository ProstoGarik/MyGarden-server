namespace GardenAPI.Transfer.Common
{
    public record CommonDTO:IdentifiableEntityDTO
    {
        public required string Title { get; init; }
    }
}
