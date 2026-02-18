using AppResources;
using BL.Contract;
using BL.DependencyInjection;
using BL.Mapping;
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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Shared_Liberary.Common;
using System.Net.Http.Headers;
using System.Reflection;



namespace Ui.Services
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

                Console.WriteLine("GeoIP DB path => " + geoDbPath); 

                return new DatabaseReader(geoDbPath);
            });



            builder.Services.AddHttpClient("ApiClient", client =>
            {
                // Base URL will be configured in GenericApiClient constructor using appsettings.json
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            // Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.LoginPath = "/Account/Login";
               options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
               options.Cookie.IsEssential = true;
               options.ExpireTimeSpan = TimeSpan.FromDays(7);
           });

            // Sql Server with transient error retry
            builder.Services.AddDbContext<ShipingContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,                       // أقصى عدد محاولات
                        maxRetryDelay: TimeSpan.FromSeconds(10), // أقصى تأخير بين المحاولات
                        errorNumbersToAdd: null                  // لو عايز تضيف أكواد خطأ معينة، سيبه null عادة
                    )
                )
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
            builder.Services.AddAuthorization();

            // تسجيل Serilog
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration)
                             .Enrich.FromLogContext()
                             .WriteTo.Console();
            });




            builder.Services.AddScoped<IMappingService, MappingService>();
            builder.Services.AddScoped<GenericApiClient>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericVwRepository<>), typeof(GenericVwRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            builder.Services.AddScoped<ICarrierService, CarrierService>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddScoped<IRefreshTokenRetriver, RefreshTokenRetriverService>();

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
            builder.Services.AddScoped<IShipmentStatusService, ShipmentStatusService>();
            builder.Services.AddScoped<IShipmentCommand, ShipmentCommandService>();
            builder.Services.AddScoped<IShipmentQuery, ShipmentQueryService>();
            builder.Services.AddScoped<IShipmentStateHandlerFactory, ShipmentStateHandlerFactory>();
            builder.Services.AddScoped<ApproveShipment>();
            builder.Services.AddScoped<ReadyShipment>();
            builder.Services.AddScoped<ShippedShipment>();
            builder.Services.AddScoped<DeliverdShipment>();
            builder.Services.AddScoped<ReturnedShipment>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();


            builder.Services.AddScoped<PayPalGateway>();
            builder.Services.AddScoped<PaymobGateway>();
            builder.Services.AddScoped<IPaymentGatewayFactory, PaymentGatewayFactory>();
            builder.Services.AddScoped<StripeGateway>();

            builder.Services.AddScoped<IUserCountryProvider, MaxMindCountryProvider>();


            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddBLServices();



        }
    }
}
