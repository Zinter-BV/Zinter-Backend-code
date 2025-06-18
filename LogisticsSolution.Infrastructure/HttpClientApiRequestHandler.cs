global using LogisticsSolution.Application.Contract;
using Newtonsoft.Json;
using System.Text;

namespace LogisticsSolution.Infrastructure
{
    public class HttpClientApiRequestHandler : IApiRequestHandler
    {
        private readonly HttpClient _httpClient;

        public HttpClientApiRequestHandler(bool ignoreSslValidation = true)
        {
            var handler = ignoreSslValidation ? CreateIgnoreSslHandler() : new HttpClientHandler();
            _httpClient = new HttpClient(handler);
        }

        private HttpClientHandler CreateIgnoreSslHandler()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };
        }

        public async Task<TResponse> SendRequestAsync<TResponse>(string url, HttpMethod method, object? body = null)
        {
            var requestMessage = new HttpRequestMessage(method, url);

            if (body != null && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                string jsonBody = JsonConvert.SerializeObject(body);
                requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentException($"Error: {response.ReasonPhrase}, Status Code: {(int)response.StatusCode}");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

                if (deserialized == null)
                {
                    throw new InvalidOperationException("The response could not be deserialized into the expected type.");
                }

                return deserialized;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Request failed: {ex.Message}", ex);
            }
        }

        public Task<TResponse> GetAsync<TResponse>(string url)
            => SendRequestAsync<TResponse>(url, HttpMethod.Get);

        public Task<TResponse> PostAsync<TResponse>(string url, object body)
            => SendRequestAsync<TResponse>(url, HttpMethod.Post, body);

        public Task<TResponse> PutAsync<TResponse>(string url, object body)
            => SendRequestAsync<TResponse>(url, HttpMethod.Put, body);

        public Task<TResponse> DeleteAsync<TResponse>(string url)
            => SendRequestAsync<TResponse>(url, HttpMethod.Delete);
    }
}
