namespace LogisticsSolution.Domain.Entities
{
    public class AgentProvince
    {
        public long Id { get; set; }
        public MovingAgent Agent { get; set; }
        public int AgentId { get; set; }
        public Province Province { get; set; }
        public int ProvinceId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public AgentProvince()
        {
            this.IsActive = true;
            this.CreatedOn = DateTime.Now;
        }
    }
}
