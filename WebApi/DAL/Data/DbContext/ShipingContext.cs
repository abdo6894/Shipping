using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace DAL.Data.DbContext;

public partial class ShipingContext : IdentityDbContext<ApplicationUser>
{
    public ShipingContext()
    {
    }

    public ShipingContext(DbContextOptions<ShipingContext> options)
        : base(options)
    {
    }
    public virtual DbSet<RefreshToken> TbRefreshTokens { get; set; }
    public virtual DbSet<Carrier> TbCarriers { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    

    public virtual DbSet<City> TbCities { get; set; }
    public virtual DbSet<VwCitiy> VwCities { get; set; }

    public virtual DbSet<Country> TbCountries { get; set; }


    public virtual DbSet<Setting> TbSettings { get; set; }

    public virtual DbSet<ShipingType> TbShipingTypes { get; set; }
    public virtual DbSet<ShipingPackging> TbShipingPackginges{ get; set; }

    public virtual DbSet<Shipment> TbShipments { get; set; }

    public virtual DbSet<ShipmentStatus> TbShipmentStatuses { get; set; }

    public virtual DbSet<SubscriptionPackage> TbSubscriptionPackages { get; set; }

    public virtual DbSet<UserReciver> TbUserReceivers { get; set; }

    public virtual DbSet<UserSender> TbUserSenders { get; set; }

    public virtual DbSet<UserSubscription> TbUserSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        // Apply configurations automatically
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShipingContext).Assembly);
    }
}
