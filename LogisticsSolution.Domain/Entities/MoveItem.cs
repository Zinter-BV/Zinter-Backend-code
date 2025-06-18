using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogisticsSolution.Domain.Entities
{
    public class MoveItem
    {
        public int Id { get; set; }
        public long MoveRequestId { get; set; }
        public MoveRequest MoveRequest { get; set; }
        public string RoomName { get; set; }
        public string ItemName { get; set; }
        public int ItemCount { get; set; } = 1;

    }
}
