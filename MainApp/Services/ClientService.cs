using LabDB.Entity;
using MainApp.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        return _context.LoadedApps.Include(a=>a.Computer).Include(a=>a.Agent);
    }
}