using LogisticsSolution.Application.Constant;
using LogisticsSolution.Application.Contract.ExternalServices;
using LogisticsSolution.Application.Dtos.Response;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LogisticsSolution.Infrastructure.ExternalServices
{
    public class KvkService : IKvk
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<KvkService> _logger;
        private readonly AppSettings _appSettings;
        /* private const string ApiBaseUrl = "https://api.kvk.nl/api/v1/"; // Example URL (Check KvK Docs)
         private const string ApiKey = "your-api-key-here"; // Replace with your actual API Key
 */
        public KvkService(IOptions<AppSettings> appSettings, ILogger<KvkService> logger)
        {
            _httpClient = new HttpClient();
            _appSettings = appSettings.Value;
            _httpClient.DefaultRequestHeaders.Add("apikey", _appSettings.KvkApiKey);
            _logger = logger;
        }

        public async Task<KvkCompanyProfile?> GetCompanyByKvkNumberAsync(string kvkNumber)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_appSettings.KvkUrl}{kvkNumber}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<KvkCompanyProfile>(jsonResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetCompanyByKvkNumber: {ex}", ex);
                return null;
            }
        }
    }
}
