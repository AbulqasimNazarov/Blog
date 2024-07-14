using BlogApp.Core.Blog.Models;
using BlogApp.Infrastructure.Blog.Queries;
using MediatR;

// using BlogApp.Core.Blog.Services;
// using BlogApp.Core.Blog.Services.Base;
// using BlogApp.Core.Topic.Services.Base;
// using BlogApp.Core.ViewModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers
{
    public class BlogController : Controller
    {
        private readonly ISender sender;

        public BlogController(ISender sender)
        {
            this.sender = sender;
        }

        // [HttpGet]
        // [Route("[controller]/[action]/{id}")]
        // public async Task<IActionResult> GetBlogsByTopic(int id)
        // {
        //     var blogs = await blogService.GetAllBlogsByTopicId(new List<int> { id });
        //     return View(blogs);
        // }

        // [HttpGet]
        // public IActionResult SearchBlogByTopic()
        // {
        //     return View();
        // }

        [HttpPost]
        [Route("/Blog/GetBlogsByTopic")]
        public async Task<IActionResult> GetBlogsByTopic(GetAllByTopicIdQuery getAllByTopicIdQuery, int id)
        {
            var blogs = await this.sender.Send(getAllByTopicIdQuery);
            
            return View("Blog", blogs);
        }


        [HttpGet("[controller]/[action]/{id}")]
        public async Task<IActionResult> Image(GetByIdQuery getByIdQuery)
        {
            var blog = await this.sender.Send(getByIdQuery);
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
}
