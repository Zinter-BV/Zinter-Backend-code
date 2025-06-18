using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace LogisticsSolution.Domain.Entities
{
    public class OfferedService
    {
        public int Id { get; set; }
        public string Service { get; set; }
    }
}
