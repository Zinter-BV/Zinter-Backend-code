namespace LogisticsSolution.Application.Dtos.Request
{
    public class MoveRequestDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
        public string PickUpAddress { get; set; }
        public string PickUpLongitude { get; set; }
        public string PickUpLatitude { get; set; }
        public string DropOffAddress { get; set; }
        public string DropOffLongitude { get; set; }
        public string DropOffLatitude { get; set; }
        public DateTime MoveTime { get; set; }
        public DateTime PickUpTime { get; set; }
        public int NumberOfFloors { get; set; }
        public string? LongCarry { get; set; }
        public bool HasElevator { get; set; }
        public bool NeedShuttle { get; set; }
        public bool HasBuildingInsurance { get; set; }
        public bool NeedHelpPacking { get; set; }
        public List<MoveItemRequestDto> Items { get; set; }
    }

    public class MoveItemRequestDto
    {
        public string Room {  get; set; }
        public string ItemName { get; set; }
        public int NumberOfItems { get; set; }
    }
}
