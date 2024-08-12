using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MotoRental.Admin.Models;

namespace MotoRental.Admin.Controllers;

public class HomeController2 : Controller
{
    private readonly ILogger<HomeController2> _logger;

    public HomeController2(ILogger<HomeController2> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
