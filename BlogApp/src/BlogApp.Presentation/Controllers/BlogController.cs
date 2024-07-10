using BlogApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;
        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }



        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> GetBlogsByTopic(int id){
            var blogs = await blogService.GetAllBlogsByTopicId(id);
            return View(blogs);
        }


        [HttpGet]
        public async Task<IActionResult> SearchBlogByTopic(){
            
            return View();
        }
    }
}