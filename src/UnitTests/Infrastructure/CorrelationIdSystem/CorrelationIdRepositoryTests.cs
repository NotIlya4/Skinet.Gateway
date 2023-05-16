using HttpContextMoq;
using Infrastructure.CorrelationIdSystem.Repository;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.Infrastructure.CorrelationIdSystem;

public class CorrelationIdRepositoryTests
{
    private readonly CorrelationIdRepository _repository;
    private readonly HttpContext _httpContext;
    
    public CorrelationIdRepositoryTests()
    {
        _httpContext = new DefaultHttpContext();
        var accessor = new Mock<IHttpContextAccessor>();
        
        accessor.Setup(a => a.HttpContext).Returns(_httpContext);
        _repository = new CorrelationIdRepository(accessor.Object);
    }

    [Fact]
    public void GenerateAndSave_EmptyHttpContext_ContextWithGuidByKey()
    {
        _repository.GenerateAndSave();

        Guid result = (Guid)_httpContext.Items[_repository.Key]!;
        
        Assert.NotNull(result);
    }

    [Fact]
    public void GetCorrelationId_EmptyHttpContext_Null()
    {
        Guid? result = _repository.GetCorrelationId();
        
        Assert.Null(result);
    }

    [Fact]
    public void GetCorrelationId_FirstGenerateThenGet_NotNull()
    {
        _repository.GenerateAndSave();
        Guid? result = _repository.GetCorrelationId();
        
        Assert.NotNull(result);
    }
}