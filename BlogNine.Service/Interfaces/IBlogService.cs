using BlogNine.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogNine.Service.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> Add(Blog blog);
    }
}