using System.Collections.Generic;
using System.Linq;
using LabDB.Entity;
using MainApp.Interfaces;

namespace gRPCTests.Services;

public class TestAgentService:IAgentService
{
    private static readonly Agent _agent = new() { Login = "test", Passsword = "test" };

    private static readonly List<Computer> _computers = Enumerable.Range(1, 10).Select(r => new Computer { Id = r }).ToList();
    
    public bool AddNewLoadedApp(LoadedApp app)
    {
        if (app is null || app.Name == "") return false;
        return _computers.Any(h=>h.Id== app.ComputerId);
    }

    public Agent? AuthAgent(string login, string pass)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass)) return null;
        if( login == _agent.Login && pass == _agent.Passsword) return _agent;
        return null;
    }

    public Agent? GetAgentById(int id)
    {
        return id == _agent.Id ? _agent : null;
    }

    public Computer? GetComputerById(int id)
    {
        return _computers.FirstOrDefault(h => h.Id == id);
    }
}