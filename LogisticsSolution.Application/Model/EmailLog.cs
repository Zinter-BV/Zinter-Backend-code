using System;

namespace LogisticsSolution.Application.Models
{
    public class EmailLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();  // Primary key (UUID)

        public string To { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }
    }
}
