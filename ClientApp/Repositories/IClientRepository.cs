using LabDB.Entity;

namespace WebApplication1.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<LoadedApp>> GetAllApps();
}