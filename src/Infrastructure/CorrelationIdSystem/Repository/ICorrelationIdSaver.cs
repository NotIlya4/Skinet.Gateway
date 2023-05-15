namespace Infrastructure.CorrelationIdSystem.Repository;

public interface ICorrelationIdSaver
{
    public void GenerateAndSave();
}