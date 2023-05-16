namespace Infrastructure.Auther.SimpleHttpClient;

public interface ISimpleHttpClient
{
    public Task<T> Get<T>(Uri url);
}