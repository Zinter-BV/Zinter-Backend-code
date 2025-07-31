using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;

namespace LogisticsSolution.Application.Contract
{
    public interface IAuth
    {
        Task<ResponseModel<string>> RegisterAgent(AgentRegistrationDto request);
        Task<ResponseModel<LoginResponseModel>> LoginUser(LoginDto request);
    }
}
