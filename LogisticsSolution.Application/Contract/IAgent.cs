using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;

namespace LogisticsSolution.Application.Contract
{
    public interface IAgent
    {
        Task<ResponseModel<string>> SendEmailVerificationCode(EmailVerificationDto request);
        Task<ResponseModel<KvkCompanyDetailsResponseModel>> GetCompanyDetailsByKvkNumber(string kvkNumber);
        Task<ResponseModel<bool>> VerifyCode(string email, string code);
        Task<ResponseModel<AgentDashBoardAnalyticsResponseModel>> GetDashBoardStatistics();
    }
}
