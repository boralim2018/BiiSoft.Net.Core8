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
using BiiSoft.ChartOfAccounts;
using BiiSoft.Taxes;
using BiiSoft.Items;

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

       
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public DbSet<CompanyAccountSetting> CompanyAccountSettings { get; set; }        
        public DbSet<Tax> Taxes { get; set; }

        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<ItemBrand> ItemBrands { get; set; }
        public DbSet<ItemGrade> ItemGrades { get; set; }
        public DbSet<ItemModel> ItemModels { get; set; }
        public DbSet<ItemSeries> ItemSeries { get; set; }
        public DbSet<ItemSize> ItemSizes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ColorPattern> ColorPatterns { get; set; }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<RAM> RAMs { get; set; }
        public DbSet<VGA> VGAs { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<HDD> HDDs { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<FieldA> FieldAs { get; set; }
        public DbSet<FieldB> FieldBs { get; set; }
        public DbSet<FieldC> FieldCs { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemGallery> ItemGalleries { get; set; }
        public DbSet<ItemFieldSetting> ItemFieldSettings { get; set; }


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
                e.HasIndex(i => i.Code).IsUnique(true);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });


            modelBuilder.Entity<ChartOfAccount>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => new { i.Code, i.TenantId }).IsUnique(true);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
                e.HasIndex(i => i.AccountType);
                e.HasIndex(i => i.SubAccountType);
                e.HasOne(i => i.Parent).WithMany().HasForeignKey(i => i.ParentId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CompanyAccountSetting>(e =>
            {
                e.HasOne(i => i.DefaultAPAccount).WithMany().HasForeignKey(i => i.DefaultAPAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultARAccount).WithMany().HasForeignKey(i => i.DefaultARAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultPurchaseDiscountAccount).WithMany().HasForeignKey(i => i.DefaultPurchaseDiscountAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultSaleDiscountAccount).WithMany().HasForeignKey(i => i.DefaultSaleDiscountAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultInventoryPurchaseAccount).WithMany().HasForeignKey(i => i.DefaultInventoryPurchaseAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultBillPaymentAccount).WithMany().HasForeignKey(i => i.DefaultBillPaymentAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultReceivePaymentAccount).WithMany().HasForeignKey(i => i.DefaultReceivePaymentAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultRetainEarningAccount).WithMany().HasForeignKey(i => i.DefaultRetainEarningAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultExchangeLossGainAccount).WithMany().HasForeignKey(i => i.DefaultExchangeLossGainAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultItemReceiptAccount).WithMany().HasForeignKey(i => i.DefaultItemReceiptAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultItemIssueAccount).WithMany().HasForeignKey(i => i.DefaultItemIssueAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultItemAdjustmentAccount).WithMany().HasForeignKey(i => i.DefaultItemAdjustmentAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultItemTransferAccount).WithMany().HasForeignKey(i => i.DefaultItemTransferAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultItemProductionAccount).WithMany().HasForeignKey(i => i.DefaultItemProductionAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultItemExchangeAccount).WithMany().HasForeignKey(i => i.DefaultItemExchangeAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultCashTransferAccount).WithMany().HasForeignKey(i => i.DefaultCashTransferAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.DefaultCashExchangeAccount).WithMany().HasForeignKey(i => i.DefaultCashExchangeAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Tax>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
                e.HasOne(i => i.PurchaseAccount).WithMany().HasForeignKey(i => i.PurchaseAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.SaleAccount).WithMany().HasForeignKey(i => i.SaleAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ItemGroup>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<ItemGrade>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<ItemBrand>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<ItemModel>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<ItemSeries>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<ItemSize>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<Unit>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<ColorPattern>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<CPU>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<RAM>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<VGA>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<Screen>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<HDD>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<Camera>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<Battery>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<FieldA>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<FieldB>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<FieldC>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
            });

            modelBuilder.Entity<Item>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.Name);
                e.HasIndex(i => i.DisplayName);
                e.HasIndex(i => i.ItemType);
                e.HasIndex(i => i.ItemCategory);
                e.HasOne(i => i.ItemGroup).WithMany().HasForeignKey(i => i.ItemGroupId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.ItemBrand).WithMany().HasForeignKey(i => i.ItemBrandId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.ItemGrade).WithMany().HasForeignKey(i => i.ItemGradeId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.ItemSize).WithMany().HasForeignKey(i => i.ItemSizeId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.ItemModel).WithMany().HasForeignKey(i => i.ItemModelId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.ItemSeries).WithMany().HasForeignKey(i => i.ItemSeriesId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.Unit).WithMany().HasForeignKey(i => i.UnitId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.ColorPattern).WithMany().HasForeignKey(i => i.ColorPatternId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.CPU).WithMany().HasForeignKey(i => i.CPUId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.RAM).WithMany().HasForeignKey(i => i.RAMId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.VGA).WithMany().HasForeignKey(i => i.VGAId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.Screen).WithMany().HasForeignKey(i => i.ScreenId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.HDD).WithMany().HasForeignKey(i => i.HDDId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.Camera).WithMany().HasForeignKey(i => i.CameraId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.Battery).WithMany().HasForeignKey(i => i.BatteryId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.FieldA).WithMany().HasForeignKey(i => i.FieldAId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.FieldB).WithMany().HasForeignKey(i => i.FieldBId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.FieldC).WithMany().HasForeignKey(i => i.FieldCId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.PurchaseAccount).WithMany().HasForeignKey(i => i.PurchaseAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.SaleAccount).WithMany().HasForeignKey(i => i.SaleAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.InventoryAccount).WithMany().HasForeignKey(i => i.InventoryAccountId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.SaleTax).WithMany().HasForeignKey(i => i.SaleTaxId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.PurchaseTax).WithMany().HasForeignKey(i => i.PurchaseTaxId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ItemGallery>(e =>
            {
                e.HasIndex(i => i.No);
                e.HasIndex(i => i.GalleryId);
                e.HasOne(i => i.Item).WithMany().HasForeignKey(i => i.ItemId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ItemFieldSetting>(e =>
            {  
            });

        }
    }
}
