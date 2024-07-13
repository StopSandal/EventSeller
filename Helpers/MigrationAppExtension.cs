using Microsoft.EntityFrameworkCore;

namespace EventSeller.Helpers
{
    public static class MigrationAppExtension
    {
        public static IApplicationBuilder ApplyMigrations<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TContext>();
                if(context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
            return app;
        }
    }
}
