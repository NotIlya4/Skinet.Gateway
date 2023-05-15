using Infrastructure.Auther.Client;
using Infrastructure.Auther.Helpers;
using Infrastructure.Auther.Models;

namespace Infrastructure.Auther;

public class Auther : IAuther
{
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly ISimpleHttpClient _client;
    private readonly AccountServiceUrlProvider _urlProvider;

    public Auther(IJwtTokenProvider jwtTokenProvider, ISimpleHttpClient client, AccountServiceUrlProvider urlProvider)
    {
        _jwtTokenProvider = jwtTokenProvider;
        _client = client;
        _urlProvider = urlProvider;
    }
    
    public async Task<UserInfo> GetUserInfo()
    {
        string jwtToken = _jwtTokenProvider.Read();
        return await _client.Get<UserInfo>(_urlProvider.MakeGetUserByJwtTokenUrl(jwtToken));
    }
}