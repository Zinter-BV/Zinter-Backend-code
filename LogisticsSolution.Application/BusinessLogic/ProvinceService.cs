global using LogisticsSolution.Application.Constant;
global using LogisticsSolution.Application.Contract.ExternalServices;
global using Microsoft.Extensions.Logging;
using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos;
using LogisticsSolution.Application.Dtos.Response;
using LogisticsSolution.Application.Utility;
using LogisticsSolution.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace LogisticsSolution.Application.BusinessLogic
{
    public class ProvinceService : IProvince
    {
        private readonly ILogger<ProvinceService> _logger;
        private readonly ICache _cache;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProvinceService(ILogger<ProvinceService> logger, ICache cache, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _cache = cache;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<List<ProvinceResponseModel>>> GetAllProvinces()
        {
            try
            {
                var response = new List<ProvinceResponseModel>();

                var provinces = await _cache.GetProvinces();

                if (provinces.Count < 1)
                    return "No provinces found".FailResponse<List<ProvinceResponseModel>>();

                foreach (var province in provinces)
                {
                    response.Add(new ProvinceResponseModel
                    {
                        ProvinceId = province.Id,
                        ProvinceName = province.Name,
                    });

                }

                _logger.LogInformation("All {count} provinces successfully retrieved {date}", provinces.Count, DateTime.Now);
                return response.SuccessfulResponse();

            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllProvinces: {ex.Message}", ex);
                return "Unable to retrieve provinces".FailResponse<List<ProvinceResponseModel>>();
            }
        }

        public async Task<ResponseModel<Paged<PendingMoveRequestResponseModel>>> GetProvinceRequestsByAgent(PaginationDto request, bool isActive)
        {
            try
            {
                HttpContextContent? jwtClaims = _httpContextAccessor.GetHttpContextValues();

                if (jwtClaims == null || jwtClaims.role == Domain.Enums.RoleEnum.MovingAgent)
                {
                    return null;
                }


                int userId = jwtClaims.userId;

                var movingAgent = await _unitOfWork.GetRepository<MovingAgent>().FindSingleWithRelatedEntitiesAsync(x => x.Id == userId && !x.IsActive,
                                                                                                                    x => x.ProvincesCovered);

                if (movingAgent == null)
                    return "Invalid Agent".FailResponse<Paged<PendingMoveRequestResponseModel>>();

                if (movingAgent.ProvincesCovered.Count() < 1)
                    return "No provinces currently covered By Agent".FailResponse<Paged<PendingMoveRequestResponseModel>>();

                List<int> ListOfProvincesCovered = movingAgent.ProvincesCovered.Select(x => x.ProvinceId).ToList();

                return await GetActiveRequestByProvinceIds(ListOfProvincesCovered, request, isActive);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred ::: GetProvinceRequestsByAgent");
                return "Unable to get request".FailResponse<Paged<PendingMoveRequestResponseModel>>();
            }
        }

        private async Task<ResponseModel<Paged<PendingMoveRequestResponseModel>>> GetActiveRequestByProvinceIds(List<int> ids, PaginationDto pagination, bool isActive)
        {
            try
            {
                List<PendingMoveRequestResponseModel> pendingMoves = new List<PendingMoveRequestResponseModel>();

                var pendingRequestsQuery = isActive ? _unitOfWork.GetRepository<MoveRequest>().FindQueryableWithRelatedEntities(predicate: x => ids.Contains(x.ProvinceId) && x.MoveTime < DateTime.UtcNow, x => x.Province) :
                                                      _unitOfWork.GetRepository<MoveRequest>().FindQueryableWithRelatedEntities(predicate: x => ids.Contains(x.ProvinceId), x => x.Province);

                var paginatedPendingRequests = Paged<PendingMoveRequestResponseModel>.PaginatedList(pendingMoves, pendingRequestsQuery.Count(), pagination.PageNumber, pagination.NumberOfRecords);

                if (pendingRequestsQuery.Count() < 1)
                    return paginatedPendingRequests.SuccessfulResponse();

                var penndingMoveRequests = await _unitOfWork.GetRepository<MoveRequest>().GetQueriableToPagedListAsync(query: pendingRequestsQuery,
                                                                                                          pageSize: pagination.NumberOfRecords,
                                                                                                          pageNumber: pagination.PageNumber,
                                                                                                          orderBy: x => x.OrderByDescending(x => x.Id),
                                                                                                          x => x.Province, x => x.MoveItems);
                foreach (var pendingMove in penndingMoveRequests)
                {
                    pendingMoves.Add(new PendingMoveRequestResponseModel
                    {
                        MoveId = pendingMove.Id,
                        MoveCode = pendingMove.MoveCode,
                        FullName = pendingMove.FullName,
                        Email = pendingMove.Email,
                        Status = pendingMove.MoveStatus.ToString(),
                        Province = pendingMove.Province.Name,
                        Address = pendingMove.PickUpAddress,
                        NumberOfRooms = pendingMove.MoveItems.DistinctBy(x => x.RoomName).Count()
                    });
                }

                return paginatedPendingRequests.SuccessfulResponse();
            }
            catch (Exception ex)
            {

                _logger.LogError($"GetRequestByProvinceId: {ex.Message}", ex);
                return "Unable to retrieve requests".FailResponse<Paged<PendingMoveRequestResponseModel>>();
            }
        }
    }
}
