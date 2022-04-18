using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public class DbInitialization
    {
        public static void InitializeDatabase(IServiceScope scope)
        {
            using (scope)
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<StudentsDbContext>();
                InitDb(context);
            }
        }

        private static void InitDb(DbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}