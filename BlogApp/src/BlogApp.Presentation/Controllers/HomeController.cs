using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Presentation.Models;


namespace BlogApp.Presentation.Controllers;

public class HomeController : Controller
{
    

    public IActionResult Index()
    {
        
        return base.RedirectToRoute("LoginView");
    }

   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
