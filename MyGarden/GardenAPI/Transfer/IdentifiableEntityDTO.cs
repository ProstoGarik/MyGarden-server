namespace GardenAPI.Transfer
{
    public record IdentifiableEntityDTO : EntityDTO
    {
        public required int? Id { get; init; }
    }
}
