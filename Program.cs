using Microsoft.EntityFrameworkCore;
using Mvc_Project.Models.Repositorys;
using Mvc_Project.Models;
using Mvc_Project.Models.Repositorys.Mvc_Project.Models.Repositorys;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Mvc_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddDbContext<Context>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("connectionString"));
            });
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            

            builder.Services.AddScoped<INotification, NotificationRepository>();

            //////////Email////////////////
            var emailConfig = builder.Configuration.GetSection("EmailSettings");


            builder.Services.AddTransient<IEmailSender>(sp =>
                new EmailSenderRepository(
                    emailConfig["SmtpServer"],
                    int.Parse(emailConfig["SmtpPort"]),
                    emailConfig["SmtpUser"],
                    emailConfig["SmtpPass"]
                )
            );

            ////////////////////////////////
            ////////iIdentity///////


            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {

                options.Tokens.ProviderMap.Add("Default", new TokenProviderDescriptor(typeof(DataProtectorTokenProvider<User>)));
                options.Tokens.EmailConfirmationTokenProvider = "Default";
                options.Tokens.PasswordResetTokenProvider = "Default";
            })
                .AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

            /////////////////////////////////

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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

            app.Run();
        }
    }
}
