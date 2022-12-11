using MainApp.Controllers;
using MainApp.Interfaces;
using MainApp.Services;
using Microsoft.EntityFrameworkCore;

namespace MainApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddGrpc();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=test.db"));
        builder.Services.AddTransient<IAgentService, AgentService>();
        builder.Services.AddTransient<IClientService, ClientService>();

        var app = builder.Build();
        
        app.MapControllers();
        app.MapGrpcService<AgentController>();
        app.Run();
    }
}

