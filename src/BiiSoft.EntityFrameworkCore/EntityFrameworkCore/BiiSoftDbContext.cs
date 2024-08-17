using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using BiiSoft.Authorization.Roles;
using BiiSoft.Authorization.Users;
using BiiSoft.MultiTenancy;
using Abp.Localization;
using System;
using BiiSoft.Branches;
using BiiSoft.BFiles;
using BiiSoft.Locations;
using BiiSoft.Currencies;
using BiiSoft.ContactInfo;
using BiiSoft.Enums;

namespace BiiSoft.EntityFrameworkCore
{
    public class BiiSoftDbContext : AbpZeroDbContext<Tenant, Role, User, BiiSoftDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<BFile> BFiles { get; set; }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CityProvince> CityProvinces { get; set; }
        public DbSet<KhanDistrict> khanDistricts { get; set; }
        public DbSet<SangkatCommune> SangkatCommunes { get; set; }
        public DbSet<Village> Villages { get; set; }

        public DbSet<ContactAddress> ContactAddresses { get; set; }

        public DbSet<CompanyGeneralSetting> CompanyGeneralSettings { get; set; }
        public DbSet<CompanyAdvanceSetting> CompanyAdvanceSettings { get; set; }
        public DbSet<TransactionNoSetting> TransactionNoSettings { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<UserBranch> UserBranches { get; set; }
      
        public DbSet<Currency> Currencies { get; set; }

        //public DbSet<CompanySetting> CompanySettings { get; set; }
        //public DbSet<Tax> Taxes { get; set; }
        //public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }


        //public DbSet<Unit> Units { get; set; }

        public BiiSoftDbContext(DbContextOptions<BiiSoftDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationLanguageText>()
                        .Property(p => p.Value)
                        .HasMaxLength(100); // any integer that is smaller than 10485760


            modelBuilder.Entity<BFile>(e =>
            {
                e.HasIndex(i => new { i.Name, i.TenantId });
                e.HasIndex(i => new { i.DisplayName, i.TenantId });
                e.HasIndex(i => new { i.FilePath, i.TenantId }).IsUnique(true);
            });

            modelBuilder.Entity<Location>(e =>
            {
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
                e.HasIndex(i => i.No);
            });

            modelBuilder.Entity<Country>(e =>
            {
                e.HasIndex(i => i.Code).IsUnique(true);
                e.HasIndex(i => i.Name).IsUnique(true);
                e.HasIndex(i => i.DisplayName);
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.PhonePrefix);
                e.HasIndex(i => i.ISO).IsUnique(true);
                e.HasIndex(i => i.ISO2).IsUnique(true);
                e.HasOne(i => i.Currency).WithMany().HasForeignKey(i => i.CurrencyId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CityProvince>(e =>
            {
                e.HasIndex(i => i.Code).IsUnique(true);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.ISO).IsUnique(true);
                e.HasOne(e => e.Country).WithMany().HasForeignKey(e => e.CountryId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<KhanDistrict>(e =>
            {
                e.HasIndex(i => new { i.TenantId, i.Code }).IsUnique(true);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
                e.HasIndex(i => i.No);
                e.HasOne(e => e.Country).WithMany().HasForeignKey(e => e.CountryId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(e => e.CityProvince).WithMany().HasForeignKey(e => e.CityProvinceId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SangkatCommune>(e =>
            {
                e.HasIndex(i => new { i.TenantId, i.Code }).IsUnique(true);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
                e.HasIndex(i => i.No);
                e.HasOne(e => e.Country).WithMany().HasForeignKey(e => e.CountryId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(e => e.CityProvince).WithMany().HasForeignKey(e => e.CityProvinceId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(e => e.KhanDistrict).WithMany().HasForeignKey(e => e.KhanDistrictId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Village>(e =>
            {
                e.HasIndex(i => new { i.TenantId, i.Code }).IsUnique(true);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
                e.HasIndex(i => i.No);
                e.HasOne(e => e.Country).WithMany().HasForeignKey(e => e.CountryId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(e => e.CityProvince).WithMany().HasForeignKey(e => e.CityProvinceId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(e => e.KhanDistrict).WithMany().HasForeignKey(e => e.KhanDistrictId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(e => e.SangkatCommune).WithMany().HasForeignKey(e => e.SangkatCommuneId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ContactAddress>(e =>
            {
                e.HasOne(i => i.Country).WithMany().HasForeignKey(i => i.CountryId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.CityProvince).WithMany().HasForeignKey(i => i.CityProvinceId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.KhanDistrict).WithMany().HasForeignKey(i => i.KhanDistrictId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.SangkatCommune).WithMany().HasForeignKey(i => i.SangkatCommuneId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.Village).WithMany().HasForeignKey(i => i.VillageId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.Location).WithMany().HasForeignKey(i => i.LocationId).IsRequired(false).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CompanyGeneralSetting>(e =>
            {
                e.HasOne(s => s.Currency).WithMany().HasForeignKey(s => s.CurrencyId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(s => s.Country).WithMany().HasForeignKey(s => s.CountryId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.Property(s => s.RoundTotalDigits).HasDefaultValue(2);
                e.Property(s => s.RoundCostDigits).HasDefaultValue(2);
            });

            modelBuilder.Entity<CompanyAdvanceSetting>(e =>
            {

            });
             
            modelBuilder.Entity<TransactionNoSetting>(e =>
            {
                e.HasIndex(s => new { s.TenantId, s.JournalType }).IsUnique(true);
            });

            modelBuilder.Entity<Branch>(e =>
            {
                e.HasIndex(i => new { i.TenantId, i.Name }).IsUnique(true);
                e.HasIndex(i => new { i.TenantId, i.DisplayName });
                e.HasIndex(i => i.No);
                e.HasOne(i => i.BillingAddress).WithMany().HasForeignKey(i => i.BillingAddressId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.ShippingAddress).WithMany().HasForeignKey(i => i.ShippingAddressId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserBranch>(e =>
            {
                e.HasOne(i => i.Member).WithMany().HasForeignKey(i => i.MemberId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.Branch).WithMany().HasForeignKey(i => i.BranchId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Currency>(e =>
            {
                e.HasIndex(i => new { i.Code }).IsUnique(true);
                e.HasIndex(i => new { i.Name });
                e.HasIndex(i => new { i.DisplayName });
            });

            //modelBuilder.Entity<CompanySetting>(e =>
            //{
            //    e.HasOne(s => s.Logo).WithMany().HasForeignKey(s => s.LogoId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            //    e.HasOne(s => s.Currency).WithMany().HasForeignKey(s => s.CurrencyId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            //    e.HasOne(s => s.Class).WithMany().HasForeignKey(s => s.ClassId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            //    e.HasOne(s => s.DefaultAPAccount).WithMany().HasForeignKey(s => s.DefaultAPAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            //    e.HasOne(s => s.DefaultARAccount).WithMany().HasForeignKey(s => s.DefaultARAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            //    e.HasOne(s => s.DefaultBillPaymentAccount).WithMany().HasForeignKey(s => s.DefaultBillPaymentAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            //});

        }
    }
}
