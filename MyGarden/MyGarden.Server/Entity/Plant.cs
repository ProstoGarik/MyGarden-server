namespace MyGarden.Server.Entity
{
    public class Plant
    {
        public required int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string BiologyTitle { get; set; } = string.Empty;
        public WateringNeed Watering { get; set; } = WateringNeed.None;
        public LightNeed LightNeed { get; set; } = LightNeed.None;
        public string Fertilization { get; set; } = string.Empty;
        public string Toxicity { get; set; } = string.Empty;
        public GrowStage Stage { get; set; } = GrowStage.None;
        public string Replacing { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ImageId { get; set; } = 0;
        public required int GroupId { get; set; }
        public int RipeningPeriod { get; set; } = int.MaxValue;
    }

    public enum GrowStage
    {
        None,
        Seed,
        Sprout,
        Junior,
        FruitBearer,
        Aged
    }

    public enum LightNeed
    {
        None,
        Low,
        Medium,
        High
    }

    public enum WateringNeed
    {
        None,
        Low,
        Medium,
        High
    }
}
