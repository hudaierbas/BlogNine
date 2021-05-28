using BlogNine.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogNine.Models.BlogViewModels
{
    public class EditViewModel
    {
        [Display(Name = "Header Image")]
        public IFormFile BlogHeaderImage { get; set; }
        public Blog Blog { get; set; }
    }
}
