using Shipping.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Shipping.Filters;
using Shipping.Repository.ArabicNamesColumnIntoRoleClaimsTable;
using Shipping.Repository.DeliveryRepo;
using Shipping.Repository.EmployeeRepo;
using Shipping.Repository.MerchantRepo;
using Shipping.Repository.StateRepo;
using Shipping.Repository.CityRepo;
using Shipping.Repository.BranchRepo;
using Shipping.Repository.OrderRepo;
using Shipping.Repository.WeightRepo;

namespace Shipping
{
    public class Program
    {   
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<MyContext>(o =>
            o.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();


            builder.Services.AddScoped<IAddArabicNamesToRoleCaimsTable, AddArabicNamesToRoleCaimsTable>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
            builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            builder.Services.AddScoped<IStateRepository, StateRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<IbranchRepository, BranchRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepositorty>();
            builder.Services.AddScoped<IWeightSettingRepository, WeightSettingRepository>();
            


            //For igonring reload page when change permissions
            builder.Services.Configure<SecurityStampValidatorOptions>(
                options =>
                {
                    options.ValidationInterval = TimeSpan.Zero;
                }) ;

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = null;
                options.User.RequireUniqueEmail = true;
            });


            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
                option =>
                {
                    option.Password.RequireUppercase = false;
                    option.Password.RequireNonAlphanumeric = false;
                }
                ).AddEntityFrameworkStores<MyContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            /*app.UseAuthentication();*/   
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}