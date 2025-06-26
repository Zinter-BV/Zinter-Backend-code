using System;

namespace LogisticsSolution.Domain.Entities
{
    public class Delivery
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DriverName { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime ScheduledAt { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
