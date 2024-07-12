namespace BlogApp.Core.ViewModel.Models;

using BlogApp.Core.Blog.Models;
using BlogApp.Core.Topic.Models;

public class BlogViewModel
{
    public IEnumerable<Blog> Blogs { get; set; }
    public IEnumerable<Topic> Topics { get; set; }
}
