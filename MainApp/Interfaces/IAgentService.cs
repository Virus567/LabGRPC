using  LabDB.Entity;

namespace MainApp.Interfaces;

public interface IAgentService
{
    bool AddNewLoadedApp(LoadedApp app);
    Agent? AuthAgent(string login, string pass);
    
    Agent? GetAgentById(int id);
    Computer? GetComputerById(int id);
}