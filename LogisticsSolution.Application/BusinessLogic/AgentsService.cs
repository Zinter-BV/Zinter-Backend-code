using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Contract.Notification;
using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;
using LogisticsSolution.Application.Utility;
using LogisticsSolution.Domain.Entities;
using LogisticsSolution.Domain.Enums;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace LogisticsSolution.Application.BusinessLogic
{
    public class AgentsService : IAgent
    {
        private readonly ILogger<AgentsService> _logger;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICache _cache;
        private readonly INotification _notification;
        private readonly IKvk _kvk;
        public AgentsService(ILogger<AgentsService> logger, IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork, ICache cache, INotification notification, IKvk kvk)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _notification = notification;
            _kvk = kvk;
        }

        public async Task<ResponseModel<string>> SendEmailVerificationCode(EmailVerificationDto request)
        {
            try
            {
                if (!request.Email.IsValidEmail())
                    return "Invalid Email address".FailResponse<string>();

                if (await _unitOfWork.GetRepository<MovingAgent>().AnyAsync(x => x.Email.ToLower() == request.Email.ToLower()))
                    return "Email already Exist".FailResponse<string>();

                if (!request.Password.IsStrongPassword())
                    return "Password is too weak".FailResponse<string>();

                var code = ExtensionMethods.GenerateEmailVerificationToken(6);

                var cacheData = new EmailVerificationCacheDto
                {
                    Email = request.Email,
                    Password = request.Password,
                    Code = code,
                };

                var isCached = await _cache.SaveVerificationCode(cacheData);

                if (!isCached)
                    return "Unable to generate verification Code".FailResponse<string>();

                var message = $"{request.Email} heres the code {code}";

                var emailDto = new EmailDto
                {
                    Subject = "Test code",
                    To = request.Email,
                    Body = message
                };

                await _notification.SendEmailAsync(emailDto);

                _logger.LogInformation("email successfully sent {email}", request.Email);

                return "email has been sent".SuccessfulResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendEmailVerificationCode: {ex.Message}", ex);
                return "Unable to send verification Code".FailResponse<string>();
            }
        }

        public async Task<ResponseModel<bool>> VerifyCode(string email, string code)
        {
            try
            {
                var isVerified = await _cache.VerifyCode(email, code);


                if (isVerified)
                    _logger.LogInformation("{email} verified account {time}", email, DateTime.UtcNow);

                return isVerified.SuccessfulResponse();
            }
            catch (Exception ex)
            {

                return "Unable to Verify".FailResponse<bool>();
            }
        }

        public async Task<ResponseModel<KvkCompanyDetailsResponseModel>> GetCompanyDetailsByKvkNumber(string kvkNumber)
        {
            try
            {
                var companyDetails = await _kvk.GetCompanyByKvkNumberAsync(kvkNumber);
                if (companyDetails is null)
                    return "Company information not found".FailResponse<KvkCompanyDetailsResponseModel>();

                _logger.LogInformation("{kvkNumber} details succesfully retrieved", kvkNumber);

                var firstAddress = companyDetails.Embedded.MainBranch.Addresses.First();

                DateTime dt = DateTime.ParseExact(companyDetails.FormalRegistrationDate, "yyyyMMdd", CultureInfo.InvariantCulture);

                var companyDetailsResponse = new KvkCompanyDetailsResponseModel
                {
                    CompanyName = companyDetails.Name,
                    RegisterationDate = dt,
                    PhoneNumber = null,
                    Address = firstAddress.FullAddress
                };

                return companyDetailsResponse.SuccessfulResponse();

            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCompanyDetailsByKvkNumber: {ex.Message}", ex);
                return "Unable to send verification Code".FailResponse<KvkCompanyDetailsResponseModel>();
            }
        }

        public async Task<ResponseModel<AgentDashBoardAnalyticsResponseModel>> GetDashBoardStatistics()
        {
            try
            {
              

                var all = await _unitOfWork.GetRepository<MovingAgent>().FindAllAsync();

                //modify
                int userId = all.FirstOrDefault().Id;

                var movingAgent = await _unitOfWork.GetRepository<MovingAgent>().FindSingleWithRelatedEntitiesAsync(x => x.Id == userId && !x.IsActive,
                                                                                                                    x => x.ProvincesCovered);

                if (movingAgent == null)
                    return "Invalid Agent".FailResponse<AgentDashBoardAnalyticsResponseModel>();

                var moveRequestRepository = _unitOfWork.GetRepository<MoveRequest>();
                var moveHistoryRepository = _unitOfWork.GetRepository<MoveHistory>();
                List<int> provincesCovered = movingAgent.ProvincesCovered.Select(x => x.ProvinceId).ToList();

                var response = new AgentDashBoardAnalyticsResponseModel
                {
                    Incoming = await moveRequestRepository.CountAsync(x => provincesCovered.Contains(x.ProvinceId) && x.MoveStatus == MoveStatusEnum.Pending && x.MoveTime < DateTime.UtcNow),
                    ApprovedRequest = await moveHistoryRepository.CountAsync(x => x.MoveAgentId == userId && x.MoveStatus != MoveStatusEnum.Cancelled),
                    PaymentMade = await moveHistoryRepository.CountAsync(x => x.MoveAgentId == userId && x.MoveStatus == MoveStatusEnum.PaymentsMade),
                    Upcoming = await moveHistoryRepository.CountAsync(x => x.MoveAgentId == userId && x.MoveStatus == MoveStatusEnum.Approved && x.ScheduledTime < DateTime.UtcNow),
                    InTransit = await moveHistoryRepository.CountAsync(x => x.MoveAgentId == userId && x.MoveStatus == MoveStatusEnum.InProgress),
                    Completed = await moveHistoryRepository.CountAsync(x => x.MoveAgentId == userId && x.MoveStatus == MoveStatusEnum.Completed),
                    Cancelled = await moveHistoryRepository.CountAsync(x => x.MoveAgentId == userId && x.MoveStatus == MoveStatusEnum.Cancelled),
                };

                return response.SuccessfulResponse();

            }
            catch (Exception ex)
            {
                _logger.LogError("GetDashBoardStatistics error ::: {ex}", ex);
                return "Unable to load figures".FailResponse<AgentDashBoardAnalyticsResponseModel>();
            }
        }


    }
}
