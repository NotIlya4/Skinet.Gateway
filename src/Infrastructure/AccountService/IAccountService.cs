namespace Infrastructure.AccountService;

public interface IAccountService
{
    public Task<JwtTokenPair> Login(RegisterCredentials registerCredentials);
    public Task<JwtTokenPair> Register(RegisterCredentials registerCredentials);
    public Task<JwtTokenPair> UpdateJwtPair(JwtTokenPair jwtTokenPair);
    public Task<UserInfo> GetUserInfo(Guid userId);
    public Task<string> ValidateJwtAndGetUserId(string jwtToken);
}