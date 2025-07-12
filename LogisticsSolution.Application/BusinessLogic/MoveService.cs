using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;
using LogisticsSolution.Application.Utility;
using LogisticsSolution.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LogisticsSolution.Application.BusinessLogic
{
    public class MoveService : IMove
    {
        private readonly ICache _cache;
        private readonly ILogger<MoveService> _logger;
        private AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnalyser _analyser;
        public MoveService(ILogger<MoveService> logger, ICache cache, IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork, IAnalyser analyser)
        {
            _logger = logger;
            _cache = cache;
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _analyser = analyser;
        }

        public async Task<ResponseModel<string>> CreateRequest(MoveRequestDto request)
        {
            try
            {
                var availableProvinces = await _cache.GetProvinces();
                var listOfMoveItems = new List<MoveItem>();
                var minimumMoveTime = DateTime.UtcNow.AddMinutes(_appSettings.MinimumRequestTime);
                var selectedProvince = availableProvinces.FirstOrDefault(x => x.Id == request.ProvinceId);

                if (selectedProvince is null)
                    "Invalid Province id".FailResponse<string>();

                if (!request.Email.IsValidEmail())
                    "Invalid email address".FailResponse<string>();

                if (request.PhoneNumber.Length < 10 || request.PhoneNumber.Length > 14)
                    "Invalid mobile number".FailResponse<string>();

                if (request.MoveTime < minimumMoveTime)
                    "Invalid move time".FailResponse<string>();

                if (request.MoveTime <= request.PickUpTime)
                    "Pick up time cannot be before Move time".FailResponse<string>();

                if (request.Items.Count <= 0)
                    "Atleast one item needed".FailResponse<string>();


                var newMoveRequest = new MoveRequest
                {
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    ProvinceId = request.ProvinceId,
                    PickUpAddress = request.PickUpAddress,
                    PickUpLatitude = request.PickUpLatitude,
                    PickUpLongitude = request.PickUpLongitude,
                    DropOffAddress = request.DropOffAddress,
                    DropOffLatitude = request.DropOffLatitude,
                    DropOffLongitude = request.DropOffLongitude,
                    MoveTime = request.MoveTime,
                    PickUpTime = request.PickUpTime,
                    FromNumberOfFloors = request.FromNumberOfFloors,
                    ToNumberOfFloors = request.ToNumberOfFloors,
                    ToLongCarry = request.ToLongCarry ?? null,
                    FromLongCarry = request.FromLongCarry ?? null,
                    ToRemark = request.ToRemark,
                    FromRemark = request.FromRemark,
                    MoveCode = ExtensionMethods.GenerateMovingCode(),
                    FromHasElevator = request.FromHasElevator,
                    ToHasElevator = request.ToHasElevator,
                    FromNeedShuttle = request.FromNeedShuttle,
                    ToNeedShuttle = request.ToNeedShuttle,
                    FromNeedHelpPacking = request.FromNeedHelpPacking,
                    ToNeedHelpPacking = request.ToNeedHelpPacking,
                    FromHasBuildingInsurance = request.FromHasBuildingInsurance,
                    ToHasBuildingInsurance = request.ToHasBuildingInsurance,
                    PickUpAddressNumber = request.PickUpAddressNumber,
                    DropOffAddressNumber = request.DropOffAddressNumber
                };

                await _unitOfWork.GetRepository<MoveRequest>().AddAsync(newMoveRequest);
                await _unitOfWork.SaveChangesAsync();

                foreach (var item in request.Items)
                {
                    listOfMoveItems.Add(new MoveItem
                    {
                        MoveRequestId = newMoveRequest.Id,
                        RoomName = item.Room,
                        ItemName = item.ItemName,
                        ItemCount = item.NumberOfItems,
                    });
                }

                await _unitOfWork.GetRepository<MoveItem>().AddRangeAsync(listOfMoveItems);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("New request generated {code} , {date}", newMoveRequest.MoveCode, DateTime.UtcNow);

                return newMoveRequest.MoveCode.SuccessfulResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateRequest: {ex.Message}", ex);

                return "unable to create Request".FailResponse<string>();
            }
        }

        public async Task<ResponseModel<List<AnalysedImageResponseModel>>> GetItemsByImage(List<IFormFile> images)
        {
            try
            {
                var result = await _analyser.GetItemsByImages(images);

                return result.SuccessfulResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetItemsByImage: {ex.Message}", ex);

                return "unable to analyse Images".FailResponse<List<AnalysedImageResponseModel>>();
            }

        }
       
        public async Task<ResponseModel<MoveDetailsResponseModel>> GetDetailsByCode(string code)
        {
            try
            {
                var moveItems = new List<MoveItemCountResponeModel>();
                var moveDetailsResponse = new MoveDetailsResponseModel();
                var moveDetails = await _unitOfWork.GetRepository<MoveRequest>().FindSingleWithRelatedEntitiesAsync(predicate: x => x.MoveCode == code,
                                                                                                                    includes: x => x.MoveItems);

                if (moveDetails is null)
                    return "Invalid code".FailResponse<MoveDetailsResponseModel>();

                moveItems = moveDetails.MoveItems
                    .GroupBy(x => x.RoomName)
                    .Select(x => new MoveItemCountResponeModel
                    {
                        RoomName = x.Key,
                        Count = x.Count(),
                        Items = x.Select(i => i.ItemName).ToList()
                    })
                    .ToList();

                moveDetailsResponse.FullName = moveDetails.FullName;
                moveDetailsResponse.Email = moveDetails.Email;
                moveDetailsResponse.PhoneNumber = moveDetails.PhoneNumber;
                moveDetailsResponse.MoveDate = moveDetails.MoveTime;
                moveDetailsResponse.MoveDay = moveDetails.MoveTime.DayOfWeek.ToString();
                moveDetailsResponse.Status = moveDetails.MoveStatus.ToString(); 
                moveDetailsResponse.MoveTime = moveDetails.MoveTime.ToString("HH:mm");
                moveDetailsResponse.NumberOfRooms = moveItems.Count;
                moveDetailsResponse.From = moveDetails.PickUpAddress;
                moveDetailsResponse.PickUpLatitude = moveDetails.PickUpLatitude;
                moveDetailsResponse.PickUpLongitude = moveDetails.PickUpLongitude;
                moveDetailsResponse.DropOffLongitude = moveDetails.DropOffLongitude;
                moveDetailsResponse.DropOffLatitude = moveDetails.DropOffLatitude;
                moveDetailsResponse.To = moveDetails.DropOffAddress;
                moveDetailsResponse.MoveItemsDetails = moveItems;

                _logger.LogInformation("MOVE DETAILS:: {code} successfully retrieved",code);

                return moveDetailsResponse.SuccessfulResponse();

            }
            catch (Exception ex)
            {
                _logger.LogError($"GetDetailsByCode: {ex.Message}", ex);

                return "unable to get details".FailResponse<MoveDetailsResponseModel>();
            }
        }

    }
}
