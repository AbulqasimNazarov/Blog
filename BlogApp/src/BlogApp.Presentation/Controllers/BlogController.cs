using BlogApp.Core.Blog.Models;
using BlogApp.Core.Blog.Services;
using BlogApp.Core.Blog.Services.Base;
using BlogApp.Core.Topic.Services.Base;
using BlogApp.Core.ViewModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;
        private readonly ITopicService topicService;

        public BlogController(IBlogService blogService, ITopicService topicService)
        {
            this.blogService = blogService;
            this.topicService = topicService;
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> GetBlogsByTopic(int id)
        {
            var blogs = await blogService.GetAllBlogsByTopicId(new List<int> { id });
            return View(blogs);
        }

        [HttpGet]
        public IActionResult SearchBlogByTopic()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FilterBlogsByTopics(List<int> selectedTopicIds)
        {
            var blogs = await blogService.GetAllBlogsByTopicId(selectedTopicIds);
            var viewModel = new BlogViewModel
            {
                Blogs = blogs,
                Topics = await topicService.GetAllTopicsAsync()
            };
            return View("Blog", viewModel);
        }


        [HttpGet("[controller]/[action]/{id}")]
        public async Task<IActionResult> Image(int id)
        {
            var blog = await blogService.GetBlogByIdAsync(id);
            if (blog == null || string.IsNullOrEmpty(blog.PictureUrl))
            {
                return NotFound("Film or image not found.");
            }
            var fileStream = System.IO.File.Open(blog.PictureUrl!, FileMode.Open);
            return File(fileStream, "image/jpeg");
        }



        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Add([FromForm] Blog newBlog, IFormFile image)
        {
            if (newBlog == null || image == null)
            {
                return BadRequest("Invalid blog data or image.");
            }

            await blogService.CreateBlogAsync(newBlog, image);
            return base.RedirectToAction(controllerName: "Blog", actionName: "FilterBlogsByTopics");
        }
    }
}
