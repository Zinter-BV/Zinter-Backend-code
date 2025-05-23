namespace LogisticsSolution.Application.Dtos.Response
{
    public class PendingMoveRequestResponseModel
    {
        public long MoveId { get; set; }
        public string MoveCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int NumberOfRooms { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
    }
}
