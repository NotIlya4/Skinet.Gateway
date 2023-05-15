namespace Infrastructure.Auther.Exceptions;

public class JwtTokenNotProvidedException : Exception
{
    public JwtTokenNotProvidedException() : base("Jwt token must be provided for this endpoint")
    {
        
    }
}