namespace Infrastructure.CorrelationIdSystem.Repository;

public interface ICorrelationIdProvider
{
    public Guid? GetCorrelationId();
}