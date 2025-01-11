using MongoDB.Bson.Serialization.Attributes;

namespace UserGardenAPI.Model
{
    public class Plant
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; } = "";

        public string PhotoId { get; set; } = "";
    }
}
