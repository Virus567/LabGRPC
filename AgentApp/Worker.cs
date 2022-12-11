using AgentApp.Repository;
using Google.Protobuf.Reflection;
using LabDB.Entity;
using MainApp;

namespace AgentApp;

public class Worker : BackgroundService
{
    private readonly IAgentRopository _agentRopository;

    private static readonly List<string> _names = new (new[] {"Word", "Photoshop", "Excel", "Google", "Сапёр" });
    private Agent? _agent;

    public Worker(IAgentRopository agentRopository)
    {
        _agentRopository = agentRopository;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var res = await Auth(new AuthRequest { Login = "Test", Password = "Test" });
        if(res.Id ==-1)return;
        _agent = new Agent { Id = res.Id, Login = res.Login, Password = res.Password };
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var req = new NewRequest
                {
                    Name = _names[new Random().Next(0,_names.Count)], Computer = new Random().Next(-5, 10), NowAgent = _agent.Id
                };
                var resp = await AddNewLoadedApp(req);
                if (resp.Res)
                {
                    await Task.Delay(1800000, stoppingToken);
                }
                else
                    Console.WriteLine("Don`t added");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("null name");
            }
        }
    }

    public async Task<AgentMessage> Auth(AuthRequest request)
    {
    
        if (request is null || string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
        {
            return new AgentMessage() { Id = -1, Login = "", Password = "" };
        }
        return await _agentRopository.Auth(request);
    }

    public async Task<NewResponse> AddNewLoadedApp(NewRequest request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Name)|| request.Computer <= 0|| request.NowAgent <= 0) return new NewResponse() { Res = false };
        return await _agentRopository.AddNewLoadedApp(request);
    }
}