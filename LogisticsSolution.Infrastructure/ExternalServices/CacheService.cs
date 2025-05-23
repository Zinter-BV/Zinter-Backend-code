using LogisticsSolution.Application.Contract.ExternalServices;
using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace LogisticsSolution.Infrastructure.ExternalServices
{
    public class CacheService : ICache
    {
        private IMemoryCache _cache;
        private readonly ILogger<CacheService> _logger;
        private IUnitOfWork _unitOfWork;
        private const string Provinces = "Provinces";

        public CacheService(IMemoryCache cache, ILogger<CacheService> logger, IUnitOfWork unitOfWork)
        {
            _cache = cache;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Province>> GetProvinces()
        {
            try
            {
                _cache.TryGetValue(Provinces, out List<Province>? cachedData);

                if (cachedData == null)
                {
                    cachedData = await _unitOfWork.GetRepository<Province>().FindAllAsync();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddDays(5))
                        .SetPriority(CacheItemPriority.High);

                    _cache.Set(Provinces, cachedData, cacheEntryOptions);

                    _logger.LogInformation($"CacheData ::: Provinces Added");

                }

                return cachedData;

            }
            catch (Exception ex)
            {
                _logger.LogError("Province Cache service {ex}",ex);
                throw;
            }
        }

        public async Task<bool> SaveVerificationCode(EmailVerificationCacheDto request)
        {
            try
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetPriority(CacheItemPriority.Normal);

                _cache.Set(request.Email, request, cacheEntryOptions);

                _logger.LogInformation($"CacheData ::: {request.Email} verification token Added");

                return true;

            }
            catch (Exception ex)
            {

                _logger.LogError("SaveVerificationCode Cache service {ex}", ex);
                throw;
            }
        }

        public async Task<bool> VerifyCode(string email, string code)
        {
            try
            {
                EmailVerificationCacheDto? verificationDetails = null;
                _cache.TryGetValue(email, out verificationDetails);

                if (verificationDetails is null)
                    return false;

                _cache.Remove(email);
                return verificationDetails.Code == code;

            }
            catch (Exception ex)
            {

                _logger.LogError("VerifyCode Cache service {ex}", ex);
                throw;
            }
        }
    }
}
