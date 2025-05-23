using LogisticsSolution.Domain.Enums;

namespace LogisticsSolution.Domain.Entities
{
    public class MoveRequest
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public MoveStatusEnum MoveStatus { get; set; } = MoveStatusEnum.Pending;
        public string PickUpAddress { get; set; }
        public string DropOffAddress { get; set; }
        public DateTime MoveTime { get; set; }
        public DateTime PickUpTime { get; set; }
        public int NumberOfFloors { get; set; }
        public string? LongCarry { get; set; }
        public string PickUpLongitude { get; set; }
        public string PickUpLatitude { get; set; }
        public string DropOffLongitude { get; set; }
        public string DropOffLatitude { get; set; }
        public string MoveCode  { get; set; }
        public bool HasElevator { get; set; }
        public bool NeedShuttle { get; set; }
        public bool HasBuildingInsurance { get; set; }
        public bool NeedHelpPacking { get; set; }
        public DateTime CreatedOn { get; set; }
        public MoveRequest()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
        public List<MoveItem> MoveItems { get; set; }
        public List<MoveHistory> MoveHistories { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
