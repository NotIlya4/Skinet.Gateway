using Newtonsoft.Json.Linq;

namespace Infrastructure.Client;

public class ServiceBadResponseException : Exception
{
    public string Title { get; }
    public string Detail { get; }
    public int StatusCode { get; }
    
    private ServiceBadResponseException(string msg, string title, string detail, int statusCode) : base(msg)
    {
        Title = title;
        Detail = detail;
        StatusCode = statusCode;
    }

    public static async Task<ServiceBadResponseException> Create(HttpResponseMessage message)
    {
        JObject jObject = JObject.Parse(await message.Content.ReadAsStringAsync());
        string title = jObject["title"]!.Value<string>()!;
        string detail = jObject["detail"]!.Value<string>()!;
        string msg = $"{title}: {detail}";
        return new ServiceBadResponseException(msg, title, detail, (int)message.StatusCode);
    }
}