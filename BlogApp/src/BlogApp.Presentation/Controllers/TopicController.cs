using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Presentation.Models;
using MediatR;
using BlogApp.Infrastructure.Topic.Queries;
// using BlogApp.Core.Topic.Services.Base;


namespace BlogApp.Presentation.Controllers;

public class TopicController : Controller
{
    private readonly ISender sender;

    public TopicController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> ChooseTags(GetAllQuery getAllQuery)
    {
        var getTopics = await sender.Send(getAllQuery);
        
        return View(getTopics);
    }

    
}
