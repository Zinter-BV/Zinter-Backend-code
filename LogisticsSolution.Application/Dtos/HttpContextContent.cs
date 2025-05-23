using LogisticsSolution.Domain.Enums;

namespace LogisticsSolution.Application.Dtos
{
    public class HttpContextContent
    {
        public int userId { get; set; }
        public RoleEnum role { get; set; }
    }
}
