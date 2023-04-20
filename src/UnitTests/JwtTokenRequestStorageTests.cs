using Api.Middlewares.JwtParserMiddleware;
using Microsoft.AspNetCore.Http;

namespace UnitTests;

public class JwtTokenRequestStorageTests
{
    public HttpContext Context { get; }
    public JwtTokenRequestStorage Storage { get; }
    public string JwtWithBearer { get; } = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
    public string JwtWithoutBearer { get; } = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
    
    public JwtTokenRequestStorageTests()
    {
        Context = new DefaultHttpContext();
        Storage = new JwtTokenRequestStorage(Context);
    }

    [Theory]
    [InlineData("Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c")]
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c")]
    public void SaveJwtToken_JwtTokenWithAndWithoutBearerPrefix_JwtTokenPersistInItemsWithoutBearer(string jwtToken)
    {
        Storage.SaveJwtToken(jwtToken);

        string result = (string)Context.Items["jwtToken"]!;
        
        Assert.Equal(JwtWithoutBearer, result);
    }

    [Fact]
    public void ReadJwtToken_JwtTokenPersistedInContext_JwtTokenWithoutBearer()
    {
        Storage.SaveJwtToken(JwtWithBearer);

        string result = Storage.ReadJwtToken();
        
        Assert.Equal(JwtWithoutBearer, result);
    }
}