namespace LogisticsSolution.Application.Dtos
{
    public class PasswordEncriptResponseModel
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
