namespace GardenAPI.Transfer
{
    public record EntityDTO
    {
        public required DateTime? CreatedAt { get; init; }
        public required DateTime? UpdatedAt { get; init; }
    }
}
