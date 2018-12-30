using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAdvice.Data;
using UserAdvice.Data.Entities;

namespace UserAdvice.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUserAdviceApplication(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            
            return services;
        }
    }
}
