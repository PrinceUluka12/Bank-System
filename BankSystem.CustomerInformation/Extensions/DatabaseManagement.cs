using BankSystem.CustomerInformation.Data;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.CustomerInformation.Extensions
{
    public static class DataBaseManagement
    {
        public static void MigrationIntilization(IApplicationBuilder app)
        {
            using(var serviceScope =  app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}