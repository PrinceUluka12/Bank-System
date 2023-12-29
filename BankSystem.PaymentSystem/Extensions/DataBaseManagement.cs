using BankSystem.PaymentSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.PaymentSystem.Extensions
{
    public class DataBaseManagement
    {
        public static void MigrationIntilization(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
