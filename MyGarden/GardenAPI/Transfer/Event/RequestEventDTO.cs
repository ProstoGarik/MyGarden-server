namespace GardenAPI.Transfer.Event
{
    public class RequestEventDTO
    {
        public required int PlantId { get; init; }
        public DateTime Date { get; init; }
        public string? Title { get; init; }
    }
}
