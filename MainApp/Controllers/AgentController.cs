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
      if (request is null || string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
         return new AgentMessage { Id = -1, Login = "", Password = "" };
      var agent = _agentService.AuthAgent(request.Login, request.Password);
      return agent is null
         ? new AgentMessage {Id = -1, Login = "", Password = ""}
         : new AgentMessage() {Id = agent.Id, Login = agent.Login, Password = agent.Password};
   }

   public override Task<AgentMessage> Auth(AuthRequest request, ServerCallContext context)
   {
      return Task.FromResult(Auth(request));
   }

   public NewResponse AddNewLoadedApp(NewRequest request)
   {
      if (request is null || request.Computer <= 0  || request.NowAgent <= 0 ||
          string.IsNullOrWhiteSpace(request.Name))
         return new NewResponse {Res = false};
      var computer = _agentService.GetComputerById(request.Computer);
      if (computer is null)
         return new NewResponse {Res = false};
      var emp = _agentService.GetAgentById(request.NowAgent);
      if (emp is null)
         return new NewResponse {Res = false};
      var ind = new LoadedApp(request.Name, emp, computer);
      var res = _agentService.AddNewLoadedApp(ind);
      return new NewResponse {Res = res};
   }

   public override Task<NewResponse> AddNewLoadedApp(NewRequest request, ServerCallContext context)
   {
      return Task.FromResult(AddNewLoadedApp(request));
   }
}