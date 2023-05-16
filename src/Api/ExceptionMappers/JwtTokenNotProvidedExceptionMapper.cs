using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;
using Infrastructure.Auther.JwtTokenProvider;

namespace Api.ExceptionMappers;

public class JwtTokenNotProvidedExceptionMapper : IExceptionMapper<JwtTokenNotProvidedException>
{
    public BadResponse Map(JwtTokenNotProvidedException exception)
    {
        return new BadResponse()
        {
            StatusCode = 402,
            ResponseDto = new
            {
                Title = "Jwt token not found",
                Detail = exception.Message
            }
        };
    }
}