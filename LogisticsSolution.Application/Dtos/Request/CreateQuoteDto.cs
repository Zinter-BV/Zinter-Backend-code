namespace LogisticsSolution.Application.Dtos.Request
{
    public class CreateQuoteDto
    {
        public long MoveId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ProposedTime { get; set; }
        public string? AdditonalInformation { get; set; } = null;
    }
}
