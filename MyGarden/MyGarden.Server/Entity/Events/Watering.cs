namespace MyGarden.Server.Entity.Events
{
    public class Watering
    {
        public required int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.MinValue;
        public required int PlantId { get; set; }
    }
}
