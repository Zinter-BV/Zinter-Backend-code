using LogisticsSolution.Application.Dtos.Response;

namespace LogisticsSolution.Application.Contract.ExternalServices
{
    public interface IKvk
    {
        Task<KvkCompanyProfile?> GetCompanyByKvkNumberAsync(string kvkNumber);
    }
}
