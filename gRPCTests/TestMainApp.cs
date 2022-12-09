using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using gRPCTests.Services;
using LabDB.Entity;
using MainApp;
using MainApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;


namespace gRPCTests;

public class TestMainApp
{

    [Test] public async Task GetAllApps()
    {
        using var server = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                ServiceDescriptor serviceDescriptor =
                    new(typeof(IClientService), typeof(TestClientService), ServiceLifetime.Transient);
                services.Remove(serviceDescriptor);
                services.AddTransient<IClientService>(s => new TestClientService());
            });
        });
        using var client = server.CreateClient();
        var res = await client.GetFromJsonAsync<IEnumerable<LoadedApp>>("/client/get");
        Assert.That(res.Count(), Is.EqualTo(2));
    }
    
    [Test] public async Task AuthAgentSuccess()
    {
        using var server = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                ServiceDescriptor serviceDescriptor =
                    new(typeof(IClientService), typeof(TestClientService), ServiceLifetime.Transient);
                services.Remove(serviceDescriptor);
                services.AddTransient<IClientService>(s => new TestClientService());
            });
        });
        using var client = server.CreateClient();
        var res = await client.GetFromJsonAsync<IEnumerable<LoadedApp>>("/client/get");
        Assert.That(res.Count(), Is.EqualTo(2));
    }
}