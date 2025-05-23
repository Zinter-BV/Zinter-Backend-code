namespace LogisticsSolution.Application.Dtos.Response
{
    public class AgentDashBoardAnalyticsResponseModel
    {
        public int Incoming { get; set; }
        public int ApprovedRequest { get; set; }
        public int PaymentMade { get; set; }
        public int Upcoming { get; set; }
        public DateTime? NextMove { get; set; } = null;
        public int InTransit { get; set; }
        public int Completed { get; set; }
        public int Cancelled { get; set; }
    }
}
