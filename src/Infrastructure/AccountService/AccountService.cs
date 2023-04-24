using Infrastructure.AccountService.Helpers;
using Infrastructure.AccountService.Models;
using Infrastructure.Client;

namespace Infrastructure.AccountService;

public class AccountService : IAccountService
{
    private readonly IServiceHttpClient _client;
    private readonly AccountServiceUrlProvider _urlProvider;

    public AccountService(IServiceHttpClient client, AccountServiceUrlProvider urlProvider)
    {
        _client = client;
        _urlProvider = urlProvider;
    }

    public async Task<JwtTokenPair> Login(LoginCredentials loginCredentials)
    {
        return await _client.Post<JwtTokenPair>(_urlProvider.Login, loginCredentials);
    }

    public async Task Logout(JwtTokenPair jwtTokenPair)
    {
        await _client.Post(_urlProvider.Logout, jwtTokenPair);
    }

    public async Task<JwtTokenPair> Register(RegisterCredentials registerCredentials)
    {
        return await _client.Post<JwtTokenPair>(_urlProvider.Register, registerCredentials);
    }

    public async Task<JwtTokenPair> UpdateJwtPair(JwtTokenPair jwtTokenPair)
    {
        return await _client.Post<JwtTokenPair>(_urlProvider.UpdateJwtPair, jwtTokenPair);
    }

    public async Task<UserInfo> GetUser(string jwtToken)
    {
        return await _client.Get<UserInfo>(_urlProvider.MakeGetUserByJwtTokenUrl(jwtToken));
    }
}