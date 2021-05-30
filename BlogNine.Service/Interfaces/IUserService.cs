using BlogNine.Data.Models;
using System.Threading.Tasks;

namespace BlogNine.Service.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> Update(ApplicationUser applicationUser);
    }
}
