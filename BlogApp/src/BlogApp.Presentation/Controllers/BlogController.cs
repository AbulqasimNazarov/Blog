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

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> GetBlogsByTopic(int id)
        {
            try
            {
                var query = new GetAllByTopicIdQuery { TopicId = id };
                var blogs = await sender.Send(query);

                return Json(blogs);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetBlogsByTopic(List<int> selectedTopicIds)
        {
            if (selectedTopicIds == null || selectedTopicIds.Count < 3)
            {
                ModelState.AddModelError("", "Please choose at least three topics.");
                return BadRequest(ModelState);
            }

            try
            {
                //var filteredBlogs = await sender.Send(new GetBlogsByTopicIdsQuery { TopicIds = selectedTopicIds });

                return View("Blog");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
