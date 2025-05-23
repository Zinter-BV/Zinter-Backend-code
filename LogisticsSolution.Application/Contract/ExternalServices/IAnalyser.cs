using LogisticsSolution.Application.Dtos.Response;
using Microsoft.AspNetCore.Http;

namespace LogisticsSolution.Application.Contract.ExternalServices
{
    public interface IAnalyser
    {
        Task<List<AnalysedImageResponseModel>> GetItemsByImages(List<IFormFile> images);
    }
}
