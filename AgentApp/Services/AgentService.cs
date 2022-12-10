using AgentApp.Repository;
using MainApp;

namespace AgentApp.Services;

public class AgentService : IAgentRopository
{
    public async Task<AgentMessage> Auth(AuthRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<NewResponse> AddNewLoadedApp(NewRequest request)
    {
        throw new NotImplementedException();
    }
}