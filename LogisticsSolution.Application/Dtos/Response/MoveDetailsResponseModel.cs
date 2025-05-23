namespace LogisticsSolution.Application.Dtos.Response
{
    public class MoveDetailsResponseModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MoveDate { get; set; }
        public string MoveDay { get; set; }
        public string Status { get; set; }
        public string MoveTime { get; set; }
        public int NumberOfRooms { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string PickUpLongitude { get; set; }
        public string PickUpLatitude { get; set; }
        public string DropOffLongitude { get; set; }
        public string DropOffLatitude { get; set; }
        public List<MoveItemCountResponeModel> MoveItemsDetails { get; set; }
    }

    public class MoveItemCountResponeModel
    {
        public string RoomName { get; set; }
        public int Count { get; set; }
        public List<string> Items { get; set; }
    }
}
