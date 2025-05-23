using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Utility;
using LogisticsSolution.Domain.Entities;

namespace LogisticsSolution.Application.BusinessLogic
{
    public class AuthService : IAuth
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICache _cache;
        public AuthService(ILogger<AuthService> logger, IUnitOfWork unitOfWork, ICache cache)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _cache = cache;
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

    }
}
