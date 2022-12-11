using AgentApp.Repository;
using Grpc.Net.Client;
using MainApp;

namespace AgentApp.Services;

public class AgentService : IAgentRopository
{
    public async Task<AgentMessage> Auth(AuthRequest request)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5174");
        var client = new AgentProtoService.AgentProtoServiceClient(channel);
        return await client.AuthAsync((request));
    }

    public async Task<NewResponse> AddNewLoadedApp(NewRequest request)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5174");
        var client = new AgentProtoService.AgentProtoServiceClient(channel);
        return await client.AddNewLoadedAppAsync(request);
    }
}