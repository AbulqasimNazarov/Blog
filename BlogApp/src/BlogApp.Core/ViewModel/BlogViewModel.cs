using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;

namespace BlogApp.Core.ViewModel
{
    public class BlogViewModel
    {
       public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
    }
}