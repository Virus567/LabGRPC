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
    private readonly IEnumerable<LoadedApp> _loadedApps ;

    public ClientAppRepositoryTests()
    {
        var a = new Agent { Id = 1, Login = "Test", Password = "Test" };
        List<LoadedApp> loadedApps = new List<LoadedApp>();
        var computer = new Computer() { Id = 1 };
        var ind0 = new LoadedApp("Test1", a, computer);
        var ind1 = new LoadedApp("Test2", a, computer);
        loadedApps.Add(ind0);
        loadedApps.Add(ind1);
        _loadedApps = loadedApps;
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
    public async Task GetLoadedAppsHttpSuccess()
    {
        var clientHandlerStub = new DelegatingHandlerStub(_loadedApps);
        var client = new HttpClient(clientHandlerStub);
        var mock = new Mock<IHttpClientFactory>();
        mock.Setup(r => r.CreateClient(It.IsAny<string>())).Returns(client);
        var service = new ClientService(mock.Object);
        var res = await service.GetAllApps();
        Assert.That(res.Count(), Is.EqualTo(_loadedApps.Count()));
    }

    [Test]
    public async Task GetLoadedAppsHttpError()
    {
        var client = new HttpClient();
        var mock = new Mock<IHttpClientFactory>();
        mock.Setup(r => r.CreateClient(It.IsAny<string>())).Returns(client);
        var service = new ClientService(mock.Object);
        var res = await service.GetAllApps();
        Assert.That(res.Count(), Is.EqualTo(0));
    }
}