using LabDB.Entity;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class ClientService : IClientRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public IEnumerable<LoadedApp> GetAllApps()
    {
        throw new NotImplementedException();
    }
}