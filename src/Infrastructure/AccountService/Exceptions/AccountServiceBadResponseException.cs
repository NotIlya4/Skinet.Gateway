using Newtonsoft.Json.Linq;

namespace Infrastructure.AccountService;

public class AccountServiceBadResponseException : Exception
{
    public string Title { get; }
    public string Detail { get; }
    public int StatusCode { get; }
    
    private AccountServiceBadResponseException(string msg, string title, string detail, int statusCode) : base(msg)
    {
        Title = title;
        Detail = detail;
        StatusCode = statusCode;
    }

    public static async Task<AccountServiceBadResponseException> Create(HttpResponseMessage message)
    {
        JObject jObject = JObject.Parse(await message.Content.ReadAsStringAsync());
        string title = jObject["title"]!.Value<string>()!;
        string detail = jObject["detail"]!.Value<string>()!;
        string msg = $"{title}: {detail}";
        return new AccountServiceBadResponseException(msg, title, detail, (int)message.StatusCode);
    }
}