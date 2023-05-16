using Infrastructure.CorrelationIdSystem.Repository;
using Infrastructure.CorrelationIdSystem.Serilog;
using Microsoft.AspNetCore.Http;
using Moq;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.TestCorrelator;

namespace UnitTests.Infrastructure.CorrelationIdSystem;

public class SerilogCorrelationIdEnricherTests
{
    private readonly ILogger _logger;
    private readonly CorrelationIdRepository _correlationIdRepository;
    
    public SerilogCorrelationIdEnricherTests()
    {
        Mock<IHttpContextAccessor> accessor = new Mock<IHttpContextAccessor>();
        HttpContext context = new DefaultHttpContext();
        accessor.Setup(a => a.HttpContext).Returns(context);
        _correlationIdRepository = new CorrelationIdRepository(accessor.Object);
        _logger = new LoggerConfiguration()
            .WriteTo.TestCorrelator()
            .Enrich.With(new SerilogCorrelationIdEnricher(_correlationIdRepository))
            .CreateLogger();
    }

    [Fact]
    public void Log_CallGenerateCorrelationId_LogWithProvidedId()
    {
        _correlationIdRepository.GenerateAndSave();

        using (TestCorrelator.CreateContext())
        {
            _logger.Information("asdasd");
            LogEventPropertyValue property = TestCorrelator.GetLogEventsFromCurrentContext().Single().Properties["CorrelationId"];
            string raw = property.ToString().Replace("\"", "");
            new Guid(raw);
        }
    }

    [Fact]
    public void Log_NoCorrelationIdInHttpContext_LogWithoutId()
    {
        using (TestCorrelator.CreateContext())
        {
            _logger.Information("asdasd");
            LogEvent log = TestCorrelator.GetLogEventsFromCurrentContext().Single();
            Assert.Throws<KeyNotFoundException>(() => log.Properties["CorrelationId"]);
        }
    }
}