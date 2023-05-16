using Microsoft.AspNetCore.Http;

namespace Infrastructure.CorrelationIdSystem.Repository;

public class CorrelationIdRepository : ICorrelationIdProvider, ICorrelationIdSaver
{
    private readonly IHttpContextAccessor _accessor;
    public string Key => "GATEWAY_CORRELATION_ID";

    public CorrelationIdRepository(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public Guid? GetCorrelationId()
    {
        HttpContext? context = _accessor.HttpContext;
        if (context is null)
        {
            return null;
        }

        object? rawId = context.Items[Key];
        if (rawId is null)
        {
            return null;
        }

        return (Guid)rawId;
    }

    public void GenerateAndSave()
    {
        Save(Guid.NewGuid());
    }

    private void Save(Guid newCorrelationId)
    {
        HttpContext? context = _accessor.HttpContext;
        if (context is null)
        {
            return;
        }

        context.Items[Key] = newCorrelationId;
    }
}