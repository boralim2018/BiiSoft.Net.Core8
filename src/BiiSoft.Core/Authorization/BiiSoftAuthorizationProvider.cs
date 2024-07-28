using Abp;
using Abp.Application.Features;
using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using BiiSoft.Features;

namespace BiiSoft.Authorization
{
    public class BiiSoftAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {

            var page = context.CreatePermission(PermissionNames.Pages, L("Pages"));

            page.CreateChildPermission(PermissionNames.Pages_Profile, L("Profile"));
            page.CreateChildPermission(PermissionNames.Pages_Dashboard, L("Dashboard"));
            page.CreateChildPermission(PermissionNames.Pages_Hangfire_Dashboard, L("HangfireDashboard"), multiTenancySides: MultiTenancySides.Host);

            #region Find
            var findPage = page.CreateChildPermission(PermissionNames.Pages_Find, L("Find"));
            findPage.CreateChildPermission(PermissionNames.Pages_Find_Users, L("FindUsers"));
            findPage.CreateChildPermission(PermissionNames.Pages_Find_Editions, L("FindEditions"));
            findPage.CreateChildPermission(PermissionNames.Pages_Find_Branches, L("FindBranches"));
            findPage.CreateChildPermission(PermissionNames.Pages_Find_Locations, L("FindLocations"));
            findPage.CreateChildPermission(PermissionNames.Pages_Find_Countries, L("FindCountries"));
            findPage.CreateChildPermission(PermissionNames.Pages_Find_CityProvinces, L("FindCityProvinces"));
            findPage.CreateChildPermission(PermissionNames.Pages_Find_KhanDistricts, L("FindKhanDistricts"));
            findPage.CreateChildPermission(PermissionNames.Pages_Find_SangkatCommunes, L("FindSangkatCommunes"));
            findPage.CreateChildPermission(PermissionNames.Pages_Find_Villages, L("FindVillages"));
            findPage.CreateChildPermission(PermissionNames.Pages_Find_Currencies, L("FindCurrencies"));
            #endregion

            #region Host

