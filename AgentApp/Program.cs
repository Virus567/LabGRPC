using AgentApp;
using AgentApp.Repository;
using AgentApp.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<IAgentRopository, AgentService>(); services.AddHostedService<Worker>(); })
    .Build();

await host.RunAsync();