namespace LogisticsSolution.Application.Dtos.Request
{
    public class EmailVerificationDto
    {
        public string Email { get; set; }   
        public string Password { get; set; }
    }

    public class EmailVerificationCacheDto : EmailVerificationDto
    {
        public string Code { get; set; }
    }
}
