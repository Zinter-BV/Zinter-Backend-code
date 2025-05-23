using LogisticsSolution.Application.Constant;
using LogisticsSolution.Application.Contract.ExternalServices;
using LogisticsSolution.Application.Dtos.Response;
using LogisticsSolution.Application.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LogisticsSolution.Infrastructure.ExternalServices
{
    public class AnalyserService : IAnalyser

    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly string _apiKey;
        public AnalyserService(IOptions<AppSettings> appSettings)
        {
            _httpClient = new HttpClient();
            _appSettings = appSettings.Value;
            _endpoint = _appSettings.AzureVisionEndpoint + "computervision/imageanalysis:analyze?api-version=2023-02-01-preview&features=objects";
            _apiKey = _appSettings.AzureVisionApiKey;
        }

        public async Task<List<AnalysedImageResponseModel>> GetItemsByImages(List<IFormFile> images)
        {
            try
            {
                var results = new List<AnalysedImageResponseModel>();

                foreach (var image in images)
                {
                    results.Add(await GetItemsByImage(image));
                }
                return results;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<AnalysedImageResponseModel> GetItemsByImage(IFormFile image)
        {
            try
            {
                var items = new List<string>();
                var analysisResponse = new AnalysedImageResponseModel();
                analysisResponse.FileName = image.FileName;
                analysisResponse.IsImage = false;
                analysisResponse.IsAnalysed = false;

                if (image.GetFileType().ToLower() != "image")
                    return analysisResponse;

                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                var byteArray = memoryStream.ToArray();

                // Set up request headers
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);
                analysisResponse.IsImage = true;

                using var content = new ByteArrayContent(byteArray);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Call Azure AI Vision API
                var response = await _httpClient.PostAsync(_endpoint, content);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return analysisResponse;

                var responseObj = JsonSerializer.Deserialize<AzureVisionResponse>(jsonResponse);

                if (responseObj == null)
                    return analysisResponse;

                if (responseObj.ObjectsResult.Values.Count > 0)
                {

                    foreach (var item in responseObj.ObjectsResult.Values.First().Tags)
                    {
                        items.Add(item.Name);

                    }
                }
                analysisResponse.IsAnalysed = true;
                analysisResponse.items = items;
                analysisResponse.NumberOfDetectedItems = items.Count;

                return analysisResponse;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
