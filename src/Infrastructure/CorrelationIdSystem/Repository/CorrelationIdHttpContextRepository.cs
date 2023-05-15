using Microsoft.AspNetCore.Http;

namespace Infrastructure.CorrelationIdSystem.Repository;

public class CorrelationIdHttpContextRepository : ICorrelationIdProvider, ICorrelationIdSaver
{
    private const string Key = "GATEWAY_CORRELATIONAL_ID";

    public Guid? GetCorrelationId()
    {
        HttpContext? context = Context();
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
        HttpContext? context = Context();
        if (context is null)
        {
            return;
        }

        context.Items[Key] = newCorrelationId;
    }

    private HttpContext? Context()
    {
        return new HttpContextAccessor().HttpContext;
    }
}