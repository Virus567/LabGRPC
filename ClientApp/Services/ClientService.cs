using System.Text.Json;
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
    public async Task<IEnumerable<LoadedApp>> GetAllApps()
    {
        var client = _httpClientFactory.CreateClient();
        try
        {
            var responseMessage = await client.GetAsync("http://localhost:5173/client/get");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<IEnumerable<LoadedApp>>(await responseMessage.Content
                    .ReadAsStringAsync())??Enumerable.Empty<LoadedApp>();
            }
            return Enumerable.Empty<LoadedApp>();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return Enumerable.Empty<LoadedApp>();
        }
        
    }
}