namespace LogisticsSolution.Application.Dtos.Response
{
    public class QuoteSummaryResponseModel
    {
        public long QuoteId { get; set; }
        public string CompanyName { get; set; }
        public DateTime ProposedTime { get; set; }
        public string CompanyEmail { get; set; }
        public string? AdditionalInformation { get; set; }
        public decimal Amount { get; set; }

    }
}
