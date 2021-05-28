using BlogNine.Data.Models;
using System.Collections.Generic;

namespace BlogNine.Models.AdminViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
