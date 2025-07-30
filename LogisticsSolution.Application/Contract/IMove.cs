using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;
using Microsoft.AspNetCore.Http;

namespace LogisticsSolution.Application.Contract
{
    public interface IMove
    {
        Task<ResponseModel<string>> CreateRequest(MoveRequestDto request);
        Task<ResponseModel<MoveDetailsResponseModel>> GetDetailsByCode(string code);
        Task<ResponseModel<List<MoveItemRequestDto>>> GetItemsByImage(List<IFormFile> images, string roomName);
    }
}
