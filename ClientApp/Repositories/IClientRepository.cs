using LabDB.Entity;

namespace WebApplication1.Repositories;

public interface IClientRepository
{
    IEnumerable<LoadedApp> GetAllApps();
}