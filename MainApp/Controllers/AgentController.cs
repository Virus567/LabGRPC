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
   public AgentMessage Auth(AuthRequest request)
   {
      throw new NotImplementedException();
   }

   public override Task<AgentMessage> Auth(AuthRequest request, ServerCallContext context)
   {
      return Task.FromResult(Auth(request));
   }

   public NewResponse AddNewLoadedApp(NewRequest request)
   {
      throw new NotImplementedException();
   }

}