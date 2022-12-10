using LabDB.Entity;
using MainApp.Interfaces;

namespace MainApp.Services;

public class ClientService: IClientService
{
    private ApplicationContext _context;

    public ClientService(ApplicationContext context)
    {
        _context = context;
    }
    public IEnumerable<LoadedApp> GetAllApps()
    {
        throw new NotImplementedException();
    }
}