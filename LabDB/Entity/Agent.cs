namespace LabDB.Entity;

public class Agent
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<LoadedApp> LoadedApps { get; set; }

    public Agent()
    {
        Login = string.Empty;
        Password = string.Empty;
        LoadedApps = new List<LoadedApp>();
    }

    public Agent(string login, string password)
    {
        Login = login;
        Password = password;
        LoadedApps = new List<LoadedApp>();
    }
}