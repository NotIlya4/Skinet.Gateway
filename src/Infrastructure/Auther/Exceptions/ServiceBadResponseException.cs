using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Auther.Client;

public class ServiceBadResponseException : Exception
{
    public JObject ResponseDto { get; }
    public int StatusCode { get; }
    
    private ServiceBadResponseException(string msg, JObject responseDto, int statusCode) : base(msg)
    {
        ResponseDto = responseDto;
        StatusCode = statusCode;
    }

    public static async Task<ServiceBadResponseException> Create(HttpResponseMessage message)
    {
        string content = await message.Content.ReadAsStringAsync();

        JObject jObject;
        try
        {
            jObject = JObject.Parse(content);
        }
        catch (JsonReaderException)
        {
            return new ServiceBadResponseException(content, new JObject(), (int)message.StatusCode);
        }

        return new ServiceBadResponseException(content, jObject, (int)message.StatusCode);
    }
}