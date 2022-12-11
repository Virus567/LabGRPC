using System.Text.Json.Serialization;

namespace LabDB.Entity;

public class Agent
{
    [JsonPropertyName("id")]public int Id { get; set; }
    [JsonPropertyName("login")]public string Login { get; set; }
    [JsonPropertyName("password")]public string Password { get; set; }
    [JsonPropertyName("loadedApps")]public List<LoadedApp> LoadedApps { get; set; }

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