using DataAccessLayer;
using DataAccessLayer.Contexts;
using DataAccessLayer.Migrations;
using DataAccessLayer.Repositiories;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace Demo.PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Services to the container
            builder.Services.AddControllersWithViews();
            builder.Services.AddDataAccessLayer(builder.Configuration);
            builder.Services.AddScoped<DbContextOptions<AppDbContext>>();

            builder.Services.AddScoped<AppDbContext>((ServicesProvider) =>
            {
               // var options = ServicesProvider.GetRequiredService<DbContextOptions<AppDbContext>>();
                
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer("ConnectionString");
                return new AppDbContext(optionsBuilder.Options);
            });

            builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(
                 builder.Configuration.GetConnectionString("DefaultConnection")
             );

            });
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            //contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped

            #endregion
            /// Add services to the container.


            var app = builder.Build();

            #region  Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            #endregion


            app.Run();
        }
    }
}
