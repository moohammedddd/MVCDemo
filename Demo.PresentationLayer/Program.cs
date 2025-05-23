using DataAccessLayer;
using DataAccessLayer.Contexts;
using DataAccessLayer.Migrations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Repositiories.Classes;
using DataAccessLayer.Repositiories.Interfaces;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.BusinessLogic.Services;
using Demo.BusinessLogic.Profiels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Services to the container
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            builder.Services.AddDataAccessLayer(builder.Configuration);
            //builder.Services.AddScoped<DbContextOptions<AppDbContext>>();

            //builder.Services.AddScoped<AppDbContext>((ServicesProvider) =>
            //{
            //   // var options = ServicesProvider.GetRequiredService<DbContextOptions<AppDbContext>>();

            //    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //    optionsBuilder.UseSqlServer("ConnectionString");
            //    return new AppDbContext(optionsBuilder.Options);
            //});
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //options.UseLazyLoadingProxies
           

            //builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
            //{
            //    optionsBuilder.UseSqlServer(
            //     builder.Configuration.GetConnectionString("ConnectionStrings:DefaultConnection")
            // );

            //});
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices,DepartmentServices>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            builder.Services.AddAutoMapper(p => p.AddProfile(new MappingProfiles()));
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
