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


    [HttpGet("api/[controller]/SearchBlogsByName")]
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

    [HttpGet("api/GetBlogsByTopic/")]
    public async Task<ActionResult<IEnumerable<Blog>>> GetBlogsByTopic(int topicId)
    {
        try
        {
            var getAllByTopicIdQuery = new GetAllByTopicIdQuery
            {
                TopicId = topicId
            };

            var blogs = await sender.Send(getAllByTopicIdQuery);

            if (blogs == null || !blogs.Any())
            {
                return NotFound("Blogs not found");
            }

            // Проверьте данные перед отправкой на клиент
            foreach (var blog in blogs)
            {
                Console.WriteLine($"API Blog: {blog.Title}, User: {blog.User?.Name}");
            }

            return Ok(blogs);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching blogs: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }



    [HttpGet("Blog/Index")]
    public async Task<IActionResult> Index(int userId)
    {
        try
        {
            var getAllTopicsByUserIdQuery = new GetAllTopicsByUserIdQuery()
            {
                UserId = userId,
            };

            var preferableTopics = await sender.Send(getAllTopicsByUserIdQuery);

            return View(preferableTopics);
        }
        catch (Exception ex)
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

            // Проверка, если запрос поступил как AJAX (ожидаем JSON ответ)
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(blog);
            }

            ViewBag.Blog = blog;
            ViewBag.IsBlogDetail = true;
            return View("Index");
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




    [HttpGet("[controller]/[action]/{id}")]
    public async Task<IActionResult> Image(int id)
    {
        try
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
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return null;
        }
    }



    [HttpPost("api/Blog/CreateBlog")]
    public async Task<IActionResult> CreateBlog([FromForm] Blog newBlog, IFormFile image)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var randomId = Random.Shared.Next(1, 100000);
            var getBlogQuery = new GetByIdQuery()
            {
                Id = randomId,
            };

            var blog = await sender.Send(getBlogQuery);
            if (blog != null)
            {
                randomId = Random.Shared.Next(1, 100000);
            }
            newBlog.Id = randomId;
            newBlog.UserId = userId;

            var extension = new FileInfo(image.FileName).Extension[1..];
            newBlog.PictureUrl = $"Assets/BlogImg/{newBlog.Id}.{extension}";

            using var newFileStream = System.IO.File.Create(newBlog.PictureUrl);
            await image.CopyToAsync(newFileStream);

            newBlog.CreationDate = DateTime.UtcNow;

            var createCommand = new CreateCommand()
            {
                Title = newBlog.Title,
                Text = newBlog.Text,
                UserId = newBlog.UserId,
                TopicId = newBlog.TopicId,
                PictureUrl = newBlog.PictureUrl,
                CreationDate = newBlog.CreationDate,

            };

            await sender.Send(createCommand);

            return RedirectToAction("Index", "Blog", new { userId });
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

