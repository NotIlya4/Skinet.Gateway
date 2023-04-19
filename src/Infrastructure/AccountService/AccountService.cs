using System.Text;
using Newtonsoft.Json.Linq;

namespace Infrastructure.AccountService;

class AccountService : IAccountService
{
    private readonly HttpClient _client;
    private readonly AccountServiceModelsMapper _mapper;
    private readonly AccountServiceUrlProvider _urlProvider;

    public AccountService(HttpClient client, AccountServiceModelsMapper mapper, AccountServiceUrlProvider urlProvider)
    {
        _client = client;
        _mapper = mapper;
        _urlProvider = urlProvider;
    }

    public async Task<JwtTokenPair> Login(RegisterCredentials registerCredentials)
    {
        return await Post<JwtTokenPair>(_urlProvider.Login, registerCredentials);
    }

    public async Task<JwtTokenPair> Register(RegisterCredentials registerCredentials)
    {
        return await Post<JwtTokenPair>(_urlProvider.Register, registerCredentials);
    }

    public async Task<JwtTokenPair> UpdateJwtPair(JwtTokenPair jwtTokenPair)
    {
        return await Post<JwtTokenPair>(_urlProvider.UpdateJwtPair, jwtTokenPair);
    }

    public async Task<UserInfo> GetUserInfo(Guid userId)
    {
        return await Get<UserInfo>(_urlProvider.GetUserById(userId.ToString()));
    }

    public Task<string> ValidateJwtAndGetUserId(string jwtToken)
    {
        throw new NotImplementedException();
    }

    private async Task<T> Post<T>(string url, object body)
    {
        JObject bodyJObject = JObject.FromObject(body);
        HttpContent requestBody = MakeHttpContent(bodyJObject);
        HttpResponseMessage response = await _client.PostAsync(url, requestBody);
        return await HandleResponse<T>(response);
    }

    private async Task<T> Get<T>(string url, object? queryParams = null)
    {
        HttpResponseMessage response = await _client.GetAsync(url);
        return await HandleResponse<T>(response);
    }

    private async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw await AccountServiceBadResponseException.Create(response);
        }

        return JObject.Parse(await response.Content.ReadAsStringAsync()).ToObject<T>() ??
               throw new InvalidOperationException("Failed to parse response from account service");
    }
    
    private StringContent MakeHttpContent(JObject jObject)
    {
        return new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
    }
}