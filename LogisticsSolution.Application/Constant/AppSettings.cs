namespace LogisticsSolution.Application.Constant
{
    public class AppSettings
    {
        public string Token { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public double TokenExpiryTime { get; set; }
        public double MinimumRequestTime { get; set; }
        public string AzureVisionEndpoint { get; set; }
        public string AzureVisionApiKey { get; set; }
        public string KvkApiKey { get; set; }
        public string KvkUrl { get; set; }
    }
}
