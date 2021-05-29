using BlogNine.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogNine.Service.Interfaces
{
    public interface IPostService
    {
        Post GetPost(int postId);
        IEnumerable<Post> GetPosts(ApplicationUser applicationUser);
        IEnumerable<Post> GetPosts(string searchString);
        Task<Post> Add(Post post);

        Task<Post> Update(Post post);
    }
}