using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly IClientRepository _clientRepository;

    public HomeController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    public async Task<IActionResult> Index()
    {
        return View(new LoadedAppViewModel { LoadedApps = await _clientRepository.GetAllApps() });
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}