using MainApp;

namespace AgentApp.Repository;

public interface IAgentRopository
{
    public Task<AgentMessage> Auth(AuthRequest request);


    public Task<NewResponse> AddNewLoadedApp(NewRequest request);

}