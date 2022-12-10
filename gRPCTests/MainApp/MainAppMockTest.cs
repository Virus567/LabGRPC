using System.Collections.Generic;
using System.Linq;
using LabDB.Entity;
using MainApp;
using MainApp.Controllers;
using MainApp.Interfaces;
using Moq;
using NUnit.Framework;

namespace Tests.MainApp;

public class MainAppMockTest
{
    private readonly Computer _testComputer;

    public MainAppMockTest()
    {
        var a = new Agent {Id = 1, Login = "Test", Passsword = "Test"};

        var computer = new Computer() {Id = 1};
        var ind0 = new LoadedApp("Test1",a, computer);
        var ind1 = new LoadedApp("Test2",a, computer);
        computer.LoadedApps.Add(ind0);
        computer.LoadedApps.Add(ind1);

        _testComputer = computer;
    }

    [Test]
    public void GetAllAppsSuccess()
    {
        var mock = new Mock<IClientService>();
        mock.Setup(r => r.GetAllApps()).Returns(_testComputer.LoadedApps);
        var clientController = new ClientController(mock.Object);
        var result = clientController.GetLoadedApps();
        Assert.That(result.Count(), Is.EqualTo(_testComputer.LoadedApps.Count()));
    }

    [Test]
    public void AddNewLoadedAppSuccess()
    {
        var mock = new Mock<IAgentService>();
        mock.Setup(r => r.AddNewLoadedApp(It.IsNotNull<LoadedApp>())).Returns(true);
        var agentController = new AgentController(mock.Object);
        var res = agentController.AddNewLoadedApp(new NewRequest());
        Assert.That(res, Is.True);
    }

    [Test]
    public void AddNewLoadedAppWithNull()
    {
        var mock = new Mock<IAgentService>();
        mock.Setup(r => r.AddNewLoadedApp(It.IsNotNull<LoadedApp>())).Returns(true);
        var agentController = new AgentController(mock.Object);
        var res = agentController.AddNewLoadedApp(null);
        Assert.That(res, Is.False);
    }

    [Test]
    public void AddNewLoadedAppWithErrorComputer()
    {
        var mock = new Mock<IAgentService>();
        mock.Setup(r => r.AddNewLoadedApp(It.Is<LoadedApp>(i => i.ComputerId > 0))).Returns(true);
        var agentController = new AgentController(mock.Object);
        var res = agentController.AddNewLoadedApp(new NewRequest{Computer = -1});
        Assert.That(res, Is.False);
    }

    [Test]
    public void AddNewLoadedAppWithErrorAgent()
    {
        var mock = new Mock<IAgentService>();
        mock.Setup(r => r.AddNewLoadedApp(It.Is<LoadedApp>(i => i.AgentId> 0))).Returns(true);
        var agentController = new AgentController(mock.Object);
        var res = agentController.AddNewLoadedApp(new NewRequest {NowAgent = -1});
        Assert.That(res, Is.False);
    }

    [Test]
    public void AddNewLoadedAppWithErrorName()
    {
        var mock = new Mock<IAgentService>();
        mock.Setup(r => r.AddNewLoadedApp(It.Is<LoadedApp>(i => i.Name !=""))).Returns(true);
        var agentController = new AgentController(mock.Object);
        var res = agentController.AddNewLoadedApp(new NewRequest {Name = ""});
        Assert.That(res, Is.False);
    }
    

    [Test]
    public void AuthSuccess()
    {
        var mock = new Mock<IAgentService>();
        mock.Setup(r => r.AuthAgent(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new Agent {Id = 1, Login = "Test", Passsword = "Test"});
        var agentController = new AgentController(mock.Object);
        var res = agentController.Auth(new AuthRequest{Login = "Test", Password = "Test"});
        Assert.That(res, Is.Not.Null);
    }

    [TestCase("   ", "test")]
    [TestCase("test", "    ")]
    [TestCase("   ", "    ")]
    [TestCase(null, "test")]
    [TestCase("test", null)]
    [TestCase(null, null)]
    public void AuthWithErrorData(string login, string password)
    {
        var mock = new Mock<IAgentService>();
        mock.Setup(r => r.AuthAgent(It.Is<string>(s => !string.IsNullOrWhiteSpace(s)),
                It.Is<string>(s => !string.IsNullOrWhiteSpace(s))))
            .Returns(new Agent {Id = 1, Login = "Test", Passsword = "Test"});
        var agentController = new AgentController(mock.Object);
        var res = agentController.Auth(new AuthRequest{Login = login, Password = password});
        Assert.That(res, Is.Null);
    }
}