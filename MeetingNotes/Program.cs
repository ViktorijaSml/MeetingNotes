using MeetingNotes.Data;
using Microsoft.Extensions.DependencyInjection;
using MeetingNotes.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MeetingManager.Services;

namespace MeetingNotes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                // Password requirements configuration
                options.Password.RequireDigit = true;        // Requires at least one digit (0-9)
                options.Password.RequiredLength = 8;        // Requires minimum password length of 8 characters
                options.Password.RequireNonAlphanumeric = true; // Requires at least one non-alphanumeric character
                options.Password.RequireUppercase = true;    // Requires at least one uppercase letter
                options.Password.RequireLowercase = true;    // Requires at least one lowercase letter
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            

            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IWorkerService, WorkerService>();
            builder.Services.AddTransient<IMeetingService, MeetingService>();
            builder.Services.AddTransient<INoteService, NoteService>();
            builder.Services.AddTransient<IManagerService, ManagerService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            // postavi podatke u bazu
            app.SeedData();

            app.Run();
        }
    }
}