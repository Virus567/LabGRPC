using System.Collections.Generic;
using System.Linq;
using LabDB.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace ClientTests;

public class ClientAppTests
{
    private readonly Computer _testComputer;
    public ClientAppTests()
    {
        var a = new Agent {Id = 1, Login = "Test", Passsword = "Test"};

        var computer = new Computer() {Id = 1};
        var ind0 = new LoadedApp("Test1",a, computer);
        var ind1 = new LoadedApp("Test2",a, computer);
        computer.LoadedApps.Add(ind0);
        computer.LoadedApps.Add(ind1);

        _testComputer = computer;
    }
    [Test]
    public void GetLoadedAppSuccess()
    {
        var mock = new Mock<IClientRepository>();
        mock.Setup(s => s.GetAllApps()).Returns(_testComputer.LoadedApps);
        var controller = new HomeController(mock.Object);
        var actionResult = controller.Index();
        Assert.That(actionResult, Is.TypeOf<LoadedAppViewModel>());
        var viewResult = (ViewResult)actionResult;
        Assert.That(viewResult.ViewData.Model, Is.TypeOf<LoadedAppViewModel>());
        var model = (LoadedAppViewModel)viewResult.ViewData.Model!;
        Assert.That(model.LoadedApps.Count(), Is.EqualTo(_testComputer.LoadedApps.Count()));
    }
    [Test]
    public void GetLoadedAppServerError()
    {
        var mock = new Mock<IClientRepository>();
        mock.Setup(s => s.GetAllApps()).Returns(new List<LoadedApp>());
        var controller = new HomeController(mock.Object);
        var actionResult = controller.Index();
        Assert.That(actionResult, Is.TypeOf<LoadedAppViewModel>());
        var viewResult = (ViewResult)actionResult;
        Assert.That(viewResult.ViewData.Model, Is.TypeOf<LoadedAppViewModel>());
        var model = (LoadedAppViewModel)viewResult.ViewData.Model!;
        Assert.That(model.LoadedApps.Count(), Is.EqualTo(0));
    }
}