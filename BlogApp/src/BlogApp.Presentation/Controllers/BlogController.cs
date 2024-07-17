namespace BlogApp.Presentation.Controllers;

using Microsoft.AspNetCore.Mvc;

using BlogApp.Core.Blog.Models;
using BlogApp.Infrastructure.Blog.Queries;
using MediatR;

public class BlogController : Controller
{
    private readonly ISender sender;

    public BlogController(ISender sender)
    {
        this.sender = sender;
    }


    [HttpGet("api/[controller]")]
    public async Task<IEnumerable<Blog?>?> SearchBlogsByName(string? name)
    {
        try
        {
            var getAllByName = new GetAllByNameQuery()
            {
                Name = name
            };

            var foundBlogs = await sender.Send(getAllByName);

            return foundBlogs;
        }
        catch (Exception)
        {   
            return null;
        }
    }

    [HttpGet("api/[action]")]
    public async Task<IEnumerable<Blog?>?> GetBlogsByTopic(int topicId)
    {
        try
        {
            var getAllByTopicIdQuery = new GetAllByTopicIdQuery
            {
                TopicId = topicId
            };

            var blogs = await sender.Send(getAllByTopicIdQuery);
            
            return blogs;
        }
        catch (Exception)
        {
            return null;
        }
    }


    // [HttpGet]
    // [Route("/Blog/GetBlogsByTopicLoginned")]
    // public async Task<IActionResult> GetBlogsByTopicLoginned(){

    //     return View();
    // }




    [HttpGet("[controller]/[action]/{id}")]
    public async Task<IActionResult> Image(int id)
    {
        var blogQuery = new GetByIdQuery
        {
            Id = id,
        };
        var blog = await this.sender.Send(blogQuery);
        if (blog == null || string.IsNullOrEmpty(blog.PictureUrl))
        {
            return NotFound("Film or image not found.");
        }
        var fileStream = System.IO.File.Open(blog.PictureUrl!, FileMode.Open);
        return File(fileStream, "image/jpeg");
    }



    // [HttpPost]
    // [Route("[controller]/[action]")]
    // public async Task<IActionResult> Add([FromForm] Blog newBlog, IFormFile image)
    // {
    //     if (newBlog == null || image == null)
    //     {
    //         return BadRequest("Invalid blog data or image.");
    //     }

    //     await blogService.CreateBlogAsync(newBlog, image);
    //     return base.RedirectToAction(controllerName: "Blog", actionName: "FilterBlogsByTopics");
    // }
}

