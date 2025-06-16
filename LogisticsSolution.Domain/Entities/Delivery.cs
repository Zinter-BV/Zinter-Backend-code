using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogisticsSolution.Domain.Entities
{
    public class Delivery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string DriverName { get; set; }
        public string Destination { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
