﻿using LabDB.Entity;
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
        if (app is null || string.IsNullOrWhiteSpace(app.Name) ||
            !_context.Computers.Any(c => c.Id == app.ComputerId) ||
            !_context.Agents.Any(a => a.Id == app.AgentId)) return false;
        try
        {
            _context.Add(app);
            _context.SaveChanges();
            return true;
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public LabDB.Entity.Agent? AuthAgent(string login, string pass)
    {
        return _context.Agents.FirstOrDefault(e => e.Login == login && e.Passsword == pass);
    }

    public Agent? GetAgentById(int id)
    {
        return _context.Agents.FirstOrDefault(e => e.Id == id);
    }

    public Computer? GetComputerById(int id)
    {
        return _context.Computers.FirstOrDefault(h => h.Id == id);
    }
}
