using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Presentation.Models;
using BlogApp.Core.Models;

namespace BlogApp.Presentation.Controllers;

public class TopicController : Controller
{
    
    [HttpGet]
    public IActionResult ChooseTags()
    {
        
        return View();
    }

    
}
