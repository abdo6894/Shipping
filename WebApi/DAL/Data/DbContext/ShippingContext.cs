using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DAL.UserModels;
namespace DAL.Data.DbContext;

public partial class ShippingContext : IdentityDbContext<ApplicationUser>
{
    public ShippingContext()
    {
    }

    public ShippingContext(DbContextOptions<ShippingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCarrier> TbCarriers { get; set; }

    public virtual DbSet<TbCity> TbCities { get; set; }

    public virtual DbSet<TbCountry> TbCountries { get; set; }

    public virtual DbSet<TbPaymentMethod> TbPaymentMethods { get; set; }

    public virtual DbSet<TbSetting> TbSettings { get; set; }

    public virtual DbSet<TbShippingType> TbShippingTypes { get; set; }

    public virtual DbSet<TbShippment> TbShippments { get; set; }

    public virtual DbSet<TbShippmentStatus> TbShippmentStatuses { get; set; }

    public virtual DbSet<TbSubscriptionPackage> TbSubscriptionPackages { get; set; }

    public virtual DbSet<TbUserReciver> TbUserReceivers { get; set; }

    public virtual DbSet<TbUserSender> TbUserSebders { get; set; }

    public virtual DbSet<TbUserSubscription> TbUserSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        // Apply configurations automatically
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShippingContext).Assembly);
    }
}
