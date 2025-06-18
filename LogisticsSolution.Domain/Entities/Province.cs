using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogisticsSolution.Domain.Entities
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MoveRequest> MoveRequests { get; set; }
        public List<AgentProvince> MovingAgents { get; set; }
    }
}
