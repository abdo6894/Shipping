using BL.Mapping;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Data.DbContext;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


namespace SharedLiberary
{
    public static class RegisterationServiceHelper
    {
        public static void RegisterationService(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ShippingContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            Log.Logger = new LoggerConfiguration()
      .WriteTo.Console()
      .WriteTo.MSSqlServer(
          connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
          sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
          {
              TableName = "Log",
              AutoCreateSqlTable = true
          }
      )
      .CreateLogger();
            builder.Host.UseSerilog();

            builder.Services.AddScoped<IMappingService, MappingService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
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

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
        }
    }
}
