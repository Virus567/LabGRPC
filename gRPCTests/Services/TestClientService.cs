using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LabDB.Entity;
using MainApp.Interfaces;

namespace gRPCTests.Services;

public class TestClientService: IClientService
{
    public IEnumerable<LoadedApp> GetAllApps()
    {
        return Enumerable.Range(0, 2).Select(_ => new LoadedApp());
    }
}