using System.Collections;
using LabDB.Entity;

namespace MainApp.Interfaces;

public interface IClientService
{
    IEnumerable<LoadedApp> GetAllApps();
}