using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;
using Infrastructure.Client;

namespace Api.ExceptionMappers;

public class ServiceBadResponseExceptionMapper : IExceptionMapper<ServiceBadResponseException>
{
    public BadResponse Map(ServiceBadResponseException exception)
    {
        return new BadResponse()
        {
            StatusCode = exception.StatusCode,
            ResponseDto = new
            {
                Title = exception.Title,
                Detail = exception.Detail
            }
        };
    }
}