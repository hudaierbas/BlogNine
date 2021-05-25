using BlogNine.BusinessManagers;
using BlogNine.BusinessManagers.Interfaces;
using BlogNine.Data;
using BlogNine.Data.Models;
using BlogNine.Service;
using BlogNine.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogNine.Configuration
{
    public static class AppService
    {
        public static void AddDefaultServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            serviceCollection.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            serviceCollection.AddControllersWithViews().AddRazorRuntimeCompilation(); 
            serviceCollection.AddRazorPages();
        }

        public static void AddCustomServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBlogBusinessManager, BlogBusinessManager>();

            serviceCollection.AddScoped<IBlogService, BlogService>();
        }
    }
}
