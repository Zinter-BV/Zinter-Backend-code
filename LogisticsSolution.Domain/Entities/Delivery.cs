using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogisticsSolution.Domain.Entities
{
    public class Delivery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("driverName")]
        public string DriverName { get; set; }

        [BsonElement("destination")]
        public string Destination { get; set; }

        [BsonElement("scheduledAt")]
        public DateTime ScheduledAt { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = "Pending";

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
