namespace LogisticsSolution.Application.Contract
{
    public interface IApiRequestHandler
    {
        Task<TResponse> SendRequestAsync<TResponse>(string url, HttpMethod method, object body = null);
        Task<TResponse> GetAsync<TResponse>(string url);
        Task<TResponse> PostAsync<TResponse>(string url, object body);
        Task<TResponse> PutAsync<TResponse>(string url, object body);
        Task<TResponse> DeleteAsync<TResponse>(string url);
    }
}
