using System;
using System.Collections.Generic;
using LogisticsSolution.Domain.Enums;


namespace LogisticsSolution.Domain.Entities
{
    public class MoveHistory
    {
        public int Id { get; set; }
        public MoveRequest MoveRequest { get; set; }
        public long MoveRequestId { get; set; }
        public MovingAgent MovingAgent { get; set; }
        public int MoveAgentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public MoveStatusEnum MoveStatus { get; set; }
        public DateTime? CompletedOn { get; set; } = null;
        public DateTime ScheduledTime { get; set; }
    }
}
