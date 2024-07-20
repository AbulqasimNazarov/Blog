namespace BlogApp.Presentation.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using BlogApp.Infrastructure.Topic.Queries;
using BlogApp.Core.Topic.Models;
using System.Text.Json;
using BlogApp.Infrastructure.UserTopic.Commands;
using BlogApp.Infrastructure.UserTopic.Queries;

public class TopicController : Controller
{
    private readonly ISender sender;

    public TopicController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet("api/[controller]")]
    public async Task<IActionResult> ChooseTags()
    {
        try
        {
            var getAllQuery = new GetAllQuery();
            var allTopics = await sender.Send(getAllQuery);

            return View(allTopics);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("[controller]/[action]/{userId}")]
    public async Task<IActionResult> CreatePreferences(int userId)
    {
        try
        {
            var topicsJson = this.HttpContext.Request.Headers["topics"];
            var topics = JsonSerializer.Deserialize<IEnumerable<Topic?>?>(topicsJson!);
            
            var createListCommand = new CreateListCommand()
            {
                Topics = topics,
                UserId = userId
            };

            await sender.Send(createListCommand);

            return RedirectToAction(controllerName: "Blog", actionName: "Index");
        }
        catch (Exception)
        {
            return RedirectToAction(controllerName: "Topic", actionName: "ChooseTags");
        }
    }

    [HttpGet("[controller]/GetSelectedTopics")]
    public async Task<ActionResult<IEnumerable<Topic>>> GetSelectedTopics(int userId)
    {
        try
        {
            var getSelectedTopicsQuery = new GetAllTopicsByUserIdQuery
            {
                UserId = userId
            };
            var topics = await sender.Send(getSelectedTopicsQuery);
            return Ok(topics);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}
