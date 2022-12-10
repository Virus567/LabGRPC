namespace LabDB.Entity;

public class LoadedApp
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateTime { get; set; }
    public Agent Agent { get; set; }
    public int AgentId { get; set; }
    public Computer Computer { get; set; }
    public int ComputerId { get; set; }

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