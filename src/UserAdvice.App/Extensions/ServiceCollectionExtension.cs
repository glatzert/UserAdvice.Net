using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAdvice.Data;
using UserAdvice.Data.Entities;
using UserAdvice.Queries;
using UserAdvice.Utilities;

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

            services.AddTransient<IClock, Clock>();

            services.Scan(x =>
                x.FromAssembliesOf(typeof(ServiceCollectionExtension))
                 .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                 .AsImplementedInterfaces()
                 .WithScopedLifetime()
            );

            return services;
        }
    }
}
