using LogisticsSolution.Application.Dtos.Request;

namespace LogisticsSolution.Application.Contract
{
    public interface IAuth
    {
        Task<ResponseModel<string>> RegisterAgent(AgentRegistrationDto request);
    }
}
