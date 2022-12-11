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
        var loadedApps = _context.LoadedApps.Include(a=>a.Computer).ToList();
        loadedApps = loadedApps.Select(l => 
        { 
            l.Computer = null;
            return l;
        }).ToList();
        return loadedApps;
        
    }
}