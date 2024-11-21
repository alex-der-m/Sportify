using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportify_back.Models;
using Sportify_Back.Models;

namespace Sportify_Back.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly SportifyDbContext _context;

    public HomeController(SportifyDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Welcome()
    {
        if (User.Identity.IsAuthenticated)  // Verifica si el usuario está autenticado
        {
            return RedirectToAction("Index", "Home"); // Redirige a Home/Index si está logeado
        }

        return View(); 
    }
    
    public IActionResult Index()
    {
        if (!User.Identity.IsAuthenticated) 
        {
            return RedirectToAction("Welcome", "Home"); 
        }
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

    [HttpGet]
    public JsonResult GetActivities()
    {
        var activities = _context.Classes
            .Where(a => a.Active)
            .Select(a => new {
                Title = a.Activities.NameActivity,
                Date = a.Sched.ToString("dd/MM/yyyy"),
                Day = a.Sched.DayOfWeek.ToString(),
                Time = a.Sched.ToString("HH:mm"),
                Teacher = a.Teachers.Name,
                Cupo = a.Quota
            })
            .ToList();

        return Json(activities);
    }
}
