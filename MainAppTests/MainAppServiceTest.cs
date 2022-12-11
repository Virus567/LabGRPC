using System;
using System.Collections.Generic;
using LabDB.Entity;
using MainApp;
using MainApp.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace MainAppTests;

public class MainAppRepositoriesTest
{
    private ApplicationContext _context;

    public MainAppRepositoriesTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase($"ContextDb_{DateTime.Now.ToFileTimeUtc()}").Options;
        _context = new ApplicationContext(options,true);
        FillDb();
    }

    private void FillDb()
    {
        var testComputer = new Computer();
        var agent = new Agent {Id = 1, Login = "test", Password = "test"};
        _context.Agents.Add(agent);
        var ind0 = new LoadedApp("Приложение1", agent,testComputer);
        var ind1 = new LoadedApp("Приложение2", agent, testComputer);
        testComputer.LoadedApps.Add(ind0);
        testComputer.LoadedApps.Add(ind1);


        _context.Add(testComputer);
        _context.SaveChanges();
    }

    ~MainAppRepositoriesTest()
    {
        _context.Dispose();
    }

    [Test]
    public void GetAllLoadedApps()
    {
        var service = new ClientService(_context);
        Assert.That(service.GetAllApps(), Is.InstanceOf<IEnumerable<LoadedApp>>());
    }

    [Test]
    public void AddNewLoadedAppSuccess()
    {
        var service = new AgentService(_context);
        var computer = _context.Computers.FirstOrDefault();
        var agent = _context.Agents.FirstOrDefault();
        Assert.That(service.AddNewLoadedApp(new LoadedApp("Приложение1", agent, computer)), Is.True);
    }

    [Test]
    public void AddNewLoadedAppWithNullComputer()
    {
        var service = new AgentService(_context);
        Computer computer = null;
        var agent = _context.Agents.FirstOrDefault();
        Assert.Catch<NullReferenceException>(() =>
            service.AddNewLoadedApp(new LoadedApp("Приложение1", agent, computer)));
    }

    [Test]
    public void AddNewLoadedAppWithNullAgent()
    {
        var service = new AgentService(_context);
        var computer = _context.Computers.FirstOrDefault();
        Agent agent = null;
        Assert.Catch<NullReferenceException>(() =>
            service.AddNewLoadedApp(new LoadedApp("Приложение1", agent, computer)));
    }

    [TestCase("")]
    [TestCase(null)]
    public void AddNewLoadedAppWithErrorName(string name)
    {
        var service = new AgentService(_context);
        var computer = _context.Computers.FirstOrDefault();
        var agent = _context.Agents.FirstOrDefault();
        Assert.That(service.AddNewLoadedApp(new LoadedApp(name, agent, computer)), Is.False);
    }


    [TestCase("test", "test", ExpectedResult = false)]
    [TestCase("test1", "test", ExpectedResult = true)]
    [TestCase("", "test", ExpectedResult = true)]
    [TestCase("test", "", ExpectedResult = true)]
    [TestCase("", "", ExpectedResult = true)]
    [TestCase(null, "test", ExpectedResult = true)]
    [TestCase("test", null, ExpectedResult = true)]
    [TestCase(null, null, ExpectedResult = true)]
    public bool AuthWithErrorData(string login, string password)
    {
        var service = new AgentService(_context);
        return service.AuthAgent(login, password) is null;
    }
    
    [TestCase(1, ExpectedResult = false)]
    [TestCase(15, ExpectedResult = true)]
    public bool GetComputerById(int id)
    {
        var service = new AgentService(_context);
        return service.GetComputerById(id) is null;
    }

    [TestCase(1, ExpectedResult = false)]
    [TestCase(15, ExpectedResult = true)]
    public bool GetAgentById(int id)
    {
        var service = new AgentService(_context);
        return service.GetAgentById(id) is null;
    }
}