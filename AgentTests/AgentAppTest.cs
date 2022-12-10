using System;
using System.Threading.Tasks;
using AgentApp;
using AgentApp.Repository;
using MainApp;
using Moq;
using NUnit.Framework;

namespace AgentAppTests.AgentApp;

public class AgentAppTest
{
    [Test]
    public async Task AuthSuccess()
    {
        var mock = new Mock<IAgentRopository>();
        mock.Setup(s => s.Auth(It.IsNotNull<AuthRequest>())).Returns(Task.FromResult(new AgentMessage()
            {Id = 1, Login = "test", Password = "test"}));
        var worker = new Worker(mock.Object);
        var res = await worker.Auth(new AuthRequest {Login = "test", Password = "test"});
        Assert.That(res.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task AuthWithNull()
    {
        var mock = new Mock<IAgentRopository>();
        mock.Setup(s => s.Auth(It.Is<AuthRequest>(r => r == null))).Returns(Task.FromResult(new  AgentMessage()
            {Id = -1, Login = "", Password = ""}));
        var worker = new Worker(mock.Object);
        var res = await worker.Auth(null);
        Assert.That(res.Id, Is.EqualTo(-1));
    }

    [TestCase("  ", "test")]
    [TestCase("test", "  ")]
    [TestCase(null, "test")]
    [TestCase("test", null)]
    [TestCase(null, null)]
    public async Task AuthWithErrorData(string login, string password)
    {
        var mock = new Mock<IAgentRopository>();
        mock.Setup(s =>
            s.Auth(It.Is<AuthRequest>(
                r => !string.IsNullOrWhiteSpace(r.Login) || !string.IsNullOrWhiteSpace(r.Password)))).Returns(
            Task.FromResult(new  AgentMessage()
                {Id = -1, Login = "", Password = ""}));
        var worker = new Worker(mock.Object);
        var res = await worker.Auth(new AuthRequest{Login = login, Password = password});
        Assert.That(res.Id, Is.EqualTo(-1));
    }

    [Test]
    public async Task AddNewLoadedAppSuccess()
    {
        var mock = new Mock<IAgentRopository>();
        mock.Setup(s => s.AddNewLoadedApp(It.IsNotNull<NewRequest>()))
            .Returns(Task.FromResult(new NewResponse {Res = true}));
        var worker = new Worker(mock.Object);
        var res = await worker.AddNewLoadedApp(new NewRequest
            {Computer = 1, NowAgent = 1, Name = "Test"});
        Assert.That(res.Res, Is.True);
    }

    [Test]
    public async Task AddNewLoadedAppWithNull()
    {
        var mock = new Mock<IAgentRopository>();
        mock.Setup(s => s.AddNewLoadedApp(It.Is<NewRequest>(r => r == null)))
            .Returns(Task.FromResult(new NewResponse {Res = false}));
        var worker = new Worker(mock.Object);
        var res = await worker.AddNewLoadedApp(null);
        Assert.That(res.Res, Is.False);
    }

    [Test]
    public async Task AddNewLoadedAppWithErrorComputer()
    {
        var mock = new Mock<IAgentRopository>();
        mock.Setup(s => s.AddNewLoadedApp(It.Is<NewRequest>(r => r.Computer > 0)))
            .Returns(Task.FromResult(new NewResponse {Res = true}));
        var worker = new Worker(mock.Object);
        var res = await worker.AddNewLoadedApp(new NewRequest
            {Computer = -1, NowAgent = 1, Name = "Test"});
        Assert.That(res.Res, Is.False);
    }

    [Test]
    public async Task AddNewLoadedAppWithErrorAgent()
    {
        var mock = new Mock<IAgentRopository>();
        mock.Setup(s => s.AddNewLoadedApp(It.Is<NewRequest>(r => r.NowAgent > 0)))
            .Returns(Task.FromResult(new NewResponse {Res = true}));
        var worker = new Worker(mock.Object);
        var res = await worker.AddNewLoadedApp(new NewRequest
            {Computer = 1, NowAgent = -1, Name= "Test"});
        Assert.That(res.Res, Is.False);
    }

    

    [TestCase("")]
    [TestCase(null)]
    public async Task AddNewLoadedAppWithErrorName(string name)
    {
        var mock = new Mock<IAgentRopository>();
        mock.Setup(s => s.AddNewLoadedApp(It.Is<NewRequest>(r => !string.IsNullOrWhiteSpace(r.Name))))
            .Returns(Task.FromResult(new NewResponse {Res = true}));
        var worker = new Worker(mock.Object);
        var res = await worker.AddNewLoadedApp(new NewRequest
            {Computer = 1, NowAgent = 1, Name = name});
        Assert.That(res.Res, Is.False);
    }
}
