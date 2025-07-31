using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;
using LogisticsSolution.Application.Utility;
using LogisticsSolution.Domain.Entities;
using LogisticsSolution.Domain.Enums;
using Microsoft.Extensions.Options;

namespace LogisticsSolution.Application.BusinessLogic
{
    public class AuthService : IAuth
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICache _cache;
        private readonly AppSettings _appSettings;
        public AuthService(ILogger<AuthService> logger, IUnitOfWork unitOfWork, ICache cache, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _appSettings = appSettings.Value;
        }


        public async Task<ResponseModel<string>> RegisterAgent(AgentRegistrationDto request)
        {
            try
            {
                var allprovinces = await _cache.GetProvinces();
                List<AgentProvince> provincesCovered = new List<AgentProvince>();

                //verify password
                if (!request.Password.IsStrongPassword())
                    return "Password strength too weak".FailResponse<string>();
                //verify email
                if (!request.Email.IsValidEmail())
                    return "Invalid Email address".FailResponse<string>();
                //verify province Id against the cache data
                var invalidProvinces = request.Provinces.Except(allprovinces.Select(x => x.Id).ToList()).ToList();

                if (invalidProvinces.Count > 0)
                    return "Invalid provinces added".FailResponse<string>();

                //Double registeration.............
                var duplicateRegistration = await _unitOfWork.GetRepository<MovingAgent>().AnyAsync(x => x.KvkNumber == request.KvkNumber);
                if (duplicateRegistration)
                    return $"Company under {request.KvkNumber} already registered".FailResponse<string>();
                //services rendered ????
                //complete registeration

                var encrpytPassword = request.Password.EncriptPassword();

                var newAgent = new MovingAgent
                {
                    Email = request.Email,
                    CompanyName = request.CompanyName,
                    KvkNumber = request.KvkNumber,
                    Image = request.Image,
                    CompanyOverView = request.CompanyOverView,
                    PasswordHash = encrpytPassword.PasswordHash,
                    PasswordSalt = encrpytPassword.PasswordSalt,
                    //list of covered provinces
                };
               
                foreach (var id in request.Provinces)
                {
                    provincesCovered.Add(new AgentProvince
                    {
                        AgentId = newAgent.Id,
                        ProvinceId = id,
                    });
                }

                newAgent.ProvincesCovered = provincesCovered;

                await _unitOfWork.GetRepository<MovingAgent>().AddAsync(newAgent);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("New agent {kvkNumber} successfully added to the system", request.KvkNumber);

                return "Registration completed".SuccessfulResponse();


            }
            catch (Exception ex)
            {
                _logger.LogError("RegisterAgent ::: {ex}", ex);
                return "Unable to complete registeration at this time".FailResponse<string>();
            }
        }

        public async Task<ResponseModel<LoginResponseModel>> LoginUser(LoginDto request)
        {
            try
            {
                var user = await _unitOfWork.GetRepository<MovingAgent>().FindSingleWithRelatedEntitiesAsync(x => (x.Email.ToLower() == request.User.ToLower() || x.KvkNumber == request.User));
                                                   

                if (user == null)
                    return "Invalid username/Password".FailResponse<LoginResponseModel>();

                if (!request.Password.VerifyPassword(user.PasswordHash, user.PasswordSalt))
                    return "Invalid username/Password".FailResponse<LoginResponseModel>();


                _logger.LogInformation($"{request.User} successfully logged in :::::::::: {DateTime.Now}");

                var response = new LoginResponseModel
                {
                    Name = user.CompanyName,
                    Role = RoleEnum.MovingAgent,
                    JwtToken = ExtensionMethods.CreateJwtToken(user.Id, (int)RoleEnum.MovingAgent, _appSettings.Token, _appSettings.TokenExpiryTime)
                };

                return response.SuccessfulResponse();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Login: {ex.Message}", ex);

                return "unable to login at this time".FailResponse<LoginResponseModel>();
            }
        }

    }
}
