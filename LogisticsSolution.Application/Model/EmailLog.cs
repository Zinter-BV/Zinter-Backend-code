using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LogisticsSolution.Application.Models
{
    public class EmailLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentAt { get; set; }
    }
}
