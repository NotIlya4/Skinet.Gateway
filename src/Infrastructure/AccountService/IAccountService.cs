using Infrastructure.AccountService.Models;

namespace Infrastructure.AccountService;

public interface IAccountService
{
    public Task<JwtTokenPair> Login(LoginCredentials loginCredentials);
    public Task Logout(JwtTokenPair jwtTokenPair);
    public Task<JwtTokenPair> Register(RegisterCredentials registerCredentials);
    public Task<JwtTokenPair> UpdateJwtPair(JwtTokenPair jwtTokenPair);
    public Task<UserInfo> GetUser(string jwtToken);
}