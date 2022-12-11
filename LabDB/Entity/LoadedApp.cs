using System.Text.Json.Serialization;

namespace LabDB.Entity;

public class LoadedApp
{
    [JsonPropertyName("id")]public int Id { get; set; }
    [JsonPropertyName("name")]public string Name { get; set; }
    [JsonPropertyName("dateTime")]public DateTime DateTime { get; set; }
    public Agent Agent { get; set; }
    [JsonPropertyName("agentId")]public int AgentId { get; set; }
    public Computer Computer { get; set; }
    [JsonPropertyName("computerId")]public int ComputerId { get; set; }

    public LoadedApp()
    {
        Name = string.Empty;
        DateTime = DateTime.Now;
        Agent = new Agent();
        Computer = new Computer();
    }

    public LoadedApp(string name, Agent agent, Computer computer)
    {
        Name = name;
        DateTime = DateTime.Now;
        Agent = agent;
        AgentId = agent.Id;
        Computer = computer;
        ComputerId = computer.Id;
    }
}