            var tenantPage = page.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenantPage.CreateChildPermission(PermissionNames.Pages_Tenants_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            tenantPage.CreateChildPermission(PermissionNames.Pages_Tenants_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            tenantPage.CreateChildPermission(PermissionNames.Pages_Tenants_ChangeFeatures, L("ChangeFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenantPage.CreateChildPermission(PermissionNames.Pages_Tenants_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);
            tenantPage.CreateChildPermission(PermissionNames.Pages_Tenants_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Host);
            tenantPage.CreateChildPermission(PermissionNames.Pages_Tenants_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Host);
            tenantPage.CreateChildPermission(PermissionNames.Pages_Tenants_Impersonation, L("LoginAsTenant"), multiTenancySides: MultiTenancySides.Host);

            var editionPage = page.CreateChildPermission(PermissionNames.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editionPage.CreateChildPermission(PermissionNames.Pages_Editions_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            editionPage.CreateChildPermission(PermissionNames.Pages_Editions_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            editionPage.CreateChildPermission(PermissionNames.Pages_Editions_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);

            var languagePage = page.CreateChildPermission(PermissionNames.Pages_Languages, L("Languages"), multiTenancySides: MultiTenancySides.Host);
            languagePage.CreateChildPermission(PermissionNames.Pages_Languages_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            languagePage.CreateChildPermission(PermissionNames.Pages_Languages_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            languagePage.CreateChildPermission(PermissionNames.Pages_Languages_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);
            languagePage.CreateChildPermission(PermissionNames.Pages_Languages_ChangeTexts, L("ChangeTexts"), multiTenancySides: MultiTenancySides.Host);
            languagePage.CreateChildPermission(PermissionNames.Pages_Languages_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Host);
            languagePage.CreateChildPermission(PermissionNames.Pages_Languages_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Host);

            page.CreateChildPermission(PermissionNames.Pages_Settings_Host, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            page.CreateChildPermission(PermissionNames.Pages_Settings_Tenant, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);

            #endregion

            #region Setup

            var setupPage = page.CreateChildPermission(PermissionNames.Pages_Setup, L("Setup"));

            var currencyPage = setupPage.CreateChildPermission(PermissionNames.Pages_Setup_Currencies, L("Currencies"), multiTenancySides: MultiTenancySides.Host);
            currencyPage.CreateChildPermission(PermissionNames.Pages_Setup_Currencies_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            currencyPage.CreateChildPermission(PermissionNames.Pages_Setup_Currencies_View, L("View"), multiTenancySides: MultiTenancySides.Host);
            currencyPage.CreateChildPermission(PermissionNames.Pages_Setup_Currencies_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            currencyPage.CreateChildPermission(PermissionNames.Pages_Setup_Currencies_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);
            currencyPage.CreateChildPermission(PermissionNames.Pages_Setup_Currencies_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Host);
            currencyPage.CreateChildPermission(PermissionNames.Pages_Setup_Currencies_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Host);
            currencyPage.CreateChildPermission(PermissionNames.Pages_Setup_Currencies_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Host);
            currencyPage.CreateChildPermission(PermissionNames.Pages_Setup_Currencies_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Host);
            currencyPage.CreateChildPermission(PermissionNames.Pages_Setup_Currencies_SetAsDefault, L("SetAsDefault"), multiTenancySides: MultiTenancySides.Host);

            var exchangeRatePage = setupPage.CreateChildPermission(PermissionNames.Pages_Setup_ExchangeRates, L("ExchangeRates"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_MultiCurrencies));
            exchangeRatePage.CreateChildPermission(PermissionNames.Pages_Setup_ExchangeRates_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_MultiCurrencies));
            exchangeRatePage.CreateChildPermission(PermissionNames.Pages_Setup_ExchangeRates_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_MultiCurrencies));
            exchangeRatePage.CreateChildPermission(PermissionNames.Pages_Setup_ExchangeRates_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_MultiCurrencies));
            exchangeRatePage.CreateChildPermission(PermissionNames.Pages_Setup_ExchangeRates_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_MultiCurrencies));
            exchangeRatePage.CreateChildPermission(PermissionNames.Pages_Setup_ExchangeRates_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_MultiCurrencies));

            #region Locations

            var locationPage = setupPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations, L("Locations"));

            var locationListPage = locationPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_List, L("LocationList"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Loctions));
            locationListPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Loctions));
            locationListPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Loctions));
            locationListPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Loctions));
            locationListPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Loctions));
            locationListPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Loctions));
            locationListPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Loctions));
            locationListPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Loctions));
            locationListPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Loctions));

            var countryPage = locationPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Countries, L("Countries"), multiTenancySides: MultiTenancySides.Host);
            countryPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Countries_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            countryPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Countries_View, L("View"), multiTenancySides: MultiTenancySides.Host);
            countryPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Countries_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            countryPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Countries_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);
            countryPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Countries_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Host);
            countryPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Countries_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Host);
            countryPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Countries_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Host);
            countryPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Countries_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Host);

            var cityProvincePage = locationPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_CityProvinces, L("CityProvinces"), multiTenancySides: MultiTenancySides.Host);
            cityProvincePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_CityProvinces_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            cityProvincePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_CityProvinces_View, L("View"), multiTenancySides: MultiTenancySides.Host);
            cityProvincePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_CityProvinces_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            cityProvincePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_CityProvinces_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);
            cityProvincePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_CityProvinces_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Host);
            cityProvincePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_CityProvinces_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Host);
            cityProvincePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_CityProvinces_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Host);
            cityProvincePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_CityProvinces_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Host);

            var khanDistrictPage = locationPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_KhanDistricts, L("KhanDistricts"), multiTenancySides: MultiTenancySides.Host);
            khanDistrictPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_KhanDistricts_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            khanDistrictPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_KhanDistricts_View, L("View"), multiTenancySides: MultiTenancySides.Host);
            khanDistrictPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_KhanDistricts_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            khanDistrictPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_KhanDistricts_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);
            khanDistrictPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_KhanDistricts_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Host);
            khanDistrictPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_KhanDistricts_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Host);
            khanDistrictPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_KhanDistricts_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Host);
            khanDistrictPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_KhanDistricts_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Host);

            var sangkatCommunePage = locationPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_SangkatCommunes, L("SangkatCommunes"), multiTenancySides: MultiTenancySides.Host);
            sangkatCommunePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            sangkatCommunePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_SangkatCommunes_View, L("View"), multiTenancySides: MultiTenancySides.Host);
            sangkatCommunePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            sangkatCommunePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);
            sangkatCommunePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_SangkatCommunes_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Host);
            sangkatCommunePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_SangkatCommunes_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Host);
            sangkatCommunePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Host);
            sangkatCommunePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Host);

            var villagePage = locationPage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Villages, L("Villages"), multiTenancySides: MultiTenancySides.Host);
            villagePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Villages_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            villagePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Villages_View, L("View"), multiTenancySides: MultiTenancySides.Host);
            villagePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Villages_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            villagePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Villages_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);
            villagePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Villages_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Host);
            villagePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Villages_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Host);
            villagePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Villages_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Host);
            villagePage.CreateChildPermission(PermissionNames.Pages_Setup_Locations_Villages_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Host);

            #endregion

            #region Items

            var itemPage = setupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items, L("Items"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items));

            var itemGroupPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups, L("ItemGroups"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Groups));
            itemGroupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Groups));
            itemGroupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Groups));
            itemGroupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Groups));
            itemGroupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Setup_Items_Groups, AppFeatures.Accounting_ChartOfAccounts));
            itemGroupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Setup_Items_Groups, AppFeatures.Accounting_ChartOfAccounts));
            itemGroupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Groups));
            itemGroupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Groups));
            itemGroupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Groups));
            itemGroupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Groups));
            itemGroupPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ItemGroups_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Groups));

            var itemListPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List, L("ItemList"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items));
            itemListPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items));
            itemListPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items));
            itemListPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items));
            itemListPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Setup_Items, AppFeatures.Accounting_ChartOfAccounts));
            itemListPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Setup_Items, AppFeatures.Accounting_ChartOfAccounts));
            itemListPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items));
            itemListPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items));
            itemListPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items));
            itemListPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items));
            itemListPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_List_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items));

            var unitPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Units, L("Units"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Units));
            unitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Units_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Units));
            unitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Units_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Units));
            unitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Units_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Units));
            unitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Units_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Units));
            unitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Units_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Units));
            unitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Units_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Units));
            unitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Units_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Units));
            unitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Units_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Units));

            var weightUnitPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_WeightUnits, L("WeightUnits"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_WeightUnits));
            weightUnitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_WeightUnits_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_WeightUnits));
            weightUnitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_WeightUnits_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_WeightUnits));
            weightUnitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_WeightUnits_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_WeightUnits));
            weightUnitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_WeightUnits_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_WeightUnits));
            weightUnitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_WeightUnits_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_WeightUnits));
            weightUnitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_WeightUnits_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_WeightUnits));
            weightUnitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_WeightUnits_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_WeightUnits));
            weightUnitPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_WeightUnits_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_WeightUnits));

            var gradePage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Grades, L("Grades"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Grades));
            gradePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Grades_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Grades));
            gradePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Grades_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Grades));
            gradePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Grades_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Grades));
            gradePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Grades_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Grades));
            gradePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Grades_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Grades));
            gradePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Grades_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Grades));
            gradePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Grades_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Grades));
            gradePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Grades_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Grades));

            var sizePage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Sizes, L("Sizes"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Sizes));
            sizePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Sizes_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Sizes));
            sizePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Sizes_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Sizes));
            sizePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Sizes_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Sizes));
            sizePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Sizes_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Sizes));
            sizePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Sizes_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Sizes));
            sizePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Sizes_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Sizes));
            sizePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Sizes_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Sizes));
            sizePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Sizes_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Sizes));

            var colorPatternPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ColorPatterns, L("ColorPatterns"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_ColorPatterns));
            colorPatternPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ColorPatterns_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_ColorPatterns));
            colorPatternPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ColorPatterns_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_ColorPatterns));
            colorPatternPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ColorPatterns_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_ColorPatterns));
            colorPatternPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ColorPatterns_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_ColorPatterns));
            colorPatternPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ColorPatterns_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_ColorPatterns));
            colorPatternPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ColorPatterns_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_ColorPatterns));
            colorPatternPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ColorPatterns_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_ColorPatterns));
            colorPatternPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_ColorPatterns_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_ColorPatterns));

            var brandPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Brands, L("Brands"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Brands));
            brandPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Brands_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Brands));
            brandPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Brands_View, L("View"),multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Brands));
            brandPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Brands_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Brands));
            brandPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Brands_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Brands));
            brandPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Brands_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Brands));
            brandPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Brands_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Brands));
            brandPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Brands_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Brands));
            brandPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Brands_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Brands));

            var fieldAPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldAs, L("FieldAs"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldAs));
            fieldAPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldAs_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldAs));
            fieldAPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldAs_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldAs));
            fieldAPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldAs_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldAs));
            fieldAPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldAs_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldAs));
            fieldAPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldAs_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldAs));
            fieldAPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldAs_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldAs));
            fieldAPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldAs_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldAs));
            fieldAPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldAs_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldAs));

            var fieldBPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldBs, L("FieldBs"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldBs));
            fieldBPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldBs_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldBs));
            fieldBPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldBs_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldBs));
            fieldBPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldBs_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldBs));
            fieldBPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldBs_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldBs));
            fieldBPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldBs_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldBs));
            fieldBPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldBs_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldBs));
            fieldBPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldBs_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldBs));
            fieldBPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldBs_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldBs));

            var fieldCPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldCs, L("FieldCs"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldCs));
            fieldCPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldCs_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldCs));
            fieldCPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldCs_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldCs));
            fieldCPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldCs_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldCs));
            fieldCPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldCs_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldCs));
            fieldCPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldCs_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldCs));
            fieldCPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldCs_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldCs));
            fieldCPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldCs_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldCs));
            fieldCPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_FieldCs_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_FieldCs));

            var priceLevelPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_PriceLevels, L("PriceLevels"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_PriceLevels));
            priceLevelPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_PriceLevels_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_PriceLevels));
            priceLevelPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_PriceLevels_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_PriceLevels));
            priceLevelPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_PriceLevels_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_PriceLevels));
            priceLevelPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_PriceLevels_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_PriceLevels));
            priceLevelPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_PriceLevels_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_PriceLevels));
            priceLevelPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_PriceLevels_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_PriceLevels));
            priceLevelPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_PriceLevels_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_PriceLevels));
            priceLevelPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_PriceLevels_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_PriceLevels));

            var promotionPage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Promotions, L("Promotions"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Promotions));
            promotionPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Promotions_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Promotions));
            promotionPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Promotions_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Promotions));
            promotionPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Promotions_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Promotions));
            promotionPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Promotions_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Promotions));
            promotionPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Promotions_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Promotions));
            promotionPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Promotions_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Promotions));
            promotionPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Promotions_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Promotions));
            promotionPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Promotions_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Promotions));

            var scorePage = itemPage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Scores, L("Scores"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Scores));
            scorePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Scores_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Scores));
            scorePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Scores_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Scores));
            scorePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Scores_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Scores));
            scorePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Scores_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Scores));
            scorePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Scores_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Scores));
            scorePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Scores_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Scores));
            scorePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Scores_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Scores));
            scorePage.CreateChildPermission(PermissionNames.Pages_Setup_Items_Scores_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Items_Scores));

            #endregion

            var paymentMethodPage = setupPage.CreateChildPermission(PermissionNames.Pages_Setup_PaymentMethods, L("PaymentMethods"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_PaymentMethods));
            paymentMethodPage.CreateChildPermission(PermissionNames.Pages_Setup_PaymentMethods_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_PaymentMethods));
            paymentMethodPage.CreateChildPermission(PermissionNames.Pages_Setup_PaymentMethods_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Setup_PaymentMethods, AppFeatures.Accounting_ChartOfAccounts));
            paymentMethodPage.CreateChildPermission(PermissionNames.Pages_Setup_PaymentMethods_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Setup_PaymentMethods, AppFeatures.Accounting_ChartOfAccounts));
            paymentMethodPage.CreateChildPermission(PermissionNames.Pages_Setup_PaymentMethods_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_PaymentMethods));
            paymentMethodPage.CreateChildPermission(PermissionNames.Pages_Setup_PaymentMethods_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_PaymentMethods));
            paymentMethodPage.CreateChildPermission(PermissionNames.Pages_Setup_PaymentMethods_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_PaymentMethods));

            var classPage = setupPage.CreateChildPermission(PermissionNames.Pages_Setup_Classes, L("Classes"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Classes));
            classPage.CreateChildPermission(PermissionNames.Pages_Setup_Classes_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Classes));
            classPage.CreateChildPermission(PermissionNames.Pages_Setup_Classes_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Classes));
            classPage.CreateChildPermission(PermissionNames.Pages_Setup_Classes_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Classes));
            classPage.CreateChildPermission(PermissionNames.Pages_Setup_Classes_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Classes));
            classPage.CreateChildPermission(PermissionNames.Pages_Setup_Classes_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Classes));
            classPage.CreateChildPermission(PermissionNames.Pages_Setup_Classes_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Classes));
            classPage.CreateChildPermission(PermissionNames.Pages_Setup_Classes_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Classes));
            classPage.CreateChildPermission(PermissionNames.Pages_Setup_Classes_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Classes));

            var warehousePage = setupPage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses, L("Warehouses"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses));
            warehousePage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses));
            warehousePage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses));
            warehousePage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses));
            warehousePage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses));
            warehousePage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses));
            warehousePage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses));
            warehousePage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses));
            warehousePage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses));

            var warehousesSlotPage = setupPage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Slots, L("Slots"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses_Slots));
            warehousesSlotPage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Slots_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses_Slots));
            warehousesSlotPage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Slots_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses_Slots));
            warehousesSlotPage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Slots_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses_Slots));
            warehousesSlotPage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Slots_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses_Slots));
            warehousesSlotPage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Slots_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses_Slots));
            warehousesSlotPage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Slots_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses_Slots));
            warehousesSlotPage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Slots_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses_Slots));
            warehousesSlotPage.CreateChildPermission(PermissionNames.Pages_Setup_Warehouses_Slots_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Warehouses_Slots));

            var formTemplatePage = setupPage.CreateChildPermission(PermissionNames.Pages_Setup_FormTemplates, L("FormTemplates"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_FormTemplates));
            formTemplatePage.CreateChildPermission(PermissionNames.Pages_Setup_FormTemplates_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_FormTemplates));
            formTemplatePage.CreateChildPermission(PermissionNames.Pages_Setup_FormTemplates_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_FormTemplates));
            formTemplatePage.CreateChildPermission(PermissionNames.Pages_Setup_FormTemplates_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_FormTemplates));
            formTemplatePage.CreateChildPermission(PermissionNames.Pages_Setup_FormTemplates_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_FormTemplates));
            formTemplatePage.CreateChildPermission(PermissionNames.Pages_Setup_FormTemplates_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_FormTemplates));
            formTemplatePage.CreateChildPermission(PermissionNames.Pages_Setup_FormTemplates_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_FormTemplates));

            var taxPage = setupPage.CreateChildPermission(PermissionNames.Pages_Setup_Taxes, L("Taxes"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Taxes));
            taxPage.CreateChildPermission(PermissionNames.Pages_Setup_Taxes_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Taxes));
            taxPage.CreateChildPermission(PermissionNames.Pages_Setup_Taxes_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Taxes));
            taxPage.CreateChildPermission(PermissionNames.Pages_Setup_Taxes_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Taxes));
            taxPage.CreateChildPermission(PermissionNames.Pages_Setup_Taxes_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Taxes));
            taxPage.CreateChildPermission(PermissionNames.Pages_Setup_Taxes_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Taxes));
            taxPage.CreateChildPermission(PermissionNames.Pages_Setup_Taxes_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Taxes));
            taxPage.CreateChildPermission(PermissionNames.Pages_Setup_Taxes_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Taxes));
            taxPage.CreateChildPermission(PermissionNames.Pages_Setup_Taxes_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Setup_Taxes));

            #endregion

            #region Tanant

            #region Company

            var companyPage = page.CreateChildPermission(PermissionNames.Pages_Company, L("Company"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company));

            var companySettingPage = companyPage.CreateChildPermission(PermissionNames.Pages_Company_CompanySetting, L("CompanySetting"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company));
            companySettingPage.CreateChildPermission(PermissionNames.Pages_Company_CompanySetting_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company));

            var branchPage = companyPage.CreateChildPermission(PermissionNames.Pages_Company_Branches, L("Branches"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_Branches));
            branchPage.CreateChildPermission(PermissionNames.Pages_Company_Branches_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_Branches));
            branchPage.CreateChildPermission(PermissionNames.Pages_Company_Branches_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_Branches));
            branchPage.CreateChildPermission(PermissionNames.Pages_Company_Branches_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_Branches));
            branchPage.CreateChildPermission(PermissionNames.Pages_Company_Branches_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_Branches));
            branchPage.CreateChildPermission(PermissionNames.Pages_Company_Branches_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_Branches));
            branchPage.CreateChildPermission(PermissionNames.Pages_Company_Branches_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_Branches));
            branchPage.CreateChildPermission(PermissionNames.Pages_Company_Branches_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_Branches));
            branchPage.CreateChildPermission(PermissionNames.Pages_Company_Branches_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_Branches));
            branchPage.CreateChildPermission(PermissionNames.Pages_Company_Branches_SetAsDefault, L("SetAsDefault"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Company_Branches));

            #endregion

            #region Files
            var filePage = page.CreateChildPermission(PermissionNames.Pages_Files, L("Files"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            
            var fileList = filePage.CreateChildPermission(PermissionNames.Pages_Files_List, L("FileList"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            fileList.CreateChildPermission(PermissionNames.Pages_Files_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            fileList.CreateChildPermission(PermissionNames.Pages_Files_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            fileList.CreateChildPermission(PermissionNames.Pages_Files_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            fileList.CreateChildPermission(PermissionNames.Pages_Files_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            fileList.CreateChildPermission(PermissionNames.Pages_Files_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            fileList.CreateChildPermission(PermissionNames.Pages_Files_RenameFolder, L("RenameFolder"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            fileList.CreateChildPermission(PermissionNames.Pages_Files_ChangeFolder, L("ChangeFolder"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));

            var folderPage = filePage.CreateChildPermission(PermissionNames.Pages_Files_Folders, L("Folders"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            folderPage.CreateChildPermission(PermissionNames.Pages_Files_Folders_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            folderPage.CreateChildPermission(PermissionNames.Pages_Files_Folders_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
            folderPage.CreateChildPermission(PermissionNames.Pages_Files_Folders_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Files));
           
            #endregion

            #region POSs
            var posPage = page.CreateChildPermission(PermissionNames.Pages_POSs, L("POS"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS));

            var tablePage = posPage.CreateChildPermission(PermissionNames.Pages_POSs_Tables, L("Tables"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables));
            tablePage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables));
            tablePage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables));
            tablePage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables));
            tablePage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables));
            tablePage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables));
            tablePage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables));
            tablePage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables));
            tablePage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables));

            var tableGroupPage = posPage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Groups, L("TableGroups"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables_Groups));
            tableGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Groups_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables_Groups));
            tableGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Groups_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables_Groups));
            tableGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Groups_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables_Groups));
            tableGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Groups_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables_Groups));
            tableGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Groups_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables_Groups));
            tableGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Groups_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables_Groups));
            tableGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Groups_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables_Groups));
            tableGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Tables_Groups_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Tables_Groups));

            var roomPage = posPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms, L("Rooms"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));
            roomPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));
            roomPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));
            roomPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));
            roomPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));
            roomPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));
            roomPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));
            roomPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));
            roomPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));
            roomPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Split, L("Split"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));
            roomPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Merge, L("Merge"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms));

            var roomGroupPage = posPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Groups, L("RoomGroups"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms_Groups));
            roomGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Groups_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms_Groups));
            roomGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Groups_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms_Groups));
            roomGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Groups_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms_Groups));
            roomGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Groups_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms_Groups));
            roomGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Groups_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms_Groups));
            roomGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Groups_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms_Groups));
            roomGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Groups_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms_Groups));
            roomGroupPage.CreateChildPermission(PermissionNames.Pages_POSs_Rooms_Groups_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Rooms_Groups));

            var counterPage = posPage.CreateChildPermission(PermissionNames.Pages_POSs_Counters, L("Counters"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Counters));
            counterPage.CreateChildPermission(PermissionNames.Pages_POSs_Counters_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Counters));
            counterPage.CreateChildPermission(PermissionNames.Pages_POSs_Counters_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Counters));
            counterPage.CreateChildPermission(PermissionNames.Pages_POSs_Counters_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Counters));
            counterPage.CreateChildPermission(PermissionNames.Pages_POSs_Counters_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Counters));
            counterPage.CreateChildPermission(PermissionNames.Pages_POSs_Counters_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Counters));
            counterPage.CreateChildPermission(PermissionNames.Pages_POSs_Counters_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Counters));
            counterPage.CreateChildPermission(PermissionNames.Pages_POSs_Counters_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Counters));
            counterPage.CreateChildPermission(PermissionNames.Pages_POSs_Counters_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Counters));

            var membersCardPage = posPage.CreateChildPermission(PermissionNames.Pages_POSs_MembersCards, L("MembersCards"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_MembersCards));
            membersCardPage.CreateChildPermission(PermissionNames.Pages_POSs_MembersCards_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_MembersCards));
            membersCardPage.CreateChildPermission(PermissionNames.Pages_POSs_MembersCards_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_MembersCards));
            membersCardPage.CreateChildPermission(PermissionNames.Pages_POSs_MembersCards_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_MembersCards));
            membersCardPage.CreateChildPermission(PermissionNames.Pages_POSs_MembersCards_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_MembersCards));
            membersCardPage.CreateChildPermission(PermissionNames.Pages_POSs_MembersCards_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_MembersCards));
            membersCardPage.CreateChildPermission(PermissionNames.Pages_POSs_MembersCards_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_MembersCards));
            membersCardPage.CreateChildPermission(PermissionNames.Pages_POSs_MembersCards_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_MembersCards));
            membersCardPage.CreateChildPermission(PermissionNames.Pages_POSs_MembersCards_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_MembersCards));

            var posSaleOrderPage = posPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders, L("SaleOrders"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_SaleOrders));
            posSaleOrderPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_SaleOrders));
            posSaleOrderPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders_ConvertToInvoice, L("ConvertToInvoice"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_SaleOrders, AppFeatures.POS_Invoices));
            posSaleOrderPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_SaleOrders));
            posSaleOrderPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_SaleOrders));
            posSaleOrderPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_SaleOrders));
            posSaleOrderPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_SaleOrders));
            posSaleOrderPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_SaleOrders));
            posSaleOrderPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_SaleOrders));
            posSaleOrderPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_SaleOrders));
            posSaleOrderPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleOrders_Reorder, L("Reorder"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_SaleOrders));

            var posInvoicePage = posPage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices, L("Invoices"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Invoices));
            var createPOSInvoicePage = posInvoicePage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Invoices));
            createPOSInvoicePage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices_Create_FromSaleOrder, L("FromSaleOrder"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_Invoices, AppFeatures.POS_SaleOrders));
            posInvoicePage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Invoices));
            posInvoicePage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Invoices));
            posInvoicePage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Invoices));
            posInvoicePage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Invoices));
            posInvoicePage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Invoices));
            posInvoicePage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Invoices));
            posInvoicePage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices_CreditSale, L("CreditSale"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Invoices));
            posInvoicePage.CreateChildPermission(PermissionNames.Pages_POSs_Invoices_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.POS_Invoices));

            var posSaleReturnPage = posPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleReturns, L("SaleReturns"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_SaleReturns, AppFeatures.POS_Invoices));
            posSaleReturnPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleReturns_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_SaleReturns, AppFeatures.POS_Invoices));
            posSaleReturnPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleReturns_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_SaleReturns, AppFeatures.POS_Invoices));
            posSaleReturnPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleReturns_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_SaleReturns, AppFeatures.POS_Invoices));
            posSaleReturnPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleReturns_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_SaleReturns, AppFeatures.POS_Invoices));
            posSaleReturnPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleReturns_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_SaleReturns, AppFeatures.POS_Invoices));
            posSaleReturnPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleReturns_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_SaleReturns, AppFeatures.POS_Invoices));
            posSaleReturnPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleReturns_Refund, L("Refund"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_SaleReturns, AppFeatures.POS_Invoices));
            posSaleReturnPage.CreateChildPermission(PermissionNames.Pages_POSs_SaleReturns_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.POS_SaleReturns, AppFeatures.POS_Invoices));
         
            #endregion

            #region Vendors

            var vendorPage = page.CreateChildPermission(PermissionNames.Pages_Vendors, L("Vendors"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors));

            var vendorGroupPage = vendorPage.CreateChildPermission(PermissionNames.Pages_Vendors_VendorGroups, L("VendorGroups"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Groups));
            vendorGroupPage.CreateChildPermission(PermissionNames.Pages_Vendors_VendorGroups_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Groups));
            vendorGroupPage.CreateChildPermission(PermissionNames.Pages_Vendors_VendorGroups_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Groups));
            vendorGroupPage.CreateChildPermission(PermissionNames.Pages_Vendors_VendorGroups_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Groups));
            vendorGroupPage.CreateChildPermission(PermissionNames.Pages_Vendors_VendorGroups_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Groups));
            vendorGroupPage.CreateChildPermission(PermissionNames.Pages_Vendors_VendorGroups_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Groups));
            vendorGroupPage.CreateChildPermission(PermissionNames.Pages_Vendors_VendorGroups_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Groups));
            vendorGroupPage.CreateChildPermission(PermissionNames.Pages_Vendors_VendorGroups_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Groups));
            vendorGroupPage.CreateChildPermission(PermissionNames.Pages_Vendors_VendorGroups_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Groups));

            var vendorListPage = vendorPage.CreateChildPermission(PermissionNames.Pages_Vendors_List, L("VendorList"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors));
            vendorListPage.CreateChildPermission(PermissionNames.Pages_Vendors_List_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors));
            vendorListPage.CreateChildPermission(PermissionNames.Pages_Vendors_List_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors));
            vendorListPage.CreateChildPermission(PermissionNames.Pages_Vendors_List_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors));
            vendorListPage.CreateChildPermission(PermissionNames.Pages_Vendors_List_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors, AppFeatures.Accounting_ChartOfAccounts));
            vendorListPage.CreateChildPermission(PermissionNames.Pages_Vendors_List_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors, AppFeatures.Accounting_ChartOfAccounts));
            vendorListPage.CreateChildPermission(PermissionNames.Pages_Vendors_List_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors));
            vendorListPage.CreateChildPermission(PermissionNames.Pages_Vendors_List_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors));
            vendorListPage.CreateChildPermission(PermissionNames.Pages_Vendors_List_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors));
            vendorListPage.CreateChildPermission(PermissionNames.Pages_Vendors_List_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors));
            vendorListPage.CreateChildPermission(PermissionNames.Pages_Vendors_List_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors));

            var purchaseTypePage = vendorPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseTypes, L("PurchaseTypes"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseTypes));
            purchaseTypePage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseTypes_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseTypes));
            purchaseTypePage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseTypes_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseTypes));
            purchaseTypePage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseTypes_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseTypes));
            purchaseTypePage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseTypes_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseTypes));
            purchaseTypePage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseTypes_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseTypes));
            purchaseTypePage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseTypes_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseTypes));
            purchaseTypePage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseTypes_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseTypes));
            purchaseTypePage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseTypes_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseTypes));

            var purchaseOrderPage = vendorPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders, L("PurchaseOrders"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseOrders));
            purchaseOrderPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseOrders));
            purchaseOrderPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders_ConvertToBill, L("ConvertToBill"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_PurchaseOrders, AppFeatures.Vendors_Bills));
            purchaseOrderPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseOrders));
            purchaseOrderPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseOrders));
            purchaseOrderPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseOrders));
            purchaseOrderPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseOrders));
            purchaseOrderPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseOrders));
            purchaseOrderPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseOrders));
            purchaseOrderPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseOrders));
            purchaseOrderPage.CreateChildPermission(PermissionNames.Pages_Vendors_PurchaseOrders_Reorder, L("Reorder"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_PurchaseOrders));

            var billPage = vendorPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills, L("Bills"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreateAccountBill, L("CreateAccountBill"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            var createPurchaseBillPage = billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreatePurchaseBill, L("CreatePurchaseBill"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            createPurchaseBillPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreatePurchaseBill_FromPurchaseOrder, L("FromPurchaseOrder"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_Bills, AppFeatures.Vendors_PurchaseOrders));
            var createPurchaseBillFromInventoryPurchasePage = createPurchaseBillPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreatePurchaseBill_FromInventoryPurchase, L("FromInventoryPurchase"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_Bills, AppFeatures.Inventories_Receipts));
            createPurchaseBillFromInventoryPurchasePage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreatePurchaseBill_FromInventoryPurchase_EditQty, L("EditQty"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_Bills, AppFeatures.Inventories_Receipts));
            var createPurchaseReturnPage = billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreatePurchaseReturn, L("CreatePurchaseReturn"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            createPurchaseReturnPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreatePurchaseReturn_FromInventoryPurchase, L("FromInventoryPurchase"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_Bills, AppFeatures.Inventories_Receipts));
            var createPurchaseReturnFromInventoryPurchaseReturnPage = createPurchaseReturnPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreatePurchaseReturn_FromInventoryPurchaseReturn, L("FromInventoryPurchaseReturn"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_Bills, AppFeatures.Inventories_Issues));
            createPurchaseReturnFromInventoryPurchaseReturnPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreatePurchaseReturn_FromInventoryPurchaseReturn_EditQty, L("EditQty"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_Bills, AppFeatures.Inventories_Issues));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreateAdvancePayment, L("CreateAdvancePayment"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_CreateDebitNote, L("CreateDebitNote"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_Bills, AppFeatures.Accounting_ChartOfAccounts));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_Bills, AppFeatures.Accounting_ChartOfAccounts));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_PayBill, L("PayBill"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_Bills, AppFeatures.Vendors_BillPayments));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_PaymentHistory, L("PaymentHistory"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_Bills, AppFeatures.Vendors_BillPayments));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));
            billPage.CreateChildPermission(PermissionNames.Pages_Vendors_Bills_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_Bills));

            var billPaymentPage = vendorPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments, L("BillPayments"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_BillPayments));
            billPaymentPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_BillPayments));
            billPaymentPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_BillPayments));
            billPaymentPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments_Edit, L("Edit"),multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_BillPayments));
            billPaymentPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_BillPayments, AppFeatures.Accounting_ChartOfAccounts));
            billPaymentPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_BillPayments, AppFeatures.Accounting_ChartOfAccounts));
            billPaymentPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_BillPayments));
            billPaymentPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_BillPayments));
            billPaymentPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_BillPayments));
            billPaymentPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Vendors_BillPayments));
            billPaymentPage.CreateChildPermission(PermissionNames.Pages_Vendors_BillPayments_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Vendors_BillPayments, AppFeatures.Over24Modify));

            #endregion

            #region Customers

            var customerPage = page.CreateChildPermission(PermissionNames.Pages_Customers, L("Customers"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers));

            var customerGroupPage = customerPage.CreateChildPermission(PermissionNames.Pages_Customers_CustomerGroups, L("CustomerGroups"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Groups));
            customerGroupPage.CreateChildPermission(PermissionNames.Pages_Customers_CustomerGroups_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Groups));
            customerGroupPage.CreateChildPermission(PermissionNames.Pages_Customers_CustomerGroups_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Groups));
            customerGroupPage.CreateChildPermission(PermissionNames.Pages_Customers_CustomerGroups_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Groups));
            customerGroupPage.CreateChildPermission(PermissionNames.Pages_Customers_CustomerGroups_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Groups));
            customerGroupPage.CreateChildPermission(PermissionNames.Pages_Customers_CustomerGroups_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Groups));
            customerGroupPage.CreateChildPermission(PermissionNames.Pages_Customers_CustomerGroups_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Groups));
            customerGroupPage.CreateChildPermission(PermissionNames.Pages_Customers_CustomerGroups_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Groups));
            customerGroupPage.CreateChildPermission(PermissionNames.Pages_Customers_CustomerGroups_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Groups));

            var customerListPage = customerPage.CreateChildPermission(PermissionNames.Pages_Customers_List, L("CustomerList"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers));
            customerListPage.CreateChildPermission(PermissionNames.Pages_Customers_List_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers));
            customerListPage.CreateChildPermission(PermissionNames.Pages_Customers_List_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers));
            customerListPage.CreateChildPermission(PermissionNames.Pages_Customers_List_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers));
            customerListPage.CreateChildPermission(PermissionNames.Pages_Customers_List_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers, AppFeatures.Accounting_ChartOfAccounts));
            customerListPage.CreateChildPermission(PermissionNames.Pages_Customers_List_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers, AppFeatures.Accounting_ChartOfAccounts));
            customerListPage.CreateChildPermission(PermissionNames.Pages_Customers_List_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers));
            customerListPage.CreateChildPermission(PermissionNames.Pages_Customers_List_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers));
            customerListPage.CreateChildPermission(PermissionNames.Pages_Customers_List_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers));
            customerListPage.CreateChildPermission(PermissionNames.Pages_Customers_List_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers));
            customerListPage.CreateChildPermission(PermissionNames.Pages_Customers_List_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers));

            var saleTypePage = customerPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleTypes, L("SaleTypes"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleTypes));
            saleTypePage.CreateChildPermission(PermissionNames.Pages_Customers_SaleTypes_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleTypes));
            saleTypePage.CreateChildPermission(PermissionNames.Pages_Customers_SaleTypes_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleTypes));
            saleTypePage.CreateChildPermission(PermissionNames.Pages_Customers_SaleTypes_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleTypes));
            saleTypePage.CreateChildPermission(PermissionNames.Pages_Customers_SaleTypes_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleTypes));
            saleTypePage.CreateChildPermission(PermissionNames.Pages_Customers_SaleTypes_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleTypes));
            saleTypePage.CreateChildPermission(PermissionNames.Pages_Customers_SaleTypes_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleTypes));
            saleTypePage.CreateChildPermission(PermissionNames.Pages_Customers_SaleTypes_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleTypes));
            saleTypePage.CreateChildPermission(PermissionNames.Pages_Customers_SaleTypes_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleTypes));

            var quotationPage = customerPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations, L("Quotations"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Quotations));
            quotationPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Quotations));
            quotationPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations_ConvertToContract, L("ConvertToContract"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Quotations, AppFeatures.Customers_Contracts));
            quotationPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations_ConvertToInvoice, L("ConvertToInvoice"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Quotations, AppFeatures.Customers_Invoices));
            quotationPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Quotations));
            quotationPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Quotations));
            quotationPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Quotations));
            quotationPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Quotations));
            quotationPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Quotations));
            quotationPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Quotations));
            quotationPage.CreateChildPermission(PermissionNames.Pages_Customers_Quotations_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Quotations, AppFeatures.Over24Modify));

            var contractPage = customerPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts, L("Contracts"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            var createContractPage = contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            createContractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Create_FromQuotation, L("FromQuotation"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Contracts, AppFeatures.Customers_Quotations));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_ConvertToInvoice, L("ConvertToInvoice"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Contracts, AppFeatures.Customers_Invoices));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Renew, L("Renew"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts));
            contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Contracts, AppFeatures.Over24Modify));

            var contrackAlertPage = contractPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Alerts, L("ContractAlerts"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts_Alerts));
            contrackAlertPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Alerts_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts_Alerts));
            contrackAlertPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Alerts_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts_Alerts));
            contrackAlertPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Alerts_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts_Alerts));
            contrackAlertPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Alerts_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts_Alerts));
            contrackAlertPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Alerts_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts_Alerts));
            contrackAlertPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Alerts_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts_Alerts));
            contrackAlertPage.CreateChildPermission(PermissionNames.Pages_Customers_Contracts_Alerts_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Contracts_Alerts));

            var saleOrderPage = customerPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders, L("SaleOrders"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleOrders));
            saleOrderPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleOrders));
            saleOrderPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders_ConvertToInvoice, L("ConvertToInvoice"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_SaleOrders, AppFeatures.Customers_Invoices));
            saleOrderPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleOrders));
            saleOrderPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleOrders));
            saleOrderPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleOrders));
            saleOrderPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleOrders));
            saleOrderPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleOrders));
            saleOrderPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleOrders));
            saleOrderPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders_Reorder, L("Reorder"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_SaleOrders));
            saleOrderPage.CreateChildPermission(PermissionNames.Pages_Customers_SaleOrders_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_SaleOrders, AppFeatures.Over24Modify));

            var invoicePage = customerPage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices, L("Invoices"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateAccountInvoice, L("CreateAccountInovice"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            var crateInvoicePage = invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateSaleInvoice, L("CreateSaleInovice"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            crateInvoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateSaleInvoice_FromQuotation, L("FromQuotation"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Customers_Quotations));
            crateInvoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateSaleInvoice_FromContract, L("FromContract"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Customers_Contracts));
            crateInvoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateSaleInvoice_FromSaleOrder, L("FromSaleOrder"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Customers_SaleOrders));
            var createInvoiceFromInventorySale = crateInvoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateSaleInvoice_FromInventorySale, L("FromInventorySale"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Inventories_Issues));
            createInvoiceFromInventorySale.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateSaleInvoice_FromInventorySale_EditQty, L("EditQty"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Inventories_Issues));
            var createSaleReturnPage = invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateSaleReturn, L("CreateSaleReturn"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            createSaleReturnPage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateSaleReturn_FromInventorySale, L("FromInventorySale"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Inventories_Receipts));
            var createSaleReturnFromInventorySaleReturnPage = createSaleReturnPage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateSaleReturn_FromInventorySaleReturn, L("FromInventorySaleReturn"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Inventories_Receipts));
            createSaleReturnFromInventorySaleReturnPage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateSaleReturn_FromInventorySaleReturn_EditQty, L("EditQty"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Inventories_Receipts));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateCustomerDeposit, L("CreateCustomerDeposit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_CreateCreditNote, L("CreateCreditNote"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Accounting_ChartOfAccounts));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Accounting_ChartOfAccounts));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_Payment, L("Payment"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Customers_ReceivePayments));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_PaymentHistory, L("PaymentHistory"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Customers_ReceivePayments));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_Invoices));
            invoicePage.CreateChildPermission(PermissionNames.Pages_Customers_Invoices_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_Invoices, AppFeatures.Over24Modify));

            var receivePaymentPage = customerPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments, L("ReceivePayments"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_ReceivePayments));
            receivePaymentPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_ReceivePayments));
            receivePaymentPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_ReceivePayments));
            receivePaymentPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_ReceivePayments));
            receivePaymentPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_ReceivePayments, AppFeatures.Accounting_ChartOfAccounts));
            receivePaymentPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_ReceivePayments, AppFeatures.Accounting_ChartOfAccounts));
            receivePaymentPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_ReceivePayments));
            receivePaymentPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_ReceivePayments));
            receivePaymentPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_ReceivePayments));
            receivePaymentPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Customers_ReceivePayments));
            receivePaymentPage.CreateChildPermission(PermissionNames.Pages_Customers_ReceivePayments_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Customers_ReceivePayments, AppFeatures.Over24Modify));

            #endregion

            #region Sale Representatives
            var saleRepresentativePage = page.CreateChildPermission(PermissionNames.Pages_SaleRepresentatives, L("SaleRepresentatives"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleRepresentatives));
            saleRepresentativePage.CreateChildPermission(PermissionNames.Pages_SaleRepresentatives_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleRepresentatives));
            saleRepresentativePage.CreateChildPermission(PermissionNames.Pages_SaleRepresentatives_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleRepresentatives));
            saleRepresentativePage.CreateChildPermission(PermissionNames.Pages_SaleRepresentatives_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleRepresentatives));
            saleRepresentativePage.CreateChildPermission(PermissionNames.Pages_SaleRepresentatives_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleRepresentatives));
            saleRepresentativePage.CreateChildPermission(PermissionNames.Pages_SaleRepresentatives_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleRepresentatives));
            saleRepresentativePage.CreateChildPermission(PermissionNames.Pages_SaleRepresentatives_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleRepresentatives));
            saleRepresentativePage.CreateChildPermission(PermissionNames.Pages_SaleRepresentatives_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleRepresentatives));
            saleRepresentativePage.CreateChildPermission(PermissionNames.Pages_SaleRepresentatives_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleRepresentatives));

            var saleCommissionPage = page.CreateChildPermission(PermissionNames.Pages_SaleCommissions, L("SaleCommissions"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleCommissions));
            saleCommissionPage.CreateChildPermission(PermissionNames.Pages_SaleCommissions_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleCommissions));
            saleCommissionPage.CreateChildPermission(PermissionNames.Pages_SaleCommissions_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleCommissions));
            saleCommissionPage.CreateChildPermission(PermissionNames.Pages_SaleCommissions_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleCommissions));
            saleCommissionPage.CreateChildPermission(PermissionNames.Pages_SaleCommissions_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleCommissions));
            saleCommissionPage.CreateChildPermission(PermissionNames.Pages_SaleCommissions_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleCommissions));
            saleCommissionPage.CreateChildPermission(PermissionNames.Pages_SaleCommissions_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleCommissions));
            saleCommissionPage.CreateChildPermission(PermissionNames.Pages_SaleCommissions_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.SaleCommissions));
            #endregion

            #region Employees
            var employeePage = page.CreateChildPermission(PermissionNames.Pages_Employees, L("Employees"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees));

            var employeeListPage = employeePage.CreateChildPermission(PermissionNames.Pages_Employees_List, L("EmployeeList"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees));
            employeeListPage.CreateChildPermission(PermissionNames.Pages_Employees_List_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees));
            employeeListPage.CreateChildPermission(PermissionNames.Pages_Employees_List_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees));
            employeeListPage.CreateChildPermission(PermissionNames.Pages_Employees_List_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees));
            employeeListPage.CreateChildPermission(PermissionNames.Pages_Employees_List_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Employees, AppFeatures.Accounting_ChartOfAccounts));
            employeeListPage.CreateChildPermission(PermissionNames.Pages_Employees_List_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Employees, AppFeatures.Accounting_ChartOfAccounts));
            employeeListPage.CreateChildPermission(PermissionNames.Pages_Employees_List_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees));
            employeeListPage.CreateChildPermission(PermissionNames.Pages_Employees_List_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees));
            employeeListPage.CreateChildPermission(PermissionNames.Pages_Employees_List_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees));
            employeeListPage.CreateChildPermission(PermissionNames.Pages_Employees_List_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees));
            employeeListPage.CreateChildPermission(PermissionNames.Pages_Employees_List_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees));

            var positionPage = customerPage.CreateChildPermission(PermissionNames.Pages_Employees_Positions, L("Positions"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees_Positons));
            positionPage.CreateChildPermission(PermissionNames.Pages_Employees_Positions_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees_Positons));
            positionPage.CreateChildPermission(PermissionNames.Pages_Employees_Positions_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees_Positons));
            positionPage.CreateChildPermission(PermissionNames.Pages_Employees_Positions_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees_Positons));
            positionPage.CreateChildPermission(PermissionNames.Pages_Employees_Positions_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees_Positons));
            positionPage.CreateChildPermission(PermissionNames.Pages_Employees_Positions_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees_Positons));
            positionPage.CreateChildPermission(PermissionNames.Pages_Employees_Positions_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees_Positons));
            positionPage.CreateChildPermission(PermissionNames.Pages_Employees_Positions_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees_Positons));
            positionPage.CreateChildPermission(PermissionNames.Pages_Employees_Positions_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Employees_Positons));

            #endregion


            #region Accounting

            var accountingPage = page.CreateChildPermission(PermissionNames.Pages_Accounting, L("Accounting"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting));

            var chartOfAccountPage = accountingPage.CreateChildPermission(PermissionNames.Pages_Accounting_ChartOfAccounts, L("ChartOfAccounts"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_ChartOfAccounts));
            chartOfAccountPage.CreateChildPermission(PermissionNames.Pages_Accounting_ChartOfAccounts_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_ChartOfAccounts));
            chartOfAccountPage.CreateChildPermission(PermissionNames.Pages_Accounting_ChartOfAccounts_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_ChartOfAccounts));
            chartOfAccountPage.CreateChildPermission(PermissionNames.Pages_Accounting_ChartOfAccounts_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_ChartOfAccounts));
            chartOfAccountPage.CreateChildPermission(PermissionNames.Pages_Accounting_ChartOfAccounts_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_ChartOfAccounts));
            chartOfAccountPage.CreateChildPermission(PermissionNames.Pages_Accounting_ChartOfAccounts_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_ChartOfAccounts));
            chartOfAccountPage.CreateChildPermission(PermissionNames.Pages_Accounting_ChartOfAccounts_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_ChartOfAccounts));
            chartOfAccountPage.CreateChildPermission(PermissionNames.Pages_Accounting_ChartOfAccounts_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_ChartOfAccounts));
            chartOfAccountPage.CreateChildPermission(PermissionNames.Pages_Accounting_ChartOfAccounts_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_ChartOfAccounts));

            var journalPage = accountingPage.CreateChildPermission(PermissionNames.Pages_Accounting_Journals, L("Journals"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Journals));
            journalPage.CreateChildPermission(PermissionNames.Pages_Accounting_Journals_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Journals));
            journalPage.CreateChildPermission(PermissionNames.Pages_Accounting_Journals_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Journals));
            journalPage.CreateChildPermission(PermissionNames.Pages_Accounting_Journals_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Journals));
            journalPage.CreateChildPermission(PermissionNames.Pages_Accounting_Journals_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Journals));
            journalPage.CreateChildPermission(PermissionNames.Pages_Accounting_Journals_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Journals));
            journalPage.CreateChildPermission(PermissionNames.Pages_Accounting_Journals_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Journals));
            journalPage.CreateChildPermission(PermissionNames.Pages_Accounting_Journals_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Journals));
            journalPage.CreateChildPermission(PermissionNames.Pages_Accounting_Journals_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Accounting_Journals, AppFeatures.Over24Modify));

            #endregion

            #region Banks
            var bankPage = accountingPage.CreateChildPermission(PermissionNames.Pages_Accounting_Banks, L("Banks"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Banks));
            bankPage.CreateChildPermission(PermissionNames.Pages_Accounting_Banks_CreateDeposit, L("CreateDeposit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Banks));
            bankPage.CreateChildPermission(PermissionNames.Pages_Accounting_Banks_CreateWithdraw, L("CreateWithdraw"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Banks));
            bankPage.CreateChildPermission(PermissionNames.Pages_Accounting_Banks_CreateTransfer, L("CreateTransfer"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Banks));
            bankPage.CreateChildPermission(PermissionNames.Pages_Accounting_Banks_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Banks));
            bankPage.CreateChildPermission(PermissionNames.Pages_Accounting_Banks_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Banks));
            bankPage.CreateChildPermission(PermissionNames.Pages_Accounting_Banks_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Banks));
            bankPage.CreateChildPermission(PermissionNames.Pages_Accounting_Banks_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Banks));
            bankPage.CreateChildPermission(PermissionNames.Pages_Accounting_Banks_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Accounting_Banks));
            bankPage.CreateChildPermission(PermissionNames.Pages_Accounting_Banks_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Accounting_Banks, AppFeatures.Over24Modify));

            #endregion

            #region Loans
            var loanPage = page.CreateChildPermission(PermissionNames.Pages_Loans, L("Loans"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans));

            var collateralPage = loanPage.CreateChildPermission(PermissionNames.Pages_Loans_Collaterals, L("Collaterals"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Collaterals));
            collateralPage.CreateChildPermission(PermissionNames.Pages_Loans_Collaterals_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Collaterals));
            collateralPage.CreateChildPermission(PermissionNames.Pages_Loans_Collaterals_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Collaterals));
            collateralPage.CreateChildPermission(PermissionNames.Pages_Loans_Collaterals_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Collaterals));
            collateralPage.CreateChildPermission(PermissionNames.Pages_Loans_Collaterals_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Collaterals));
            collateralPage.CreateChildPermission(PermissionNames.Pages_Loans_Collaterals_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Collaterals));
            collateralPage.CreateChildPermission(PermissionNames.Pages_Loans_Collaterals_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Collaterals));
            collateralPage.CreateChildPermission(PermissionNames.Pages_Loans_Collaterals_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Collaterals));
            collateralPage.CreateChildPermission(PermissionNames.Pages_Loans_Collaterals_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Collaterals));

            var interestRatePage = loanPage.CreateChildPermission(PermissionNames.Pages_Loans_InterestRates, L("InterestRates"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_InterestRates));
            interestRatePage.CreateChildPermission(PermissionNames.Pages_Loans_InterestRates_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_InterestRates));
            interestRatePage.CreateChildPermission(PermissionNames.Pages_Loans_InterestRates_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_InterestRates));
            interestRatePage.CreateChildPermission(PermissionNames.Pages_Loans_InterestRates_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_InterestRates));
            interestRatePage.CreateChildPermission(PermissionNames.Pages_Loans_InterestRates_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_InterestRates));
            interestRatePage.CreateChildPermission(PermissionNames.Pages_Loans_InterestRates_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_InterestRates));
            interestRatePage.CreateChildPermission(PermissionNames.Pages_Loans_InterestRates_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_InterestRates));
            interestRatePage.CreateChildPermission(PermissionNames.Pages_Loans_InterestRates_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_InterestRates));
            interestRatePage.CreateChildPermission(PermissionNames.Pages_Loans_InterestRates_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_InterestRates));

            var penaltyPage = loanPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties, L("Penalties"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties));
            penaltyPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties));
            penaltyPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties));
            penaltyPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties));
            penaltyPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties));
            penaltyPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties));
            penaltyPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties));
            penaltyPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties));
            penaltyPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties));

            var penaltyAlertPage = penaltyPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Alerts, L("Alerts"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties_Alerts));
            penaltyAlertPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Alerts_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties_Alerts));
            penaltyAlertPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Alerts_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties_Alerts));
            penaltyAlertPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Alerts_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties_Alerts));
            penaltyAlertPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Alerts_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties_Alerts));
            penaltyAlertPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Alerts_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties_Alerts));
            penaltyAlertPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Alerts_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties_Alerts));
            penaltyAlertPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Alerts_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties_Alerts));
            penaltyAlertPage.CreateChildPermission(PermissionNames.Pages_Loans_Penalties_Alerts_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Penalties_Alerts));

            var loanListPage = loanPage.CreateChildPermission(PermissionNames.Pages_Loans_List, L("LoanList"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans));
            var loanCreatePage = loanListPage.CreateChildPermission(PermissionNames.Pages_Loans_List_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans));
            loanCreatePage.CreateChildPermission(PermissionNames.Pages_Loans_List_Create_FromContract, L("FromContract"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans));
            loanListPage.CreateChildPermission(PermissionNames.Pages_Loans_List_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans));
            loanListPage.CreateChildPermission(PermissionNames.Pages_Loans_List_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans));
            loanListPage.CreateChildPermission(PermissionNames.Pages_Loans_List_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Loans, AppFeatures.Accounting_ChartOfAccounts));
            loanListPage.CreateChildPermission(PermissionNames.Pages_Loans_List_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Loans, AppFeatures.Accounting_ChartOfAccounts));
            loanListPage.CreateChildPermission(PermissionNames.Pages_Loans_List_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans));
            loanListPage.CreateChildPermission(PermissionNames.Pages_Loans_List_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans));
            loanListPage.CreateChildPermission(PermissionNames.Pages_Loans_List_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans));
            loanListPage.CreateChildPermission(PermissionNames.Pages_Loans_List_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans));
            loanListPage.CreateChildPermission(PermissionNames.Pages_Loans_List_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Loans, AppFeatures.Over24Modify));

            var loanPaymentPage = loanPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments, L("Payments"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Payments));
            loanPaymentPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Payments));
            loanPaymentPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Payments));
            loanPaymentPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Payments));
            loanPaymentPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Loans_Payments, AppFeatures.Accounting_ChartOfAccounts));
            loanPaymentPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Loans_Payments, AppFeatures.Accounting_ChartOfAccounts));
            loanPaymentPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Payments));
            loanPaymentPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Payments));
            loanPaymentPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Payments));
            loanPaymentPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Loans_Payments));
            loanPaymentPage.CreateChildPermission(PermissionNames.Pages_Loans_Payments_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Loans_Payments, AppFeatures.Over24Modify));

            #endregion


            #region Inventories
            var inventoriesPage = page.CreateChildPermission(PermissionNames.Pages_Inventories, L("InventoryTransactions"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories));
            var inventoriesCommonPage = inventoriesPage.CreateChildPermission(PermissionNames.Pages_Inventories_Common, L("Common"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories));
            inventoriesCommonPage.CreateChildPermission(PermissionNames.Pages_Inventories_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories, AppFeatures.Accounting_ChartOfAccounts));
            inventoriesCommonPage.CreateChildPermission(PermissionNames.Pages_Inventories_SeeAccount, L("SeeAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories, AppFeatures.Accounting_ChartOfAccounts));
            inventoriesCommonPage.CreateChildPermission(PermissionNames.Pages_Inventories_EditPrice, L("EditPrice"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories));
            inventoriesCommonPage.CreateChildPermission(PermissionNames.Pages_Inventories_SeePrice, L("SeePrice"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories));
            inventoriesCommonPage.CreateChildPermission(PermissionNames.Pages_Inventories_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories));


            #region Receipts
            var itemReceiptsPage = inventoriesPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts, L("Receipts"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts));

            var itemReceiptsPurchasePage = itemReceiptsPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Purchases, L("Purchases"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemReceiptsPurchasePage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Purchases_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemReceiptsPurchasePage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Purchases_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemReceiptsPurchasePage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Purchases_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemReceiptsPurchasePage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Purchases_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemReceiptsPurchasePage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Purchases_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemReceiptsPurchasePage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Purchases_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Receipts_Purchases, AppFeatures.Over24Modify));

            var itemIssuesPurchaseReturnPage = itemReceiptsPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_PurchaseReturns, L("PurchaseReturns"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemIssuesPurchaseReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_PurchaseReturns_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemIssuesPurchaseReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_PurchaseReturns_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemIssuesPurchaseReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_PurchaseReturns_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemIssuesPurchaseReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_PurchaseReturns_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemIssuesPurchaseReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_PurchaseReturns_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Purchases));
            itemIssuesPurchaseReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_PurchaseReturns_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Receipts_Purchases, AppFeatures.Over24Modify));

            var itemReceiptsAdjustmentPage = itemReceiptsPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Adjustments, L("ReceiptAdjustments"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Adjustments));
            itemReceiptsAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Adjustments_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Adjustments));
            itemReceiptsAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Adjustments_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Adjustments));
            itemReceiptsAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Adjustments_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Adjustments));
            itemReceiptsAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Adjustments_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Adjustments));
            itemReceiptsAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Adjustments_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Adjustments));
            itemReceiptsAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Adjustments_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Receipts_Adjustments, AppFeatures.Over24Modify));

            var itemReceiptsOtherPage = itemReceiptsPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Others, L("ReceiptOthers"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Others));
            itemReceiptsOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Others_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Others));
            itemReceiptsOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Others_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Others));
            itemReceiptsOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Others_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Others));
            itemReceiptsOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Others_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Others));
            itemReceiptsOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Others_ImportExcel, L("ImportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Others));
            itemReceiptsOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Others_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Receipts_Others));
            itemReceiptsOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_Others_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Receipts_Others, AppFeatures.Over24Modify));

            #endregion

            #region Issues
            var itemIssuesPage = inventoriesPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues, L("Issues"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues));

            var itemIssuesSalePage = itemIssuesPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Sales, L("Sales"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemIssuesSalePage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Sales_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemIssuesSalePage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Sales_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemIssuesSalePage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Sales_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemIssuesSalePage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Sales_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemIssuesSalePage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Sales_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemIssuesSalePage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Sales_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Issues_Sales, AppFeatures.Over24Modify));

            var itemReceiptsSaleReturnPage = itemIssuesPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_SaleReturns, L("SaleReturns"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemReceiptsSaleReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_SaleReturns_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemReceiptsSaleReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_SaleReturns_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemReceiptsSaleReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_SaleReturns_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemReceiptsSaleReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_SaleReturns_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemReceiptsSaleReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_SaleReturns_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Sales));
            itemReceiptsSaleReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_SaleReturns_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Issues_Sales, AppFeatures.Over24Modify));

            var itemIssueCustomerBorrowPage = itemIssuesPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_CustomerBorrows, L("CustomerBorrows"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemIssueCustomerBorrowPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_CustomerBorrows_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemIssueCustomerBorrowPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_CustomerBorrows_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemIssueCustomerBorrowPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_CustomerBorrows_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemIssueCustomerBorrowPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_CustomerBorrows_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemIssueCustomerBorrowPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_CustomerBorrows_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemIssueCustomerBorrowPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_CustomerBorrows_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Issues_CustomerBorrows, AppFeatures.Over24Modify));

            var itemReceiptCustomerBorrowReturnPage = itemIssuesPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_CustomerBorrowReturns, L("CustomerBorrowReturns"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemReceiptCustomerBorrowReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_CustomerBorrowReturns_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemReceiptCustomerBorrowReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_CustomerBorrowReturns_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemReceiptCustomerBorrowReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_CustomerBorrowReturns_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemReceiptCustomerBorrowReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_CustomerBorrowReturns_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemReceiptCustomerBorrowReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_CustomerBorrowReturns_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_CustomerBorrows));
            itemReceiptCustomerBorrowReturnPage.CreateChildPermission(PermissionNames.Pages_Inventories_Receipts_CustomerBorrowReturns_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Issues_CustomerBorrows, AppFeatures.Over24Modify));

            var itemIssuesAdjustmentPage = itemIssuesPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Adjustments, L("IssueAdjustments"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Adjustments));
            itemIssuesAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Adjustments_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Adjustments));
            itemIssuesAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Adjustments_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Adjustments));
            itemIssuesAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Adjustments_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Adjustments));
            itemIssuesAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Adjustments_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Adjustments));
            itemIssuesAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Adjustments_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Adjustments));
            itemIssuesAdjustmentPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Adjustments_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Issues_Adjustments, AppFeatures.Over24Modify));

            var itemIssuesOtherPage = itemIssuesPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Others, L("IssueOthers"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Others));
            itemIssuesOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Others_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Others));
            itemIssuesOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Others_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Others));
            itemIssuesOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Others_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Others));
            itemIssuesOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Others_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Others));
            itemIssuesOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Others_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Issues_Others));
            itemIssuesOtherPage.CreateChildPermission(PermissionNames.Pages_Inventories_Issues_Others_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Issues_Others, AppFeatures.Over24Modify));

            #endregion

            #region Transfers
            var itemTransferPage = inventoriesPage.CreateChildPermission(PermissionNames.Pages_Inventories_Transfers, L("Transfers"),multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Transfers));
            itemTransferPage.CreateChildPermission(PermissionNames.Pages_Inventories_Transfers_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Transfers));
            itemTransferPage.CreateChildPermission(PermissionNames.Pages_Inventories_Transfers_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Transfers));
            itemTransferPage.CreateChildPermission(PermissionNames.Pages_Inventories_Transfers_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Transfers));
            itemTransferPage.CreateChildPermission(PermissionNames.Pages_Inventories_Transfers_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Transfers));
            itemTransferPage.CreateChildPermission(PermissionNames.Pages_Inventories_Transfers_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Inventories_Transfers));
            itemTransferPage.CreateChildPermission(PermissionNames.Pages_Inventories_Transfers_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Inventories_Transfers, AppFeatures.Over24Modify));
            #endregion

            #region Productions
            var productionPage = page.CreateChildPermission(PermissionNames.Pages_Productions, L("Productions"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions));
            
            var productionOrderPage = productionPage.CreateChildPermission(PermissionNames.Pages_Productions_Orders, L("ProductionOrders"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Orders));
            productionOrderPage.CreateChildPermission(PermissionNames.Pages_Productions_Orders_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Orders));
            productionOrderPage.CreateChildPermission(PermissionNames.Pages_Productions_Orders_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Orders));
            productionOrderPage.CreateChildPermission(PermissionNames.Pages_Productions_Orders_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Orders));
            productionOrderPage.CreateChildPermission(PermissionNames.Pages_Productions_Orders_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Orders));
            productionOrderPage.CreateChildPermission(PermissionNames.Pages_Productions_Orders_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Orders));
            productionOrderPage.CreateChildPermission(PermissionNames.Pages_Productions_Orders_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Productions_Orders, AppFeatures.Over24Modify));

            var productionsLinePage = productionPage.CreateChildPermission(PermissionNames.Pages_Productions_Lines, L("ProductionLines"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Lines));
            productionsLinePage.CreateChildPermission(PermissionNames.Pages_Productions_Lines_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Lines));
            productionsLinePage.CreateChildPermission(PermissionNames.Pages_Productions_Lines_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Lines));
            productionsLinePage.CreateChildPermission(PermissionNames.Pages_Productions_Lines_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Lines));
            productionsLinePage.CreateChildPermission(PermissionNames.Pages_Productions_Lines_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Lines));
            productionsLinePage.CreateChildPermission(PermissionNames.Pages_Productions_Lines_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Lines));
            productionsLinePage.CreateChildPermission(PermissionNames.Pages_Productions_Lines_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Lines));

            var productionListPage = productionPage.CreateChildPermission(PermissionNames.Pages_Productions_List, L("ProductionList"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions));
            productionListPage.CreateChildPermission(PermissionNames.Pages_Productions_List_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions));
            productionListPage.CreateChildPermission(PermissionNames.Pages_Productions_List_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions));
            productionListPage.CreateChildPermission(PermissionNames.Pages_Productions_List_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions));
            productionListPage.CreateChildPermission(PermissionNames.Pages_Productions_List_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions));
            productionListPage.CreateChildPermission(PermissionNames.Pages_Productions_List_Void, L("Void"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions));
            productionListPage.CreateChildPermission(PermissionNames.Pages_Productions_List_Over24Modify, L("Over24Modify"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(requiresAll: true, AppFeatures.Productions, AppFeatures.Over24Modify));

            var productionsProcessPage = productionPage.CreateChildPermission(PermissionNames.Pages_Productions_Processes, L("ProductionProcesses"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Processes));
            productionsProcessPage.CreateChildPermission(PermissionNames.Pages_Productions_Processes_Create, L("Create"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Processes));
            productionsProcessPage.CreateChildPermission(PermissionNames.Pages_Productions_Processes_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Processes));
            productionsProcessPage.CreateChildPermission(PermissionNames.Pages_Productions_Processes_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Processes));
            productionsProcessPage.CreateChildPermission(PermissionNames.Pages_Productions_Processes_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Processes));
            productionsProcessPage.CreateChildPermission(PermissionNames.Pages_Productions_Processes_Enable, L("Enable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Processes));
            productionsProcessPage.CreateChildPermission(PermissionNames.Pages_Productions_Processes_Disable, L("Disable"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Productions_Processes));
            #endregion

            #endregion

            #region Reports

            var reportPage = page.CreateChildPermission(PermissionNames.Pages_Reports, L("Reports"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports));

            var reportCommonPage = reportPage.CreateChildPermission(PermissionNames.Pages_Reports_Common, L("Common"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports));
            reportCommonPage.CreateChildPermission(PermissionNames.Pages_Reports_See, L("See"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports));
            reportCommonPage.CreateChildPermission(PermissionNames.Pages_Reports_ModifyAllTemplates, L("ModifyAllTemplates"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports));

            #region Vendor Reports
            var vendorReportPage = reportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors, L("Vendors"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors));

            var purchaseBillReportPage = vendorReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBills, L("PurchaseBills"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBills));
            purchaseBillReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBills_View, L("View"),multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBills));
            purchaseBillReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBills_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBills));
            purchaseBillReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBills_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBills));
            purchaseBillReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBills_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBills));
            purchaseBillReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBills_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBills));

            var purchaseBillDetailReportPage = vendorReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBillDetail, L("PurchaseBillDetail"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBillDetail));
            purchaseBillDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBillDetail_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBillDetail));
            purchaseBillDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBillDetail_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBillDetail));
            purchaseBillDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBillDetail_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBillDetail));
            purchaseBillDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBillDetail_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBillDetail));
            purchaseBillDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_PurchaseBillDetail_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_PurchaseBillDetail));

            var vendorAgingReportPage = vendorReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_VendorAging, L("VendorAging"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_Aging));
            vendorAgingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_VendorAging_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_Aging));
            vendorAgingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_VendorAging_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_Aging));
            vendorAgingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_VendorAging_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_Aging));
            vendorAgingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_VendorAging_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_Aging));
            vendorAgingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Vendors_VendorAging_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Vendors_Aging));

            #endregion

            #region Customer Reports
            var customerReportPage = reportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers, L("Customers"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers));

            var saleInvoiceReportPage = customerReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoices, L("SaleInvoices"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoices));
            saleInvoiceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoices_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoices));
            saleInvoiceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoices_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoices));
            saleInvoiceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoices_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoices));
            saleInvoiceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoices_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoices));
            saleInvoiceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoices_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoices));

            var saleInvoiceDetailReportPage = customerReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoiceDetail, L("SaleInvoiceDetail"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoiceDetail));
            saleInvoiceDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoiceDetail_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoiceDetail));
            saleInvoiceDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoiceDetail_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoiceDetail));
            saleInvoiceDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoiceDetail_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoiceDetail));
            saleInvoiceDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoiceDetail_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoiceDetail));
            saleInvoiceDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_SaleInvoiceDetail_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_SaleInvoiceDetail));

            var customerAgingReportPage = customerReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_CustomerAging, L("CustomerAging"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_Aging));
            customerAgingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_CustomerAging_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_Aging));
            customerAgingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_CustomerAging_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_Aging));
            customerAgingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_CustomerAging_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_Aging));
            customerAgingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_CustomerAging_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_Aging));
            customerAgingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Customers_CustomerAging_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Customers_Aging));

            #endregion

            #region Accounting Reports

            var accountingReportPage = reportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting, L("Accounting"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting));

            var journalReportPage = accountingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Journals, L("Journals"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Journals));
            journalReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Journals_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Journals));
            journalReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Journals_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Journals));
            journalReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Journals_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Journals));
            journalReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Journals_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Journals));
            journalReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Journals_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Journals));
            journalReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Journals_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Journals));

            var ledgerReportPage = accountingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Ledger, L("Ledger"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Ledger));
            ledgerReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Ledger_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Ledger));
            ledgerReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Ledger_EditAccount, L("EditAccount"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Ledger));
            ledgerReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Ledger_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Ledger));
            ledgerReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Ledger_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Ledger));
            ledgerReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Ledger_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Ledger));
            ledgerReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_Ledger_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_Ledger));

            var profitLossReportPage = accountingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_ProfitLoss, L("ProfitLoss"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_ProfitLoss));
            profitLossReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_ProfitLoss_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_ProfitLoss));
            profitLossReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_ProfitLoss_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_ProfitLoss));
            profitLossReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_ProfitLoss_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_ProfitLoss));
            profitLossReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_ProfitLoss_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_ProfitLoss));
            profitLossReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_ProfitLoss_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_ProfitLoss));

            var retainEarningReportPage = accountingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_RetainEarning, L("RetainEarning"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_RetainEarning));
            retainEarningReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_RetainEarning_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_RetainEarning));
            retainEarningReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_RetainEarning_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_RetainEarning));
            retainEarningReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_RetainEarning_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_RetainEarning));
            retainEarningReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_RetainEarning_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_RetainEarning));
            retainEarningReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_RetainEarning_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_RetainEarning));

            var balanceSheetReportPage = accountingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_BalanceSheet, L("BalanceSheet"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_BalanceSheet));
            balanceSheetReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_BalanceSheet_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_BalanceSheet));
            balanceSheetReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_BalanceSheet_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_BalanceSheet));
            balanceSheetReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_BalanceSheet_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_BalanceSheet));
            balanceSheetReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_BalanceSheet_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_BalanceSheet));
            balanceSheetReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_BalanceSheet_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_BalanceSheet));

            var trialBalanceReportPage = accountingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_TrialBalance, L("TrialBalance"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_TrialBalance));
            trialBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_TrialBalance_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_TrialBalance));
            trialBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_TrialBalance_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_TrialBalance));
            trialBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_TrialBalance_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_TrialBalance));
            trialBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_TrialBalance_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_TrialBalance));
            trialBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_TrialBalance_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_TrialBalance));

            var cashFlowReportPage = accountingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlow, L("CashFlow"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlow));
            cashFlowReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlow_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlow));
            cashFlowReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlow_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlow));
            cashFlowReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlow_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlow));
            cashFlowReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlow_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlow));
            cashFlowReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlow_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlow));

            var cashFlowDetailReportPage = accountingReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlowDetail, L("CashFlowDetail"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlowDetail));
            cashFlowDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlowDetail_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlowDetail));
            cashFlowDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlowDetail_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlowDetail));
            cashFlowDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlowDetail_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlowDetail));
            cashFlowDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlowDetail_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlowDetail));
            cashFlowDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Accounting_CashFlowDetail_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Accounting_CashFlowDetail));

            #endregion

            #region Loans Reports
            var loanReportPage = reportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans, L("Loans"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans));

            var loanBalanceReportPage = loanReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Balances, L("Balances"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Balance));
            loanBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Balances_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Balance));
            loanBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Balances_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Balance));
            loanBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Balances_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Balance));
            loanBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Balances_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Balance));
            loanBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Balances_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Balance));

            var loanCollectionReportPage = loanReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collections, L("Collections"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collections));
            loanCollectionReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collections_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collections));
            loanCollectionReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collections_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collections));
            loanCollectionReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collections_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collections));
            loanCollectionReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collections_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collections));
            loanCollectionReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collections_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collections));

            var loanCollateralReportPage = loanReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collaterals, L("Collaterals"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collaterals));
            loanCollateralReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collaterals_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collaterals));
            loanCollateralReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collaterals_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collaterals));
            loanCollateralReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collaterals_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collaterals));
            loanCollateralReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collaterals_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collaterals));
            loanCollateralReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Loans_Collaterals_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Loans_Collaterals));

            #endregion

            #region Inventories Reports
            var inventoryReportPage = reportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories, L("InventoryReports"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories));

            var stockBalanceReportPage = inventoryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_StockBalance, L("StockBalance"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_StockBalance));
            stockBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_StockBalance_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_StockBalance));
            stockBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_StockBalance_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_StockBalance));
            stockBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_StockBalance_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_StockBalance));
            stockBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_StockBalance_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_StockBalance));
            stockBalanceReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_StockBalance_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_StockBalance));

            var inventoryTransactionReportPage = inventoryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_Transactions, L("Transactions"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_Transactions));
            inventoryTransactionReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_Transactions_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_Transactions));
            inventoryTransactionReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_Transactions_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_Transactions));
            inventoryTransactionReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_Transactions_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_Transactions));
            inventoryTransactionReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_Transactions_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_Transactions));
            inventoryTransactionReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_Transactions_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_Transactions));

            var inventoryTransactionDetailReportPage = inventoryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_TransactionDetail, L("TransactionDetail"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_TransactionDetail));
            inventoryTransactionDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_TransactionDetail_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_TransactionDetail));
            inventoryTransactionDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_TransactionDetail_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_TransactionDetail));
            inventoryTransactionDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_TransactionDetail_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_TransactionDetail));
            inventoryTransactionDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_TransactionDetail_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_TransactionDetail));
            inventoryTransactionDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_TransactionDetail_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_TransactionDetail));

            var inventoryCustomerBorrowReportPage = inventoryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CustomerBorrows, L("CustomerBorrow"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CustomerBorrows));
            inventoryCustomerBorrowReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CustomerBorrows_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CustomerBorrows));
            inventoryCustomerBorrowReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CustomerBorrows_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CustomerBorrows));
            inventoryCustomerBorrowReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CustomerBorrows_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CustomerBorrows));
            inventoryCustomerBorrowReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CustomerBorrows_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CustomerBorrows));
            inventoryCustomerBorrowReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CustomerBorrows_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CustomerBorrows));

            var inventoryCostSummaryReportPage = inventoryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostSummary, L("CostSummary"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostSummary));
            inventoryCostSummaryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostSummary_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostSummary));
            inventoryCostSummaryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostSummary_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostSummary));
            inventoryCostSummaryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostSummary_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostSummary));
            inventoryCostSummaryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostSummary_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostSummary));
            inventoryCostSummaryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostSummary_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostSummary));

            var inventoryCostDetailReportPage = inventoryReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostDetail, L("CostDetail"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostDetail));
            inventoryCostDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostDetail_View, L("View"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostDetail));
            inventoryCostDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostDetail_ExportExcel, L("ExportExcel"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostDetail));
            inventoryCostDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostDetail_Print, L("Print"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostDetail));
            inventoryCostDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostDetail_CreateTemplate, L("CreateTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostDetail));
            inventoryCostDetailReportPage.CreateChildPermission(PermissionNames.Pages_Reports_Inventories_CostDetail_ModifyTemplate, L("ModifyTemplate"), multiTenancySides: MultiTenancySides.Tenant, featureDependency: new SimpleFeatureDependency(AppFeatures.Reports_Inventories_CostDetail));

            #endregion

            #endregion

            #endregion

            #region Administrations
            var administrationPage = page.CreateChildPermission(PermissionNames.Pages_Administrations, L("Administrations"));

            var userPage = administrationPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users, L("Users"));
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_Create, L("Create"));
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_View, L("View"));
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_Edit, L("Edit"));
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_Delete, L("Delete"));
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_Enable, L("Enable"));
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_Disable, L("Disable"));
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_Activate, L("Activate"), multiTenancySides: MultiTenancySides.Tenant);
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_Deactivate, L("Deactivate"));
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_ResetPassword, L("ResetPassword"));
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_ChangePermissions, L("ChangePermissions"));
            userPage.CreateChildPermission(PermissionNames.Pages_Administrations_Users_Impersonation, L("LoginAsUser"));

            var rolePage = administrationPage.CreateChildPermission(PermissionNames.Pages_Administrations_Roles, L("Roles"));
            rolePage.CreateChildPermission(PermissionNames.Pages_Administrations_Roles_Create, L("Create"));
            rolePage.CreateChildPermission(PermissionNames.Pages_Administrations_Roles_View, L("View"));
            rolePage.CreateChildPermission(PermissionNames.Pages_Administrations_Roles_Edit, L("Edit"));
            rolePage.CreateChildPermission(PermissionNames.Pages_Administrations_Roles_Delete, L("Delete"));
            rolePage.CreateChildPermission(PermissionNames.Pages_Administrations_Roles_Enable, L("Enable"));
            rolePage.CreateChildPermission(PermissionNames.Pages_Administrations_Roles_Disable, L("Disable"));

            var auditLogPage = administrationPage.CreateChildPermission(PermissionNames.Pages_Administrations_AuditLogs, L("AuditLogs"));
            auditLogPage.CreateChildPermission(PermissionNames.Pages_Administrations_AuditLogs_View, L("View"));
            auditLogPage.CreateChildPermission(PermissionNames.Pages_Administrations_AuditLogs_ExportExcel, L("ExportExcel"));


            var organizationUnits = administrationPage.CreateChildPermission(PermissionNames.Pages_Administrations_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(PermissionNames.Pages_Administrations_OrganizationUnits_Create, L("Create"));
            organizationUnits.CreateChildPermission(PermissionNames.Pages_Administrations_OrganizationUnits_View, L("View"));
            organizationUnits.CreateChildPermission(PermissionNames.Pages_Administrations_OrganizationUnits_Edit, L("Edit"));
            organizationUnits.CreateChildPermission(PermissionNames.Pages_Administrations_OrganizationUnits_Delete, L("Delete"));
            organizationUnits.CreateChildPermission(PermissionNames.Pages_Administrations_OrganizationUnits_Enable, L("Enable"));
            organizationUnits.CreateChildPermission(PermissionNames.Pages_Administrations_OrganizationUnits_Disable, L("Disable"));
            organizationUnits.CreateChildPermission(PermissionNames.Pages_Administrations_OrganizationUnits_ManageOrganizationTree, L("ManageOrganizationTree"));
            organizationUnits.CreateChildPermission(PermissionNames.Pages_Administrations_OrganizationUnits_ManageMembers, L("ManageMembers"));

            #endregion

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BiiSoftConsts.LocalizationSourceName);
        }
    }
}
