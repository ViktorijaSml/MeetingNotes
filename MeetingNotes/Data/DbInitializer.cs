using MeetingNotes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotes.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {

            context.Database.Migrate();

            if (context.Workers.Any())
            {
                return;   // DB has been seeded
            }
           
        }
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

                DbInitializer.Initialize(db);
            }
            return app;
        }

    }
}
