namespace Infrastructure.Client;

public interface IServiceHttpClient
{
    public Task<T> Get<T>(Uri url, object? queryParams = null);
    public Task Get(Uri url, object? queryParams = null);
    public Task<string> GetRaw(Uri url, object? queryParams = null);
    public Task<T> Post<T>(Uri url, object? body = null);
    public Task Post(Uri url, object? body = null);
    public Task<string> PostRaw(Uri url, object? body = null);
}