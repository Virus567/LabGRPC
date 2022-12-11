using System.Text.Json.Serialization;

namespace LabDB.Entity;

public class Computer
{
    [JsonPropertyName("id")]public int Id { get; set; }
    [JsonPropertyName("loadedApps")]public List<LoadedApp> LoadedApps { get; set; }

    public Computer()
    {
        LoadedApps = new List<LoadedApp>();
    }
    
}