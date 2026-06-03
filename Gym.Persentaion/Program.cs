using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Data.Seeder;
using Gym.DataAccess.Dependencies;
using Gym.DataAccess.Interceptor;
using Gym.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gym.Persentaion
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDataAccess(connectionString);
            
            

            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            //builder.Services.AddDbContext<GymDBContext>( options =>
            //{
            //    options.UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection"));
            //});

            //builder.Services.AddScoped<AuditColumns>();
            //builder.Services.AddDbContext<GymDBContext>((sp, options) =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            //    options.AddInterceptors(
            //        sp.GetRequiredService<AuditColumns>());
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            if(app.Environment.IsDevelopment())
            {
                await using var scope = app.Services.CreateAsyncScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<GymDBContext>();
                await dbContext.Database.MigrateAsync();
                await DataBAseSeeder.SeedAllAsync(dbContext);
            }

            app.Run();
        }
    }
}
