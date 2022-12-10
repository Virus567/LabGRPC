using LabDB.Entity;
using MainApp.Interfaces;

namespace MainApp.Services;

public class AgentService : IAgentService
{
    private ApplicationContext _context;

    public AgentService(ApplicationContext context)
    {
        _context = context;
    }
    public bool AddNewLoadedApp(LoadedApp app)
    {
        throw new NotImplementedException();
    }

    public LabDB.Entity.Agent? AuthAgent(string login, string pass)
    {
        throw new NotImplementedException();
    }

    public Agent? GetAgentById(int id)
    {
        throw new NotImplementedException();
    }

    public Computer? GetComputerById(int id)
    {
        throw new NotImplementedException();
    }
}
