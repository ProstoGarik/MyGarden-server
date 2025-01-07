namespace GardenAPI.Transfer
{
    public record IdentifiableEntityDTO : EntityDTO
    {
        public int? Id { get; init; }
    }
}
