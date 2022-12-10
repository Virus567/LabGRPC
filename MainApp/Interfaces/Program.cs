using MainApp.Controllers;
using MainApp.Interfaces;
using MainApp.Services;

namespace MainApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddGrpc();
        builder.Services.AddControllers();
        builder.Services.AddTransient<IAgentService, AgentService>();
        builder.Services.AddTransient<IClientService, ClientService>();

        var app = builder.Build();
        
        app.MapControllers();
        app.MapGrpcService<AgentController>();
        //app.UseEndpoints(e => e.MapControllers());
        app.Run();
    }
}

