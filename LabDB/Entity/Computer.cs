namespace LabDB.Entity;

public class Computer
{
    public int Id { get; set; }
    public List<LoadedApp> LoadedApps { get; set; }

    public Computer()
    {
        LoadedApps = new List<LoadedApp>();
    }
    
}