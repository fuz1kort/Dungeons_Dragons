using System.Diagnostics;
using DungeonsDragons.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsDragons.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}