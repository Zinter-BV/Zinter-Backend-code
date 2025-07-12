namespace LogisticsSolution.Application.Dtos.Request
{
    public class MoveRequestDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
        public string PickUpAddress { get; set; }
        public string PickUpAddressNumber { get; set; }
        public string DropOffAddressNumber { get; set; }
        public string PickUpLongitude { get; set; }
        public string PickUpLatitude { get; set; }
        public string DropOffAddress { get; set; }
        public string DropOffLongitude { get; set; }
        public string DropOffLatitude { get; set; }
        public DateTime MoveTime { get; set; }
        public DateTime PickUpTime { get; set; }
        public int FromNumberOfFloors { get; set; }
        public int ToNumberOfFloors { get; set; }
        public string? FromLongCarry { get; set; }
        public string? ToLongCarry { get; set; }
        public string? ToRemark { get; set; }
        public string? FromRemark { get; set; }
        public bool ToHasElevator { get; set; }
        public bool FromHasElevator { get; set; }
        public bool ToNeedShuttle { get; set; }
        public bool FromNeedShuttle { get; set; }
        public bool FromHasBuildingInsurance { get; set; }
        public bool ToHasBuildingInsurance { get; set; }
        public bool FromNeedHelpPacking { get; set; }
        public bool ToNeedHelpPacking { get; set; }
        public List<MoveItemRequestDto> Items { get; set; }
    }

    public class MoveItemRequestDto
    {
        public string Room {  get; set; }
        public string ItemName { get; set; }
        public int NumberOfItems { get; set; }
    }
}
