using System;

namespace LogisticsSolution.Domain.Entities
{
    public class Quote
    {
        public long Id { get; set; }
        public MoveRequest MoveRequest { get; set; }
        public long MoveRequestId { get; set; }
        public MovingAgent MovingAgent { get; set; }
        public int MovingAgentId { get; set; }
        public decimal Amount { get; set; }
        public bool? IsAccepted { get; set; } = null;
        public DateTime CreatedOn { get; set; }
        public DateTime ProposedTime { get; set; }
        public string? AdditonalInformation { get; set; } = null;
        public DateTime? LastModifiedOn { get; set; } = null;
        public Quote()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
