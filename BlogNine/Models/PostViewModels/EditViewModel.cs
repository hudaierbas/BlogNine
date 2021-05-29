using BlogNine.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogNine.Models.PostViewModels
{
    public class EditViewModel
    {
        [Display(Name = "Header Image")]
        public IFormFile HeaderImage { get; set; }
        public Post Post { get; set; }
    }
}
