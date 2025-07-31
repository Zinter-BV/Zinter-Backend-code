using LogisticsSolution.Domain.Enums;

namespace LogisticsSolution.Application.Dtos.Response
{
    public class LoginResponseModel
    {
        public string Name { get; set; }
        public RoleEnum Role { get; set; }
        public string JwtToken { get; set; }
    }
}
