using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Domain.Entities;

namespace LogisticsSolution.Application.Contract.ExternalServices
{
    public interface ICache
    {
        Task<List<Province>> GetProvinces();
        Task<bool> SaveVerificationCode(EmailVerificationCacheDto request);
        Task<bool> VerifyCode(string email, string code);
    }
}
