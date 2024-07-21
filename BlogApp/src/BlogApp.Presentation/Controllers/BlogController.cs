namespace BlogApp.Presentation.Controllers;

using Microsoft.AspNetCore.Mvc;

using BlogApp.Core.Blog.Models;
using BlogApp.Infrastructure.Blog.Queries;
using MediatR;
using BlogApp.Infrastructure.Blog.Commands;
using Microsoft.AspNetCore.Authorization;
using BlogApp.Infrastructure.UserTopic.Queries;

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

    [HttpGet("Blog/Index")]
    public async Task<IActionResult> Index(int userId)
    {
        try
        {
            var getAllBlogsByUserIdQuery = new GetAllByUserIdQuery()            {
                UserId = userId,
            };

                var blogs = await sender.Send(getAllBlogsByUserIdQuery);
                return View("Blog", blogs);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }

    [HttpGet("[controller]/{blogId}")]
    public async Task<IActionResult> GetBlog(int blogId)
    {
        try
        {
            var getBlogQuery = new GetByIdQuery()
            {
                Id = blogId,
            };

            var blog = await sender.Send(getBlogQuery);
            return View(blog);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


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
            return NotFound("Blog or image not found.");
        }
        var fileStream = System.IO.File.Open(blog.PictureUrl!, FileMode.Open);
        return File(fileStream, "image/jpeg");
    }



    [HttpPost("api/[controller]")]
    public async Task<IActionResult> CreateBlog([FromForm] Blog newBlog, IFormFile image)
    {
        try
        {
            var randomId = Random.Shared.Next(1, 100000);
            var getBlogQuery = new GetByIdQuery()
            {
                Id = randomId,
            };

            var blog = await sender.Send(getBlogQuery);
            if (blog != null){
                randomId = Random.Shared.Next(1, 100000);
            }
            newBlog.Id = randomId;

            var extension = new FileInfo(image.FileName).Extension[1..];
            newBlog.PictureUrl = $"Assets/BlogImg/{newBlog.Id}.{extension}";

            using var newFileStream = System.IO.File.Create(newBlog.PictureUrl);
            await image.CopyToAsync(newFileStream);


            var createCommand = new CreateCommand()
            {
                Title = newBlog.Title,
                Text = newBlog.Text,
                UserId = newBlog.UserId,
                TopicId = newBlog.TopicId,
                CreationDate = DateTime.Now,
                
            };

            await sender.Send(createCommand);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

