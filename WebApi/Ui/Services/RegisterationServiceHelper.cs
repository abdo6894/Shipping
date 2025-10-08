using BL.DependencyInjection;
using BL.Mapping;
using BL.Services.Implementation;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Data.DbContext;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Domains;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;


namespace Ui.Services
{
    public static class RegisterationServiceHelper
    {
        public static void RegisterationService(WebApplicationBuilder builder)
        {
            // Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.ExpireTimeSpan = TimeSpan.FromDays(14); 
               options.SlidingExpiration = true; 
               options.LoginPath = "/login";
               options.AccessDeniedPath = "/access-denied";
           });

            // Sql Server
            builder.Services.AddDbContext<ShippingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ShippingContext>()
    .AddDefaultTokenProviders();
            builder.Services.AddAuthorization();

            // تسجيل Serilog
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration)
                             .Enrich.FromLogContext()
                             .WriteTo.Console();
            });

            builder.Services.AddScoped<IMappingService, MappingService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericVwRepository<>), typeof(GenericVwRepository<>));
            builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            builder.Services.AddScoped<ICarrierService, CarrierService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            builder.Services.AddScoped<ISettingService, SettingService>();
            builder.Services.AddScoped<IShippingTypeService, ShippingTypeService>();
            builder.Services.AddScoped<IShippmentService, ShippmentService>();
            builder.Services.AddScoped<IShippmentStatusService, ShippmentStatusService>();
            builder.Services.AddScoped<ISubscriptionPackageService, SubscriptionPackageService>();
            builder.Services.AddScoped<IUserReciverService, UserReciverService>();
            builder.Services.AddScoped<IUserSenderService, UserSenderService>();
            builder.Services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddBLServices();



        }
    }
}
