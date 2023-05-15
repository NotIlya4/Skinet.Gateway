using Infrastructure.CorrelationIdSystem.Repository;
using Serilog.Core;
using Serilog.Events;

namespace Infrastructure.CorrelationIdSystem.Serilog;

public class SerilogCorrelationIdEnricher : ILogEventEnricher
{
    private readonly ICorrelationIdProvider _provider;

    public SerilogCorrelationIdEnricher(ICorrelationIdProvider provider)
    {
        _provider = provider;
    }
    
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        Guid? id = _provider.GetCorrelationId();
        if (id.HasValue)
        {
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("CorrelationId", id.Value.ToString()));
        }
    }
}