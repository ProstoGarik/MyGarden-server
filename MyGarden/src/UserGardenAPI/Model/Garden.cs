using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UserGardenAPI.Model
{
    public class Garden
    {
        [BsonId]
        public string Id { get; set; }

        public string UserId { get; set; } = "";

        public string GardenType { get; set; } = "";

        public List<Plant> Plants { get; set; } = [];
    }
}
