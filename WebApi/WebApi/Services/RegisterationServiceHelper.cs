using BL.Contract;
using BL.DependencyInjection;
using BL.Mapping;
using BL.Services;
using BL.Services.Implementation;
using BL.Services.Implementation.Generic;
using BL.Services.Implementation.MaxMind_Ip;
using BL.Services.Implementation.Payments;
using BL.Services.Implementation.ShipmentService;
using BL.Services.Implementation.ShipmentService.ManageState;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using BL.Services.Interfaces.IMaxMind_Ip;
using BL.Services.Interfaces.IPayments;
using BL.Services.Interfaces.IShipment;
using BL.Services.Interfaces.IShipment.IManageStatue;
using DAL.Data.DbContext;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Domains;
using FluentValidation;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shared_Liberary.Common;
using System.Reflection;
using System.Text;
using WebApi.Services;


namespace WebAPI.Services
{
    public static class RegisterationServiceHelper
    {
        public static void RegisterationService(WebApplicationBuilder builder)
        {


            builder.Services.AddSingleton<DatabaseReader>(sp =>
            {
                var geoDbPath = Path.Combine(
                    builder.Environment.ContentRootPath,
                    "GeoIP",
                    "GeoLite2-Country.mmdb");

                return new DatabaseReader(geoDbPath);
            });


            // Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.ExpireTimeSpan = TimeSpan.FromDays(14);
               options.SlidingExpiration = true;
               options.Cookie.IsEssential = true;
               options.LoginPath = "/login";
               options.AccessDeniedPath = "/access-denied";
           });

            // Sql Server
            builder.Services.AddDbContext<ShipingContext>(options =>
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
            }).AddEntityFrameworkStores<ShipingContext>()
    .AddDefaultTokenProviders();


            // JWT Authentication
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
            builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("Paypal"));



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
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            builder.Services.AddScoped<ICarrierService, CarrierService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<ISettingService, SettingService>();
            builder.Services.AddScoped<IShipingTypeService, ShipingTypeService>();
            builder.Services.AddScoped<IShipingPackgingTypes, ShipingPackgingService>();
            builder.Services.AddScoped<ISubscriptionPackageService, SubscriptionPackageService>();
            builder.Services.AddScoped<IUserReciverService, UserReciverService>();
            builder.Services.AddScoped<IUserSenderService, UserSenderService>();
            builder.Services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICalculateRateService, CalculateRateService>();
            builder.Services.AddScoped<ITrackingNumberCreatorService, TrackingNumberCreatorService>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddScoped<IRefreshTokenRetriver, RefreshTokenRetriverService>();
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<IShipmentStatusService, ShipmentStatusService>();
            builder.Services.AddScoped<IShipmentCommand, ShipmentCommandService>();
            builder.Services.AddScoped<IShipmentQuery, ShipmentQueryService>();
            builder.Services.AddScoped<IShipmentStateHandlerFactory, ShipmentStateHandlerFactory>();
            builder.Services.AddScoped<ApproveShipment>();
            builder.Services.AddScoped<ReadyShipment>();
            builder.Services.AddScoped<ShippedShipment>();
            builder.Services.AddScoped<DeliverdShipment>();
            builder.Services.AddScoped<ReturnedShipment>();
            builder.Services.AddHttpClient<PayPalGateway>();
            builder.Services.AddHttpClient<PaymobGateway>();
            builder.Services.AddScoped<PaymentGatewayFactory>();
            builder.Services.AddScoped<StripeGateway>();
            builder.Services.AddHttpContextAccessor();


            builder.Services.AddScoped<IUserCountryProvider, MaxMindCountryProvider>();

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddBLServices();



        }
    }
}
