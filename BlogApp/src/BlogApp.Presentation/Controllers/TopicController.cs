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
    public async Task<IActionResult> CreatePreferences([FromBody]IEnumerable<int> topics, int userId)
    {
        try
        {  
            // var topicTasks = topics.Select(async id => await sender.Send(new GetByIdQuery { Id = id }));
            // var topicList = await Task.WhenAll(topicTasks);

            // var validTopics = topicList.Where(topic => topic != null).ToList();

            var createListCommand = new CreateListCommand()
            {
                Topics = topics,
                UserId = userId
            };

            await sender.Send(createListCommand);

            return RedirectToAction("Index", "Blog", new { userId });
        }
        catch (Exception)
        {
            return RedirectToAction(controllerName: "Topic", actionName: "ChooseTags");
        }
    }
}
