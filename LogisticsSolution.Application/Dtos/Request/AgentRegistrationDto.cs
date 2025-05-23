namespace LogisticsSolution.Application.Dtos.Request
{
    public class AgentRegistrationDto
    {
        public string Email { get; set; }
        public string KvkNumber { get; set; }
        public string CompanyName { get; set; }
        public string Password { get; set; }
        public string Image {  get; set; }
        public List<int> Provinces { get; set; }
        public string CompanyOverView { get; set; }
    }
}
