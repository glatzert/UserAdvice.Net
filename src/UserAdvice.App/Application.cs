using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using UserAdvice.Data;

namespace UserAdvice
{
    public static class ApplicationSetup
    {
        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            using (serviceProvider.CreateScope())
            {
                var dbContext = serviceProvider
                    .GetRequiredService<ApplicationDbContext>();

                dbContext.Database.Migrate();
            }
        }
    }
}
