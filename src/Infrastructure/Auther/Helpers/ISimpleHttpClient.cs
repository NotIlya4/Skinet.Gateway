namespace Infrastructure.Auther.Client;

public interface ISimpleHttpClient
{
    public Task<T> Get<T>(Uri url);
}