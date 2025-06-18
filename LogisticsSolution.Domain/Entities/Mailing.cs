using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace LogisticsSolution.Domain.Entities
{
    public class Mailing
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public Mailing()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
