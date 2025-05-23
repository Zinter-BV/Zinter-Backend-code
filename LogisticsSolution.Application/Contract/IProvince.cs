using LogisticsSolution.Application.Dtos.Response;
using LogisticsSolution.Application.Utility;

namespace LogisticsSolution.Application.Contract
{
    public interface IProvince
    {
        Task<ResponseModel<List<ProvinceResponseModel>>> GetAllProvinces();
        Task<ResponseModel<Paged<PendingMoveRequestResponseModel>>> GetProvinceRequestsByAgent(PaginationDto request, bool isActive);
    }
}
