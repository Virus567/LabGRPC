using LabDB.Entity;

namespace WebApplication1.Models;

public class LoadedAppViewModel
{
    public IEnumerable<LoadedApp> LoadedApps { get; set; }

    public LoadedAppViewModel()
    {
        LoadedApps = new List<LoadedApp>();
    }

    public LoadedAppViewModel(IEnumerable<LoadedApp> loadedApps)
    {
        LoadedApps = loadedApps;
    }
}