namespace MyGarden.Server.Entity.Events
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.MinValue;
        public int EventId { get; set; }
    }
}
