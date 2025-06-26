using System;
using System.Collections.Generic;

namespace LogisticsSolution.Domain.Entities
{
    public class MovingAgent
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string KvkNumber { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        public string Image { get; set; }
        public string CompanyOverView { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public MovingAgent()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
        public List<AgentProvince> ProvincesCovered { get; set; }
        public List<MoveHistory> MoveHistories { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
