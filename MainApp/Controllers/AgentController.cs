using Grpc.Core;
using LabDB.Entity;
using MainApp.Interfaces;

namespace MainApp.Controllers;

public class AgentController: AgentProtoService.AgentProtoServiceBase
{
   private readonly IAgentService _agentService;

   public AgentController(IAgentService agentService)
   {
      _agentService = agentService;
   }

   public override Task<AgentMessage> Auth(AuthRequest request, ServerCallContext context)
   {
      var a = _agentService.AuthAgent(request.Login, request.Password);
      if (a is null) return Task.FromResult(new AgentMessage());
      return Task.FromResult(new AgentMessage
      {
         Id = a.Id, Login = a.Login, Password = a.Passsword
      });
   }

   public override Task<NewResponse> AddNewLoadedApp(NewRequest request, ServerCallContext context)
   {
      var r = new LoadedApp(request.Name, _agentService.GetAgentById(request.NowAgent), _agentService.GetComputerById(request.Computer));
      return Task.FromResult(new NewResponse
      {
         Res = _agentService.AddNewLoadedApp(r)
      });
   }

}