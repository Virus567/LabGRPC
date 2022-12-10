using AgentApp.Repository;
using MainApp;

namespace AgentApp;

public class Worker : BackgroundService
{
    private readonly IAgentRopository _agentRopository;

    public Worker(IAgentRopository agentRopository)
    {
        _agentRopository = agentRopository;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            throw new NotImplementedException();
        }
    }

    public async Task<AgentMessage> Auth(AuthRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<NewResponse> AddNewLoadedApp(NewRequest request)
    {
        throw new NotImplementedException();
    }
}