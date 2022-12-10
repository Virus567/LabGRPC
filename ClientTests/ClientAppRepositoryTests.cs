using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LabDB.Entity;
using Moq;
using NUnit.Framework;
using WebApplication1.Services;

namespace ClientTests;

public class ClientAppRepositoryTests
{
    private readonly Computer _testComputer;

    public ClientAppRepositoryTests()
    {
        var a = new Agent { Id = 1, Login = "Test", Passsword = "Test" };

        var computer = new Computer() { Id = 1 };
        var ind0 = new LoadedApp("Test1", a, computer);
        var ind1 = new LoadedApp("Test2", a, computer);
        computer.LoadedApps.Add(ind0);
        computer.LoadedApps.Add(ind1);

        _testComputer = computer;
    }

    public class DelegatingHandlerStub : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;

        public DelegatingHandlerStub()
        {
            _handlerFunc = (request, cancellationToken) =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.RequestMessage = request;
                var res = JsonSerializer.Serialize(new List<LoadedApp>());
                response.Content = new StringContent(res);
                return Task.FromResult(response);
            };
        }

        public DelegatingHandlerStub(IEnumerable<LoadedApp> testLoadedApps)
        {
            _handlerFunc = (request, cancellationToken) =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.RequestMessage = request;
                var res = JsonSerializer.Serialize(testLoadedApps);
                response.Content = new StringContent(res);
                return Task.FromResult(response);
            };
        }

        public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }
    }

    [Test]
    public void GetLoadedAppsHttpSuccess()
    {
        var clientHandlerStub = new DelegatingHandlerStub(_testComputer.LoadedApps);
        var client = new HttpClient(clientHandlerStub);
        var mock = new Mock<IHttpClientFactory>();
        mock.Setup(r => r.CreateClient(It.IsAny<string>())).Returns(client);
        var service = new ClientService(mock.Object);
        var res = service.GetAllApps();
        Assert.That(res.Count(), Is.EqualTo(_testComputer.LoadedApps.Count()));
    }

    [Test]
    public void GetLoadedAppsHttpError()
    {
        var client = new HttpClient();
        var mock = new Mock<IHttpClientFactory>();
        mock.Setup(r => r.CreateClient(It.IsAny<string>())).Returns(client);
        var service = new ClientService(mock.Object);
        var res = service.GetAllApps();
        Assert.That(res.Count(), Is.EqualTo(0));
    }
}