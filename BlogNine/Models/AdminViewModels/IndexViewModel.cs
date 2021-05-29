using BlogNine.Data.Models;
using System.Collections.Generic;

namespace BlogNine.Models.AdminViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
