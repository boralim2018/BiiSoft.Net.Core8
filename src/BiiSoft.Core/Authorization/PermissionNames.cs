namespace BiiSoft.Authorization
{
    public static class PermissionNames
    {

        public const string Pages = "Pages";

        public const string Pages_Profile = "Pages.Profile";
        public const string Pages_Dashboard = "Pages.Dashboard";
        public const string Pages_Hangfire_Dashboard = "Pages.Hangfire.Dashboard";

        public const string Pages_Find = "Pages.Find";
        public const string Pages_Find_Users = "Pages.Find.Users";
        public const string Pages_Find_Editions = "Pages.Find.Editions";
        public const string Pages_Find_Branches = "Pages.Find.Branches";
        public const string Pages_Find_Locations = "Pages.Find.Locations";
        public const string Pages_Find_Countries = "Pages.Find.Countries";
        public const string Pages_Find_CityProvinces = "Pages.Find.CityProvinces";
        public const string Pages_Find_KhanDistricts = "Pages.Find.KhanDistricts";
        public const string Pages_Find_SangkatCommunes = "Pages.Find.SangkatCommunes";
        public const string Pages_Find_Villages = "Pages.Find.Villages";
        public const string Pages_Find_Currencies = "Pages.Find.Currencies";

        #region Tenants
        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Enable = "Pages.Tenants.Enable";
        public const string Pages_Tenants_Disable = "Pages.Tenants.Disable";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";
        #endregion

        #region Editions
        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";
        #endregion

        #region Languages
        public const string Pages_Languages = "Pages.Languages";
        public const string Pages_Languages_Create = "Pages.Languages.Create";
        public const string Pages_Languages_Edit = "Pages.Languages.Edit";
        public const string Pages_Languages_Delete = "Pages.Languages.Delete";
        public const string Pages_Languages_ChangeTexts = "Pages.Languages.ChangeTexts";
        public const string Pages_Languages_Enable = "Pages.Languages.Enable";
        public const string Pages_Languages_Disable = "Pages.Languages.Disable";
        #endregion

        #region Settings
        public const string Pages_Settings_Host = "Pages.Settings.Host";
        public const string Pages_Settings_Tenant = "Pages.Settings.Tenant";
        #endregion

        #region Setup
        public const string Pages_Setup = "Pages.Setup";

        #region Currencies
        public const string Pages_Setup_Currencies = "Pages.Setup.Currencies";
        public const string Pages_Setup_Currencies_Create = "Pages.Setup.Currencies.Create";
        public const string Pages_Setup_Currencies_View = "Pages.Setup.Currencies.View";
        public const string Pages_Setup_Currencies_Edit = "Pages.Setup.Currencies.Edit";
        public const string Pages_Setup_Currencies_Delete = "Pages.Setup.Currencies.Delete";
        public const string Pages_Setup_Currencies_ImportExcel = "Pages.Setup.Currencies.ImportExcel";
        public const string Pages_Setup_Currencies_ExportExcel = "Pages.Setup.Currencies.ExportExcel";
        public const string Pages_Setup_Currencies_Enable = "Pages.Setup.Currencies.Enable";
        public const string Pages_Setup_Currencies_Disable = "Pages.Setup.Currencies.Disable";
        public const string Pages_Setup_Currencies_SetAsDefault = "Pages.Setup.Currencies.SetAsDefault";

        public const string Pages_Setup_ExchangeRates = "Pages.Setup.ExchangeRates";
        public const string Pages_Setup_ExchangeRates_Create = "Pages.Setup.ExchangeRates.Create";
        public const string Pages_Setup_ExchangeRates_View = "Pages.Setup.ExchangeRates.View";
        public const string Pages_Setup_ExchangeRates_Edit = "Pages.Setup.ExchangeRates.Edit";
        public const string Pages_Setup_ExchangeRates_Delete = "Pages.Setup.ExchangeRates.Delete";
        public const string Pages_Setup_ExchangeRates_ExportExcel = "Pages.Setup.ExchangeRates.ExportExcel";
        #endregion 

        #region Locations
        public const string Pages_Setup_Locations = "Pages.Setup.Locations";

        public const string Pages_Setup_Locations_List = "Pages.Setup.Locations_List";
        public const string Pages_Setup_Locations_Create = "Pages.Setup.Locations.Create";
        public const string Pages_Setup_Locations_View = "Pages.Setup.Locations.View";
        public const string Pages_Setup_Locations_Edit = "Pages.Setup.Locations.Edit";
        public const string Pages_Setup_Locations_Delete = "Pages.Setup.Locations.Delete";
        public const string Pages_Setup_Locations_ImportExcel = "Pages.Setup.Locations.ImportExcel";
        public const string Pages_Setup_Locations_ExportExcel = "Pages.Setup.Locations.ExportExcel";
        public const string Pages_Setup_Locations_Enable = "Pages.Setup.Locations.Enable";
        public const string Pages_Setup_Locations_Disable = "Pages.Setup.Locations.Disable";

        public const string Pages_Setup_Locations_Countries = "Pages.Setup.Locations.Countries";
        public const string Pages_Setup_Locations_Countries_Create = "Pages.Setup.Locations.Countries.Create";
        public const string Pages_Setup_Locations_Countries_View = "Pages.Setup.Locations.Countries.View";
        public const string Pages_Setup_Locations_Countries_Edit = "Pages.Setup.Locations.Countries.Edit";
        public const string Pages_Setup_Locations_Countries_Delete = "Pages.Setup.Locations.Countries.Delete";
        public const string Pages_Setup_Locations_Countries_ImportExcel = "Pages.Setup.Locations.Countries.ImportExcel";
        public const string Pages_Setup_Locations_Countries_ExportExcel = "Pages.Setup.Locations.Countries.ExportExcel";
        public const string Pages_Setup_Locations_Countries_Enable = "Pages.Setup.Locations.Countries.Enable";
        public const string Pages_Setup_Locations_Countries_Disable = "Pages.Setup.Locations.Countries.Disable";

        public const string Pages_Setup_Locations_CityProvinces = "Pages.Setup.Locations.CityProvinces";
        public const string Pages_Setup_Locations_CityProvinces_Create = "Pages.Setup.Locations.CityProvinces.Create";
        public const string Pages_Setup_Locations_CityProvinces_View = "Pages.Setup.Locations.CityProvinces.View";
        public const string Pages_Setup_Locations_CityProvinces_Edit = "Pages.Setup.Locations.CityProvinces.Edit";
        public const string Pages_Setup_Locations_CityProvinces_Delete = "Pages.Setup.Locations.CityProvinces.Delete";
        public const string Pages_Setup_Locations_CityProvinces_ImportExcel = "Pages.Setup.Locations.CityProvinces.ImportExcel";
        public const string Pages_Setup_Locations_CityProvinces_ExportExcel = "Pages.Setup.Locations.CityProvinces.ExportExcel";
        public const string Pages_Setup_Locations_CityProvinces_Enable = "Pages.Setup.Locations.CityProvinces.Enable";
        public const string Pages_Setup_Locations_CityProvinces_Disable = "Pages.Setup.Locations.CityProvinces.Disable";

        public const string Pages_Setup_Locations_KhanDistricts = "Pages.Setup.Locations.KhanDistricts";
        public const string Pages_Setup_Locations_KhanDistricts_Create = "Pages.Setup.Locations.KhanDistricts.Create";
        public const string Pages_Setup_Locations_KhanDistricts_View = "Pages.Setup.Locations.KhanDistricts.View";
        public const string Pages_Setup_Locations_KhanDistricts_Edit = "Pages.Setup.Locations.KhanDistricts.Edit";
        public const string Pages_Setup_Locations_KhanDistricts_Delete = "Pages.Setup.Locations.KhanDistricts.Delete";
        public const string Pages_Setup_Locations_KhanDistricts_ImportExcel = "Pages.Setup.Locations.KhanDistricts.ImportExcel";
        public const string Pages_Setup_Locations_KhanDistricts_ExportExcel = "Pages.Setup.Locations.KhanDistricts.ExportExcel";
        public const string Pages_Setup_Locations_KhanDistricts_Enable = "Pages.Setup.Locations.KhanDistricts.Enable";
        public const string Pages_Setup_Locations_KhanDistricts_Disable = "Pages.Setup.Locations.KhanDistricts.Disable";

        public const string Pages_Setup_Locations_SangkatCommunes = "Pages.Setup.Locations.SangkatCommunes";
        public const string Pages_Setup_Locations_SangkatCommunes_Create = "Pages.Setup.Locations.SangkatCommunes.Create";
        public const string Pages_Setup_Locations_SangkatCommunes_View = "Pages.Setup.Locations.SangkatCommunes.View";
        public const string Pages_Setup_Locations_SangkatCommunes_Edit = "Pages.Setup.Locations.SangkatCommunes.Edit";
        public const string Pages_Setup_Locations_SangkatCommunes_Delete = "Pages.Setup.Locations.SangkatCommunes.Delete";
        public const string Pages_Setup_Locations_SangkatCommunes_ImportExcel = "Pages.Setup.Locations.SangkatCommunes.ImportExcel";
        public const string Pages_Setup_Locations_SangkatCommunes_ExportExcel = "Pages.Setup.Locations.SangkatCommunes.ExportExcel";
        public const string Pages_Setup_Locations_SangkatCommunes_Enable = "Pages.Setup.Locations.SangkatCommunes.Enable";
        public const string Pages_Setup_Locations_SangkatCommunes_Disable = "Pages.Setup.Locations.SangkatCommunes.Disable";

        public const string Pages_Setup_Locations_Villages = "Pages.Setup.Locations.Villages";
        public const string Pages_Setup_Locations_Villages_Create = "Pages.Setup.Locations.Villages.Create";
        public const string Pages_Setup_Locations_Villages_View = "Pages.Setup.Locations.Villages.View";
        public const string Pages_Setup_Locations_Villages_Edit = "Pages.Setup.Locations.Villages.Edit";
        public const string Pages_Setup_Locations_Villages_Delete = "Pages.Setup.Locations.Villages.Delete";
        public const string Pages_Setup_Locations_Villages_ImportExcel = "Pages.Setup.Locations.Villages.ImportExcel";
        public const string Pages_Setup_Locations_Villages_ExportExcel = "Pages.Setup.Locations.Villages.ExportExcel";
        public const string Pages_Setup_Locations_Villages_Enable = "Pages.Setup.Locations.Villages.Enable";
        public const string Pages_Setup_Locations_Villages_Disable = "Pages.Setup.Locations.Villages.Disable";

        #endregion

        #region Items
        public const string Pages_Setup_Items = "Pages.Setup.Items";

        public const string Pages_Setup_Items_ItemGroups = "Pages.Setup.Items.ItemGroups";
        public const string Pages_Setup_Items_ItemGroups_Create = "Pages.Setup.Items.ItemGroups.Create";
        public const string Pages_Setup_Items_ItemGroups_View = "Pages.Setup.Items.ItemGroups.View";
        public const string Pages_Setup_Items_ItemGroups_Edit = "Pages.Setup.Items.ItemGroups.Edit";
        public const string Pages_Setup_Items_ItemGroups_EditAccount = "Pages.Setup.Items.ItemGroups.EditAccount";
        public const string Pages_Setup_Items_ItemGroups_SeeAccount = "Pages.Setup.Items.ItemGroups.SeeAccount";
        public const string Pages_Setup_Items_ItemGroups_Delete = "Pages.Setup.Items.ItemGroups.Delete";
        public const string Pages_Setup_Items_ItemGroups_ImportExcel = "Pages.Setup.Items.ItemGroups.ImportExcel";
        public const string Pages_Setup_Items_ItemGroups_ExportExcel = "Pages.Setup.Items.ItemGroups.ExportExcel";
        public const string Pages_Setup_Items_ItemGroups_Enable = "Pages.Setup.Items.ItemGroups.Enable";
        public const string Pages_Setup_Items_ItemGroups_Disable = "Pages.Setup.Items.ItemGroups.Disable";
       
        public const string Pages_Setup_Items_List = "Pages.Setup.Items.List";
        public const string Pages_Setup_Items_List_Create = "Pages.Setup.Items.List.Create";
        public const string Pages_Setup_Items_List_View = "Pages.Setup.Items.List.View";
        public const string Pages_Setup_Items_List_Edit = "Pages.Setup.Items.List.Edit";
        public const string Pages_Setup_Items_List_EditAccount = "Pages.Setup.Items.List.EditAccount";
        public const string Pages_Setup_Items_List_SeeAccount = "Pages.Setup.Items.List.SeeAccount";
        public const string Pages_Setup_Items_List_Delete = "Pages.Setup.Items.List.Delete";
        public const string Pages_Setup_Items_List_ImportExcel = "Pages.Setup.Items.List.ImportExcel";
        public const string Pages_Setup_Items_List_ExportExcel = "Pages.Setup.Items.List.ExportExcel";
        public const string Pages_Setup_Items_List_Enable = "Pages.Setup.Items.List.Enable";
        public const string Pages_Setup_Items_List_Disable = "Pages.Setup.Items.List.Disable";
       

        public const string Pages_Setup_Items_Units = "Pages.Setup.Units";
        public const string Pages_Setup_Items_Units_Create = "Pages.Setup.Items.Units.Create";
        public const string Pages_Setup_Items_Units_View = "Pages.Setup.Items.Units.View";
        public const string Pages_Setup_Items_Units_Edit = "Pages.Setup.Items.Units.Edit";
        public const string Pages_Setup_Items_Units_Delete = "Pages.Setup.Items.Units.Delete";
        public const string Pages_Setup_Items_Units_ImportExcel = "Pages.Setup.Items.Units.ImportExcel";
        public const string Pages_Setup_Items_Units_ExportExcel = "Pages.Setup.Items.Units.ExportExcel";
        public const string Pages_Setup_Items_Units_Enable = "Pages.Setup.Items.Units.Enable";
        public const string Pages_Setup_Items_Units_Disable = "Pages.Setup.Items.Units.Disable";

        public const string Pages_Setup_Items_WeightUnits = "Pages.Setup.WeightUnits";
        public const string Pages_Setup_Items_WeightUnits_Create = "Pages.Setup.Items.WeightUnits.Create";
        public const string Pages_Setup_Items_WeightUnits_View = "Pages.Setup.Items.WeightUnits.View";
        public const string Pages_Setup_Items_WeightUnits_Edit = "Pages.Setup.Items.WeightUnits.Edit";
        public const string Pages_Setup_Items_WeightUnits_Delete = "Pages.Setup.Items.WeightUnits.Delete";
        public const string Pages_Setup_Items_WeightUnits_ImportExcel = "Pages.Setup.Items.WeightUnits.ImportExcel";
        public const string Pages_Setup_Items_WeightUnits_ExportExcel = "Pages.Setup.Items.WeightUnits.ExportExcel";
        public const string Pages_Setup_Items_WeightUnits_Enable = "Pages.Setup.Items.WeightUnits.Enable";
        public const string Pages_Setup_Items_WeightUnits_Disable = "Pages.Setup.Items.WeightUnits.Disable";

        public const string Pages_Setup_Items_Grades = "Pages.Setup.Items.Grades";
        public const string Pages_Setup_Items_Grades_Create = "Pages.Setup.Items.Grades.Create";
        public const string Pages_Setup_Items_Grades_View = "Pages.Setup.Items.Grades.View";
        public const string Pages_Setup_Items_Grades_Edit = "Pages.Setup.Items.Grades.Edit";
        public const string Pages_Setup_Items_Grades_Delete = "Pages.Setup.Items.Grades.Delete";
        public const string Pages_Setup_Items_Grades_ImportExcel = "Pages.Setup.Items.Grades.ImportExcel";
        public const string Pages_Setup_Items_Grades_ExportExcel = "Pages.Setup.Items.Grades.ExportExcel";
        public const string Pages_Setup_Items_Grades_Enable = "Pages.Setup.Items.Grades.Enable";
        public const string Pages_Setup_Items_Grades_Disable = "Pages.Setup.Items.Grades.Disable";

        public const string Pages_Setup_Items_Sizes = "Pages.Setup.Items.Sizes";
        public const string Pages_Setup_Items_Sizes_Create = "Pages.Setup.Items.Sizes.Create";
        public const string Pages_Setup_Items_Sizes_View = "Pages.Setup.Items.Sizes.View";
        public const string Pages_Setup_Items_Sizes_Edit = "Pages.Setup.Items.Sizes.Edit";
        public const string Pages_Setup_Items_Sizes_Delete = "Pages.Setup.Items.Sizes.Delete";
        public const string Pages_Setup_Items_Sizes_ImportExcel = "Pages.Setup.Items.Sizes.ImportExcel";
        public const string Pages_Setup_Items_Sizes_ExportExcel = "Pages.Setup.Items.Sizes.ExportExcel";
        public const string Pages_Setup_Items_Sizes_Enable = "Pages.Setup.Items.Sizes.Enable";
        public const string Pages_Setup_Items_Sizes_Disable = "Pages.Setup.Items.Sizes.Disable";

        public const string Pages_Setup_Items_ColorPatterns = "Pages.Setup.Items.ColorPatterns";
        public const string Pages_Setup_Items_ColorPatterns_Create = "Pages.Setup.Items.ColorPatterns.Create";
        public const string Pages_Setup_Items_ColorPatterns_View = "Pages.Setup.Items.ColorPatterns.View";
        public const string Pages_Setup_Items_ColorPatterns_Edit = "Pages.Setup.Items.ColorPatterns.Edit";
        public const string Pages_Setup_Items_ColorPatterns_Delete = "Pages.Setup.Items.ColorPatterns.Delete";
        public const string Pages_Setup_Items_ColorPatterns_ImportExcel = "Pages.Setup.Items.ColorPatterns.ImportExcel";
        public const string Pages_Setup_Items_ColorPatterns_ExportExcel = "Pages.Setup.Items.ColorPatterns.ExportExcel";
        public const string Pages_Setup_Items_ColorPatterns_Enable = "Pages.Setup.Items.ColorPatterns.Enable";
        public const string Pages_Setup_Items_ColorPatterns_Disable = "Pages.Setup.Items.ColorPatterns.Disable";

        public const string Pages_Setup_Items_Brands = "Pages.Setup.Items.Brands";
        public const string Pages_Setup_Items_Brands_Create = "Pages.Setup.Items.Brands.Create";
        public const string Pages_Setup_Items_Brands_View = "Pages.Setup.Items.Brands.View";
        public const string Pages_Setup_Items_Brands_Edit = "Pages.Setup.Items.Brands.Edit";
        public const string Pages_Setup_Items_Brands_Delete = "Pages.Setup.Items.Brands.Delete";
        public const string Pages_Setup_Items_Brands_ImportExcel = "Pages.Setup.Items.Brands.ImportExcel";
        public const string Pages_Setup_Items_Brands_ExportExcel = "Pages.Setup.Items.Brands.ExportExcel";
        public const string Pages_Setup_Items_Brands_Enable = "Pages.Setup.Items.Brands.Enable";
        public const string Pages_Setup_Items_Brands_Disable = "Pages.Setup.Items.Brands.Disable";

        public const string Pages_Setup_Items_FieldAs = "Pages.Setup.Items.FieldAs";
        public const string Pages_Setup_Items_FieldAs_Create = "Pages.Setup.Items.FieldAs.Create";
        public const string Pages_Setup_Items_FieldAs_View = "Pages.Setup.Items.FieldAs.View";
        public const string Pages_Setup_Items_FieldAs_Edit = "Pages.Setup.Items.FieldAs.Edit";
        public const string Pages_Setup_Items_FieldAs_Delete = "Pages.Setup.Items.FieldAs.Delete";
        public const string Pages_Setup_Items_FieldAs_ImportExcel = "Pages.Setup.Items.FieldAs.ImportExcel";
        public const string Pages_Setup_Items_FieldAs_ExportExcel = "Pages.Setup.Items.FieldAs.ExportExcel";
        public const string Pages_Setup_Items_FieldAs_Enable = "Pages.Setup.Items.FieldAs.Enable";
        public const string Pages_Setup_Items_FieldAs_Disable = "Pages.Setup.Items.FieldAs.Disable";

        public const string Pages_Setup_Items_FieldBs = "Pages.Setup.Items.FieldBs";
        public const string Pages_Setup_Items_FieldBs_Create = "Pages.Setup.Items.FieldBs.Create";
        public const string Pages_Setup_Items_FieldBs_View = "Pages.Setup.Items.FieldBs.View";
        public const string Pages_Setup_Items_FieldBs_Edit = "Pages.Setup.Items.FieldBs.Edit";
        public const string Pages_Setup_Items_FieldBs_Delete = "Pages.Setup.Items.FieldBs.Delete";
        public const string Pages_Setup_Items_FieldBs_ImportExcel = "Pages.Setup.Items.FieldBs.ImportExcel";
        public const string Pages_Setup_Items_FieldBs_ExportExcel = "Pages.Setup.Items.FieldBs.ExportExcel";
        public const string Pages_Setup_Items_FieldBs_Enable = "Pages.Setup.Items.FieldBs.Enable";
        public const string Pages_Setup_Items_FieldBs_Disable = "Pages.Setup.Items.FieldBs.Disable";

        public const string Pages_Setup_Items_FieldCs = "Pages.Setup.Items.FieldCs";
        public const string Pages_Setup_Items_FieldCs_Create = "Pages.Setup.Items.FieldCs.Create";
        public const string Pages_Setup_Items_FieldCs_View = "Pages.Setup.Items.FieldCs.View";
        public const string Pages_Setup_Items_FieldCs_Edit = "Pages.Setup.Items.FieldCs.Edit";
        public const string Pages_Setup_Items_FieldCs_Delete = "Pages.Setup.Items.FieldCs.Delete";
        public const string Pages_Setup_Items_FieldCs_ImportExcel = "Pages.Setup.Items.FieldCs.ImportExcel";
        public const string Pages_Setup_Items_FieldCs_ExportExcel = "Pages.Setup.Items.FieldCs.ExportExcel";
        public const string Pages_Setup_Items_FieldCs_Enable = "Pages.Setup.Items.FieldCs.Enable";
        public const string Pages_Setup_Items_FieldCs_Disable = "Pages.Setup.Items.FieldCs.Disable";

        public const string Pages_Setup_Items_PriceLevels = "Pages.Setup.Items.PriceLevels";
        public const string Pages_Setup_Items_PriceLevels_Create = "Pages.Setup.Items.PriceLevels.Create";
        public const string Pages_Setup_Items_PriceLevels_View = "Pages.Setup.Items.PriceLevels.View";
        public const string Pages_Setup_Items_PriceLevels_Edit = "Pages.Setup.Items.PriceLevels.Edit";
        public const string Pages_Setup_Items_PriceLevels_Delete = "Pages.Setup.Items.PriceLevels.Delete";
        public const string Pages_Setup_Items_PriceLevels_ImportExcel = "Pages.Setup.Items.PriceLevels.ImportExcel";
        public const string Pages_Setup_Items_PriceLevels_ExportExcel = "Pages.Setup.Items.PriceLevels.ExportExcel";
        public const string Pages_Setup_Items_PriceLevels_Enable = "Pages.Setup.Items.PriceLevels.Enable";
        public const string Pages_Setup_Items_PriceLevels_Disable = "Pages.Setup.Items.PriceLevels.Disable";

        public const string Pages_Setup_Items_Promotions = "Pages.Setup.Items.Promotions";
        public const string Pages_Setup_Items_Promotions_Create = "Pages.Setup.Items.Promotions.Create";
        public const string Pages_Setup_Items_Promotions_View = "Pages.Setup.Items.Promotions.View";
        public const string Pages_Setup_Items_Promotions_Edit = "Pages.Setup.Items.Promotions.Edit";
        public const string Pages_Setup_Items_Promotions_Delete = "Pages.Setup.Items.Promotions.Delete";
        public const string Pages_Setup_Items_Promotions_ImportExcel = "Pages.Setup.Items.Promotions.ImportExcel";
        public const string Pages_Setup_Items_Promotions_ExportExcel = "Pages.Setup.Items.Promotions.ExportExcel";
        public const string Pages_Setup_Items_Promotions_Enable = "Pages.Setup.Items.Promotions.Enable";
        public const string Pages_Setup_Items_Promotions_Disable = "Pages.Setup.Items.Promotions.Disable";

        public const string Pages_Setup_Items_Scores = "Pages.Setup.Items.Scores";
        public const string Pages_Setup_Items_Scores_Create = "Pages.Setup.Items.Scores.Create";
        public const string Pages_Setup_Items_Scores_View = "Pages.Setup.Items.Scores.View";
        public const string Pages_Setup_Items_Scores_Edit = "Pages.Setup.Items.Scores.Edit";
        public const string Pages_Setup_Items_Scores_Delete = "Pages.Setup.Items.Scores.Delete";
        public const string Pages_Setup_Items_Scores_ImportExcel = "Pages.Setup.Items.Scores.ImportExcel";
        public const string Pages_Setup_Items_Scores_ExportExcel = "Pages.Setup.Items.Scores.ExportExcel";
        public const string Pages_Setup_Items_Scores_Enable = "Pages.Setup.Items.Scores.Enable";
        public const string Pages_Setup_Items_Scores_Disable = "Pages.Setup.Items.Scores.Disable";

        #endregion

        #region Payment Methods
        public const string Pages_Setup_PaymentMethods = "Pages.Setup.PaymentMethods";
        public const string Pages_Setup_PaymentMethods_View = "Pages.Setup.PaymentMethods.View";
        public const string Pages_Setup_PaymentMethods_EditAccount = "Pages.Setup.PaymentMethods.EditAccount";
        public const string Pages_Setup_PaymentMethods_SeeAccount = "Pages.Setup.PaymentMethods.SeeAccount";
        public const string Pages_Setup_PaymentMethods_ExportExcel = "Pages.Setup.PaymentMethods.ExportExcel";
        public const string Pages_Setup_PaymentMethods_Enable = "Pages.Setup.PaymentMethods.Enable";
        public const string Pages_Setup_PaymentMethods_Disable = "Pages.Setup.PaymentMethods.Disable";
        #endregion

        #region Classes
        public const string Pages_Setup_Classes = "Pages.Setup.Classes";
        public const string Pages_Setup_Classes_Create = "Pages.Setup.Classes.Create";
        public const string Pages_Setup_Classes_View = "Pages.Setup.Classes.View";
        public const string Pages_Setup_Classes_Edit = "Pages.Setup.Classes.Edit";
        public const string Pages_Setup_Classes_Delete = "Pages.Setup.Classes.Delete";
        public const string Pages_Setup_Classes_ImportExcel = "Pages.Setup.Classes.ImportExcel";
        public const string Pages_Setup_Classes_ExportExcel = "Pages.Setup.Classes.ExportExcel";
        public const string Pages_Setup_Classes_Enable = "Pages.Setup.Classes.Enable";
        public const string Pages_Setup_Classes_Disable = "Pages.Setup.Classes.Disable";
        #endregion

        #region Warehoues
        public const string Pages_Setup_Warehouses = "Pages.Setup.Warehouses";
        public const string Pages_Setup_Warehouses_Create = "Pages.Setup.Warehouses.Create";
        public const string Pages_Setup_Warehouses_View = "Pages.Setup.Warehouses.View";
        public const string Pages_Setup_Warehouses_Edit = "Pages.Setup.Warehouses.Edit";
        public const string Pages_Setup_Warehouses_Delete = "Pages.Setup.Warehouses.Delete";
        public const string Pages_Setup_Warehouses_ImportExcel = "Pages.Setup.Warehouses.ImportExcel";
        public const string Pages_Setup_Warehouses_ExportExcel = "Pages.Setup.Warehouses.ExportExcel";
        public const string Pages_Setup_Warehouses_Enable = "Pages.Setup.Warehouses.Enable";
        public const string Pages_Setup_Warehouses_Disable = "Pages.Setup.Warehouses.Disable";

        public const string Pages_Setup_Warehouses_Slots = "Pages.Setup.Warehouses.Slots";
        public const string Pages_Setup_Warehouses_Slots_Create = "Pages.Setup.Warehouses.Slots.Create";
        public const string Pages_Setup_Warehouses_Slots_View = "Pages.Setup.Warehouses.Slots.View";
        public const string Pages_Setup_Warehouses_Slots_Edit = "Pages.Setup.Warehouses.Slots.Edit";
        public const string Pages_Setup_Warehouses_Slots_Delete = "Pages.Setup.Warehouses.Slots.Delete";
        public const string Pages_Setup_Warehouses_Slots_ImportExcel = "Pages.Setup.Warehouses.Slots.ImportExcel";
        public const string Pages_Setup_Warehouses_Slots_ExportExcel = "Pages.Setup.Warehouses.Slots.ExportExcel";
        public const string Pages_Setup_Warehouses_Slots_Enable = "Pages.Setup.Warehouses.Slots.Enable";
        public const string Pages_Setup_Warehouses_Slots_Disable = "Pages.Setup.Warehouses.Slots.Disable";
        #endregion

        #region Form Templates
        public const string Pages_Setup_FormTemplates = "Pages.Setup.FormTemplates";
        public const string Pages_Setup_FormTemplates_Create = "Pages.Setup.FormTemplates.Create";
        public const string Pages_Setup_FormTemplates_View = "Pages.Setup.FormTemplates.View";
        public const string Pages_Setup_FormTemplates_Edit = "Pages.Setup.FormTemplates.Edit";
        public const string Pages_Setup_FormTemplates_Delete = "Pages.Setup.FormTemplates.Delete";
        public const string Pages_Setup_FormTemplates_Enable = "Pages.Setup.FormTemplates.Enable";
        public const string Pages_Setup_FormTemplates_Disable = "Pages.Setup.FormTemplates.Disable";
        #endregion

        #region Taxes
        public const string Pages_Setup_Taxes = "Pages.Setup.Taxes";
        public const string Pages_Setup_Taxes_Create = "Pages.Setup.Taxes.Create";
        public const string Pages_Setup_Taxes_View = "Pages.Setup.Taxes.View";
        public const string Pages_Setup_Taxes_Edit = "Pages.Setup.Taxes.Edit";
        public const string Pages_Setup_Taxes_Delete = "Pages.Setup.Taxes.Delete";
        public const string Pages_Setup_Taxes_ImportExcel = "Pages.Setup.Taxes.ImportExcel";
        public const string Pages_Setup_Taxes_ExportExcel = "Pages.Setup.Taxes.ExportExcel";
        public const string Pages_Setup_Taxes_Enable = "Pages.Setup.Taxes.Enable";
        public const string Pages_Setup_Taxes_Disable = "Pages.Setup.Taxes.Disable";
        #endregion

        #endregion

        #region Company
        public const string Pages_Company = "Pages.Company";

        public const string Pages_Company_CompanySetting = "Pages.Company.CompanySetting";
        public const string Pages_Company_CompanySetting_Edit = "Pages.Company.CompanySetting.Edit";

        public const string Pages_Company_Branches = "Pages.Company.Branches";
        public const string Pages_Company_Branches_Create = "Pages.Company.Branches.Create";
        public const string Pages_Company_Branches_View = "Pages.Company.Branches.View";
        public const string Pages_Company_Branches_Edit = "Pages.Company.Branches.Edit";
        public const string Pages_Company_Branches_Delete = "Pages.Company.Branches.Delete";
        public const string Pages_Company_Branches_Enable = "Pages.Company.Branches.Enable";
        public const string Pages_Company_Branches_Disable = "Pages.Company.Branches.Disable";
        public const string Pages_Company_Branches_ImportExcel = "Pages.Company.Branches.ImportExcel";
        public const string Pages_Company_Branches_ExportExcel = "Pages.Company.Branches.ExportExcel";
        public const string Pages_Company_Branches_SetAsDefault = "Pages.Company.Branches.SetAsDefault";

        #endregion

        #region Files
        public const string Pages_Files = "Pages.Files";
        public const string Pages_Files_List = "Pages.Files_List";
        public const string Pages_Files_Create = "Pages.Files.Create";
        public const string Pages_Files_View = "Pages.Files.View";
        public const string Pages_Files_Edit = "Pages.Files.Edit";
        public const string Pages_Files_Delete = "Pages.Files.Delete";
        public const string Pages_Files_ExportExcel = "Pages.Files.ExportExcel";
        public const string Pages_Files_RenameFolder = "Pages.Files.RenameFolder";
        public const string Pages_Files_ChangeFolder = "Pages.Files.ChangeFolder";

        public const string Pages_Files_Folders = "Pages.Files.Folders";
        public const string Pages_Files_Folders_Create = "Pages.Files.Folders.Create";
        public const string Pages_Files_Folders_Edit = "Pages.Files.Folders.Edit";
        public const string Pages_Files_Folders_Delete = "Pages.Files.Folders.Delete";
        #endregion

        #region POS
        public const string Pages_POSs = "Pages.POSs";

        public const string Pages_POSs_Tables = "Pages.POSs.Tables";
        public const string Pages_POSs_Tables_Create = "Pages.POSs.Tables.Create";
        public const string Pages_POSs_Tables_View = "Pages.POSs.Tables.View";
        public const string Pages_POSs_Tables_Edit = "Pages.POSs.Tables.Edit";
        public const string Pages_POSs_Tables_Delete = "Pages.POSs.Tables.Delete";
        public const string Pages_POSs_Tables_ExportExcel = "Pages.POSs.Tables.ExportExcel";
        public const string Pages_POSs_Tables_ImportExcel = "Pages.POSs.Tables.ImportExcel";
        public const string Pages_POSs_Tables_Enable = "Pages.POSs.Tables.Enable";
        public const string Pages_POSs_Tables_Disable = "Pages.POSs.Tables.Disable";

        public const string Pages_POSs_Tables_Groups = "Pages.POSs.Tables.Groups";
        public const string Pages_POSs_Tables_Groups_Create = "Pages.POSs.Tables.Groups.Create";
        public const string Pages_POSs_Tables_Groups_View = "Pages.POSs.Tables.Groups.View";
        public const string Pages_POSs_Tables_Groups_Edit = "Pages.POSs.Tables.Groups.Edit";
        public const string Pages_POSs_Tables_Groups_Delete = "Pages.POSs.Tables.Groups.Delete";
        public const string Pages_POSs_Tables_Groups_ExportExcel = "Pages.POSs.Tables.Groups.ExportExcel";
        public const string Pages_POSs_Tables_Groups_ImportExcel = "Pages.POSs.Tables.Groups.ImportExcel";
        public const string Pages_POSs_Tables_Groups_Enable = "Pages.POSs.Tables.Groups.Enable";
        public const string Pages_POSs_Tables_Groups_Disable = "Pages.POSs.Tables.Groups.Disable";

        public const string Pages_POSs_Rooms = "Pages.POSs.Rooms";
        public const string Pages_POSs_Rooms_Create = "Pages.POSs.Rooms.Create";
        public const string Pages_POSs_Rooms_View = "Pages.POSs.Rooms.View";
        public const string Pages_POSs_Rooms_Edit = "Pages.POSs.Rooms.Edit";
        public const string Pages_POSs_Rooms_Delete = "Pages.POSs.Rooms.Delete";
        public const string Pages_POSs_Rooms_ExportExcel = "Pages.POSs.Rooms.ExportExcel";
        public const string Pages_POSs_Rooms_ImportExcel = "Pages.POSs.Rooms.ImportExcel";
        public const string Pages_POSs_Rooms_Enable = "Pages.POSs.Rooms.Enable";
        public const string Pages_POSs_Rooms_Disable = "Pages.POSs.Rooms.Disable";
        public const string Pages_POSs_Rooms_Split = "Pages.POSs.Rooms.Split";
        public const string Pages_POSs_Rooms_Merge = "Pages.POSs.Rooms.Merge";

        public const string Pages_POSs_Rooms_Groups = "Pages.POSs.Rooms.Groups";
        public const string Pages_POSs_Rooms_Groups_Create = "Pages.POSs.Rooms.Groups.Create";
        public const string Pages_POSs_Rooms_Groups_View = "Pages.POSs.Rooms.Groups.View";
        public const string Pages_POSs_Rooms_Groups_Edit = "Pages.POSs.Rooms.Groups.Edit";
        public const string Pages_POSs_Rooms_Groups_Delete = "Pages.POSs.Rooms.Groups.Delete";
        public const string Pages_POSs_Rooms_Groups_ExportExcel = "Pages.POSs.Rooms.Groups.ExportExcel";
        public const string Pages_POSs_Rooms_Groups_ImportExcel = "Pages.POSs.Rooms.Groups.ImportExcel";
        public const string Pages_POSs_Rooms_Groups_Enable = "Pages.POSs.Rooms.Groups.Enable";
        public const string Pages_POSs_Rooms_Groups_Disable = "Pages.POSs.Rooms.Groups.Disable";      

        public const string Pages_POSs_Counters = "Pages.POSs.Counters";
        public const string Pages_POSs_Counters_Create = "Pages.POSs.Counters.Create";
        public const string Pages_POSs_Counters_View = "Pages.POSs.Counters.View";
        public const string Pages_POSs_Counters_Edit = "Pages.POSs.Counters.Edit";
        public const string Pages_POSs_Counters_Delete = "Pages.POSs.Counters.Delete";
        public const string Pages_POSs_Counters_ExportExcel = "Pages.POSs.Counters.ExportExcel";
        public const string Pages_POSs_Counters_ImportExcel = "Pages.POSs.Counters.ImportExcel";
        public const string Pages_POSs_Counters_Enable = "Pages.POSs.Counters.Enable";
        public const string Pages_POSs_Counters_Disable = "Pages.POSs.Counters.Disable";

        public const string Pages_POSs_MembersCards = "Pages.POSs.MembersCards";
        public const string Pages_POSs_MembersCards_Create = "Pages.POSs.MembersCards.Create";
        public const string Pages_POSs_MembersCards_View = "Pages.POSs.MembersCards.View";
        public const string Pages_POSs_MembersCards_Edit = "Pages.POSs.MembersCards.Edit";
        public const string Pages_POSs_MembersCards_Delete = "Pages.POSs.MembersCards.Delete";
        public const string Pages_POSs_MembersCards_ExportExcel = "Pages.POSs.MembersCards.ExportExcel";
        public const string Pages_POSs_MembersCards_ImportExcel = "Pages.POSs.MembersCards.ImportExcel";
        public const string Pages_POSs_MembersCards_Enable = "Pages.POSs.MembersCards.Enable";
        public const string Pages_POSs_MembersCards_Disable = "Pages.POSs.MembersCards.Disable";

        public const string Pages_POSs_SaleOrders = "Pages.POSs.SaleOrders";
        public const string Pages_POSs_SaleOrders_Create = "Pages.POSs.SaleOrders.Create";
        public const string Pages_POSs_SaleOrders_ConvertToInvoice = "Pages.POSs.SaleOrders.ConvertToInvoice";
        public const string Pages_POSs_SaleOrders_View = "Pages.POSs.SaleOrders.View";
        public const string Pages_POSs_SaleOrders_Edit = "Pages.POSs.SaleOrders.Edit";
        public const string Pages_POSs_SaleOrders_Delete = "Pages.POSs.SaleOrders.Delete";
        public const string Pages_POSs_SaleOrders_ExportExcel = "Pages.POSs.SaleOrders.ExportExcel";
        public const string Pages_POSs_SaleOrders_Print = "Pages.POSs.SaleOrders.Print";
        public const string Pages_POSs_SaleOrders_Void = "Pages.POSs.SaleOrders.Void";       
        public const string Pages_POSs_SaleOrders_Over24Modify = "Pages.POSs.SaleOrders.Over24Modify";
        public const string Pages_POSs_SaleOrders_Reorder = "Pages.POSs.SaleOrders.Reorder";

        public const string Pages_POSs_Invoices = "Pages.POSs.Invoices";
        public const string Pages_POSs_Invoices_Create = "Pages.POSs.Invoices.Create";
        public const string Pages_POSs_Invoices_Create_FromSaleOrder = "Pages.POSs.Invoices.Create.FromSaleOrder";
        public const string Pages_POSs_Invoices_View = "Pages.POSs.Invoices.View";
        public const string Pages_POSs_Invoices_Edit = "Pages.POSs.Invoices.Edit";
        public const string Pages_POSs_Invoices_Delete = "Pages.POSs.Invoices.Delete";
        public const string Pages_POSs_Invoices_ExportExcel = "Pages.POSs.Invoices.ExportExcel";
        public const string Pages_POSs_Invoices_Print = "Pages.POSs.Invoices.Print";
        public const string Pages_POSs_Invoices_Void = "Pages.POSs.Invoices.Void";
        public const string Pages_POSs_Invoices_CreditSale = "Pages.POSs.Invoices.CreditSale";
        public const string Pages_POSs_Invoices_Over24Modify = "Pages.POSs.Invoices.Over24Modify";

        public const string Pages_POSs_SaleReturns = "Pages.POSs.SaleReturns";
        public const string Pages_POSs_SaleReturns_Create = "Pages.POSs.SaleReturns.Create";
        public const string Pages_POSs_SaleReturns_View = "Pages.POSs.SaleReturns.View";
        public const string Pages_POSs_SaleReturns_Edit = "Pages.POSs.SaleReturns.Edit";
        public const string Pages_POSs_SaleReturns_Delete = "Pages.POSs.SaleReturns.Delete";
        public const string Pages_POSs_SaleReturns_ExportExcel = "Pages.POSs.SaleReturns.ExportExcel";
        public const string Pages_POSs_SaleReturns_Void = "Pages.POSs.SaleReturns.Void";
        public const string Pages_POSs_SaleReturns_Refund = "Pages.POSs.SaleReturns.Refund";
        public const string Pages_POSs_SaleReturns_Over24Modify = "Pages.POSs.SaleReturns.Over24Modify";

        #endregion

        #region Vendors
        public const string Pages_Vendors = "Pages.Vendors";

        public const string Pages_Vendors_VendorGroups = "Pages.Vendors.VendorGroups";
        public const string Pages_Vendors_VendorGroups_Create = "Pages.Vendors.VendorGroups.Create";
        public const string Pages_Vendors_VendorGroups_View = "Pages.Vendors.VendorGroups.View";
        public const string Pages_Vendors_VendorGroups_Edit = "Pages.Vendors.VendorGroups.Edit";
        public const string Pages_Vendors_VendorGroups_Delete = "Pages.Vendors.VendorGroups.Delete";
        public const string Pages_Vendors_VendorGroups_ExportExcel = "Pages.Vendors.VendorGroups.ExportExcel";
        public const string Pages_Vendors_VendorGroups_ImportExcel = "Pages.Vendors.VendorGroups.ImportExcel";
        public const string Pages_Vendors_VendorGroups_Enable = "Pages.Vendors.VendorGroups.Enable";
        public const string Pages_Vendors_VendorGroups_Disable = "Pages.Vendors.VendorGroups.Disable";

        public const string Pages_Vendors_List = "Pages.Vendors.List";
        public const string Pages_Vendors_List_Create = "Pages.Vendors.List.Create";
        public const string Pages_Vendors_List_View = "Pages.Vendors.List.View";
        public const string Pages_Vendors_List_Edit = "Pages.Vendors.List.Edit";
        public const string Pages_Vendors_List_EditAccount = "Pages.Vendors.List.EditAccount";
        public const string Pages_Vendors_List_SeeAccount = "Pages.Vendors.List.SeeAccount";
        public const string Pages_Vendors_List_Delete = "Pages.Vendors.List.Delete";
        public const string Pages_Vendors_List_ExportExcel = "Pages.Vendors.List.ExportExcel";
        public const string Pages_Vendors_List_ImportExcel = "Pages.Vendors.List.ImportExcel";
        public const string Pages_Vendors_List_Enable = "Pages.Vendors.List.Enable";
        public const string Pages_Vendors_List_Disable = "Pages.Vendors.List.Disable";

        public const string Pages_Vendors_PurchaseTypes = "Pages.Vendors.PurchaseTypes";
        public const string Pages_Vendors_PurchaseTypes_Create = "Pages.Vendors.PurchaseTypes.Create";
        public const string Pages_Vendors_PurchaseTypes_View = "Pages.Vendors.PurchaseTypes.View";
        public const string Pages_Vendors_PurchaseTypes_Edit = "Pages.Vendors.PurchaseTypes.Edit";
        public const string Pages_Vendors_PurchaseTypes_Delete = "Pages.Vendors.PurchaseTypes.Delete";
        public const string Pages_Vendors_PurchaseTypes_ImportExcel = "Pages.Vendors.PurchaseTypes.ImportExcel";
        public const string Pages_Vendors_PurchaseTypes_ExportExcel = "Pages.Vendors.PurchaseTypes.ExportExcel";
        public const string Pages_Vendors_PurchaseTypes_Enable = "Pages.Vendors.PurchaseTypes.Enable";
        public const string Pages_Vendors_PurchaseTypes_Disable = "Pages.Vendors.PurchaseTypes.Disable";

        public const string Pages_Vendors_PurchaseOrders = "Pages.Vendors.PurchaseOrders";
        public const string Pages_Vendors_PurchaseOrders_Create = "Pages.Vendors.PurchaseOrders.Create";
        public const string Pages_Vendors_PurchaseOrders_ConvertToBill = "Pages.Vendors.PurchaseOrders.ConvertToBill";
        public const string Pages_Vendors_PurchaseOrders_View = "Pages.Vendors.PurchaseOrders.View";
        public const string Pages_Vendors_PurchaseOrders_Edit = "Pages.Vendors.PurchaseOrders.Edit";
        public const string Pages_Vendors_PurchaseOrders_Delete = "Pages.Vendors.PurchaseOrders.Delete";
        public const string Pages_Vendors_PurchaseOrders_ExportExcel = "Pages.Vendors.PurchaseOrders.ExportExcel";
        public const string Pages_Vendors_PurchaseOrders_Print = "Pages.Vendors.PurchaseOrders.Print";
        public const string Pages_Vendors_PurchaseOrders_Void = "Pages.Vendors.PurchaseOrders.Void";
        public const string Pages_Vendors_PurchaseOrders_Over24Modify = "Pages.Vendors.PurchaseOrders.Over24Modify";
        public const string Pages_Vendors_PurchaseOrders_Reorder = "Pages.Vendors.PurchaseOrders.Reorder";

        public const string Pages_Vendors_Bills = "Pages.Vendors.Bills";       
        public const string Pages_Vendors_Bills_CreateAccountBill = "Pages.Vendors.Bills.CreateAccountBill";
        public const string Pages_Vendors_Bills_CreatePurchaseBill = "Pages.Vendors.Bills.CreatePurchaseBill";
        public const string Pages_Vendors_Bills_CreatePurchaseBill_FromPurchaseOrder = "Pages.Vendors.Bills.CreatePurchaseBill.FromPurchaseOrder";
        public const string Pages_Vendors_Bills_CreatePurchaseBill_FromInventoryPurchase = "Pages.Vendors.Bills.CreatePurchaseBill.FromInventoryPurchase";
        public const string Pages_Vendors_Bills_CreatePurchaseBill_FromInventoryPurchase_EditQty = "Pages.Vendors.Bills.CreatePurchaseBill.FromInventoryPurchase.EditQty";
        public const string Pages_Vendors_Bills_CreatePurchaseReturn = "Pages.Vendors.Bills.CreatePurchaseReturn";
        public const string Pages_Vendors_Bills_CreatePurchaseReturn_FromInventoryPurchase = "Pages.Vendors.Bills.CreatePurchaseReturn.FromInventoryPurchase";
        public const string Pages_Vendors_Bills_CreatePurchaseReturn_FromInventoryPurchaseReturn = "Pages.Vendors.Bills.CreatePurchaseReturn.FromInventoryPurchaseReturn";
        public const string Pages_Vendors_Bills_CreatePurchaseReturn_FromInventoryPurchaseReturn_EditQty = "Pages.Vendors.Bills.CreatePurchaseReturn.FromInventoryPurchaseReturn.EditQty";
        public const string Pages_Vendors_Bills_CreateAdvancePayment = "Pages.Vendors.Bills.CreateAdvancePayment";
        public const string Pages_Vendors_Bills_CreateDebitNote = "Pages.Vendors.Bills.CreateDebitNote";
        public const string Pages_Vendors_Bills_View = "Pages.Vendors.Bills.View";
        public const string Pages_Vendors_Bills_Edit = "Pages.Vendors.Bills.Edit";
        public const string Pages_Vendors_Bills_EditAccount = "Pages.Vendors.Bills.EditAccount";
        public const string Pages_Vendors_Bills_SeeAccount = "Pages.Vendors.Bills.SeeAccount";
        public const string Pages_Vendors_Bills_Delete = "Pages.Vendors.Bills.Delete";
        public const string Pages_Vendors_Bills_ExportExcel = "Pages.Vendors.Bills.ExportExcel";
        public const string Pages_Vendors_Bills_PayBill = "Pages.Vendors.Bills.PayBill";
        public const string Pages_Vendors_Bills_PaymentHistory = "Pages.Vendors.Bills.PaymentHistory";
        public const string Pages_Vendors_Bills_Void = "Pages.Vendors.Bills.Void";
        public const string Pages_Vendors_Bills_Over24Modify = "Pages.Vendors.Bills.Over24Modify";

        public const string Pages_Vendors_BillPayments = "Pages.Vendors.BillPayments";
        public const string Pages_Vendors_BillPayments_Create = "Pages.Vendors.BillPayments.Create";
        public const string Pages_Vendors_BillPayments_View = "Pages.Vendors.BillPayments.View";
        public const string Pages_Vendors_BillPayments_Edit = "Pages.Vendors.BillPayments.Edit";
        public const string Pages_Vendors_BillPayments_EditAccount = "Pages.Vendors.BillPayments.EditAccount";
        public const string Pages_Vendors_BillPayments_SeeAccount = "Pages.Vendors.BillPayments.SeeAccount";
        public const string Pages_Vendors_BillPayments_Delete = "Pages.Vendors.BillPayments.Delete";
        public const string Pages_Vendors_BillPayments_ExportExcel = "Pages.Vendors.BillPayments.ExportExcel";
        public const string Pages_Vendors_BillPayments_Print = "Pages.Vendors.BillPayments.Print";
        public const string Pages_Vendors_BillPayments_Void = "Pages.Vendors.BillPayments.Void";
        public const string Pages_Vendors_BillPayments_Over24Modify = "Pages.Vendors.BillPayments.Over24Modify";
        #endregion

        #region Customers
        public const string Pages_Customers = "Pages.Customers";

        public const string Pages_Customers_CustomerGroups = "Pages.CustomerGroups";
        public const string Pages_Customers_CustomerGroups_Create = "Pages.CustomerGroups.Create";
        public const string Pages_Customers_CustomerGroups_View = "Pages.CustomerGroups.View";
        public const string Pages_Customers_CustomerGroups_Edit = "Pages.CustomerGroups.Edit";
        public const string Pages_Customers_CustomerGroups_Delete = "Pages.CustomerGroups.Delete";
        public const string Pages_Customers_CustomerGroups_ExportExcel = "Pages.CustomerGroups.ExportExcel";
        public const string Pages_Customers_CustomerGroups_ImportExcel = "Pages.CustomerGroups.ImportExcel";
        public const string Pages_Customers_CustomerGroups_Enable = "Pages.Customers.CustomerGroups.Enable";
        public const string Pages_Customers_CustomerGroups_Disable = "Pages.Customers.CustomerGroups.Disable";

        public const string Pages_Customers_List = "Pages.Customers.List";
        public const string Pages_Customers_List_Create = "Pages.Customers.List.Create";
        public const string Pages_Customers_List_View = "Pages.Customers.List.View";
        public const string Pages_Customers_List_Edit = "Pages.Customers.List.Edit";
        public const string Pages_Customers_List_EditAccount = "Pages.Customers.List.EditAccount";
        public const string Pages_Customers_List_SeeAccount = "Pages.Customers.List.SeeAccount";
        public const string Pages_Customers_List_Delete = "Pages.Customers.List.Delete";
        public const string Pages_Customers_List_ExportExcel = "Pages.Customers.List.ExportExcel";
        public const string Pages_Customers_List_ImportExcel = "Pages.Customers.List.ImportExcel";
        public const string Pages_Customers_List_Enable = "Pages.Customers.List.Enable";
        public const string Pages_Customers_List_Disable = "Pages.Customers.List.Disable";

        public const string Pages_Customers_SaleTypes = "Pages.Customers.SaleTypes";
        public const string Pages_Customers_SaleTypes_Create = "Pages.Customers.SaleTypes.Create";
        public const string Pages_Customers_SaleTypes_View = "Pages.Customers.SaleTypes.View";
        public const string Pages_Customers_SaleTypes_Edit = "Pages.Customers.SaleTypes.Edit";
        public const string Pages_Customers_SaleTypes_Delete = "Pages.Customers.SaleTypes.Delete";
        public const string Pages_Customers_SaleTypes_ImportExcel = "Pages.Customers.SaleTypes.ImportExcel";
        public const string Pages_Customers_SaleTypes_ExportExcel = "Pages.Customers.SaleTypes.ExportExcel";
        public const string Pages_Customers_SaleTypes_Enable = "Pages.Customers.SaleTypes.Enable";
        public const string Pages_Customers_SaleTypes_Disable = "Pages.Customers.SaleTypes.Disable";

        public const string Pages_Customers_Quotations = "Pages.Customers.Quotations";
        public const string Pages_Customers_Quotations_Create = "Pages.Customers.Quotations.Create";
        public const string Pages_Customers_Quotations_ConvertToContract = "Pages.Customers.Quotations.ConvertToContract";
        public const string Pages_Customers_Quotations_ConvertToInvoice = "Pages.Customers.Quotations.ConvertToInvoice";
        public const string Pages_Customers_Quotations_View = "Pages.Customers.Quotations.View";
        public const string Pages_Customers_Quotations_Edit = "Pages.Customers.Quotations.Edit";
        public const string Pages_Customers_Quotations_Delete = "Pages.Customers.Quotations.Delete";
        public const string Pages_Customers_Quotations_ExportExcel = "Pages.Customers.Quotations.ExportExcel";
        public const string Pages_Customers_Quotations_Print = "Pages.Customers.Quotations.Print";
        public const string Pages_Customers_Quotations_Void = "Pages.Customers.Quotations.Void";
        public const string Pages_Customers_Quotations_Over24Modify = "Pages.Customers.Quotations.Over24Modify";


        public const string Pages_Customers_Contracts = "Pages.Customers.Contracts";
        public const string Pages_Customers_Contracts_Create = "Pages.Customers.Contracts.Create";
        public const string Pages_Customers_Contracts_Create_FromQuotation = "Pages.Customers.Contracts.Create.FromQuotation";
        public const string Pages_Customers_Contracts_ConvertToInvoice = "Pages.Customers.Contracts.ConvertToInvoice";
        public const string Pages_Customers_Contracts_View = "Pages.Customers.Contracts.View";
        public const string Pages_Customers_Contracts_Edit = "Pages.Customers.Contracts.Edit";
        public const string Pages_Customers_Contracts_Delete = "Pages.Customers.Contracts.Delete";
        public const string Pages_Customers_Contracts_ExportExcel = "Pages.Customers.Contracts.ExportExcel";
        public const string Pages_Customers_Contracts_Print = "Pages.Customers.Contracts.Print";
        public const string Pages_Customers_Contracts_Void = "Pages.Customers.Contracts.Void";
        public const string Pages_Customers_Contracts_Renew = "Pages.Customers.Contracts.Renew";
        public const string Pages_Customers_Contracts_Enable = "Pages.Customers.Contracts.Enable";
        public const string Pages_Customers_Contracts_Disable = "Pages.Customers.Contracts.Disable";
        public const string Pages_Customers_Contracts_Over24Modify = "Pages.Customers.Contracts.Over24Modify";

        public const string Pages_Customers_Contracts_Alerts = "Pages.Customers.Contracts.Alerts";
        public const string Pages_Customers_Contracts_Alerts_Create = "Pages.Customers.Contracts.Alerts.Create";
        public const string Pages_Customers_Contracts_Alerts_View = "Pages.Customers.Contracts.Alerts.View";
        public const string Pages_Customers_Contracts_Alerts_Edit = "Pages.Customers.Contracts.Alerts.Edit";
        public const string Pages_Customers_Contracts_Alerts_Delete = "Pages.Customers.Contracts.Alerts.Delete";
        public const string Pages_Customers_Contracts_Alerts_ExportExcel = "Pages.Customers.Contracts.Alerts.ExportExcel";
        public const string Pages_Customers_Contracts_Alerts_Enable = "Pages.Customers.Contracts.Alerts.Enable";
        public const string Pages_Customers_Contracts_Alerts_Disable = "Pages.Customers.Contracts.Alerts.Disable";

        public const string Pages_Customers_SaleOrders = "Pages.Customers.SaleOrders";
        public const string Pages_Customers_SaleOrders_Create = "Pages.Customers.SaleOrders.Create";
        public const string Pages_Customers_SaleOrders_ConvertToInvoice = "Pages.Customers.SaleOrders.ConvertToInvoice";
        public const string Pages_Customers_SaleOrders_View = "Pages.Customers.SaleOrders.View";
        public const string Pages_Customers_SaleOrders_Edit = "Pages.Customers.SaleOrders.Edit";
        public const string Pages_Customers_SaleOrders_Delete = "Pages.Customers.SaleOrders.Delete";
        public const string Pages_Customers_SaleOrders_ExportExcel = "Pages.Customers.SaleOrders.ExportExcel";
        public const string Pages_Customers_SaleOrders_Print = "Pages.Customers.SaleOrders.Print";
        public const string Pages_Customers_SaleOrders_Void = "Pages.Customers.SaleOrders.Void";
        public const string Pages_Customers_SaleOrders_Reorder = "Pages.Customers.SaleOrders.Reorder";
        public const string Pages_Customers_SaleOrders_Over24Modify = "Pages.Customers.SaleOrders.Over24Modify";

        public const string Pages_Customers_Invoices = "Pages.Customers.Invoices";
        public const string Pages_Customers_Invoices_CreateAccountInvoice = "Pages.Customers.Invoices.CreateAccountInvoice";
        public const string Pages_Customers_Invoices_CreateSaleInvoice = "Pages.Customers.Invoices.CreateSaleInvoice";
        public const string Pages_Customers_Invoices_CreateSaleInvoice_FromQuotation = "Pages.Customers.Invoices.CreateSaleInvoice.FromQuotation";
        public const string Pages_Customers_Invoices_CreateSaleInvoice_FromContract = "Pages.Customers.Invoices.CreateSaleInvoice.FromContract";
        public const string Pages_Customers_Invoices_CreateSaleInvoice_FromSaleOrder = "Pages.Customers.Invoices.CreateSaleInvoice.FromSaleOrder";
        public const string Pages_Customers_Invoices_CreateSaleInvoice_FromInventorySale = "Pages.Customers.Invoices.CreateSaleInvoice.FromInventorySale";
        public const string Pages_Customers_Invoices_CreateSaleInvoice_FromInventorySale_EditQty = "Pages.Customers.Invoices.CreateSaleInvoice.FromInventorySale.EditQty";
        public const string Pages_Customers_Invoices_CreateSaleReturn = "Pages.Customers.Invoices.CreateSaleReturn";
        public const string Pages_Customers_Invoices_CreateSaleReturn_FromInventorySale = "Pages.Customers.Invoices.CreateSaleReturn.FromInventorySale";
        public const string Pages_Customers_Invoices_CreateSaleReturn_FromInventorySaleReturn = "Pages.Customers.Invoices.CreateSaleReturn.FromInventorySaleReturn";
        public const string Pages_Customers_Invoices_CreateSaleReturn_FromInventorySaleReturn_EditQty = "Pages.Customers.Invoices.CreateSaleReturn.FromInventorySaleReturn.EditQty";
        public const string Pages_Customers_Invoices_CreateCustomerDeposit = "Pages.Customers.Invoices.CreateCustomerDeposit";
        public const string Pages_Customers_Invoices_CreateCreditNote = "Pages.Customers.Invoices.CreateCreditNote";
        public const string Pages_Customers_Invoices_View = "Pages.Customers.Invoices.View";
        public const string Pages_Customers_Invoices_Edit = "Pages.Customers.Invoices.Edit";
        public const string Pages_Customers_Invoices_EditAccount = "Pages.Customers.Invoices.EditAccount";
        public const string Pages_Customers_Invoices_SeeAccount = "Pages.Customers.Invoices.SeeAccount";
        public const string Pages_Customers_Invoices_Delete = "Pages.Customers.Invoices.Delete";
        public const string Pages_Customers_Invoices_ExportExcel = "Pages.Customers.Invoices.ExportExcel";
        public const string Pages_Customers_Invoices_Print = "Pages.Customers.Invoices.Print";
        public const string Pages_Customers_Invoices_Payment = "Pages.Customers.Invoices.Payment";
        public const string Pages_Customers_Invoices_PaymentHistory = "Pages.Customers.Invoices.PaymentHistory";
        public const string Pages_Customers_Invoices_Void = "Pages.Customers.Invoices.Void";
        public const string Pages_Customers_Invoices_Over24Modify = "Pages.Customers.Invoices.Over24Modify";

        public const string Pages_Customers_ReceivePayments = "Pages.Customers.ReceivePayments";
        public const string Pages_Customers_ReceivePayments_Create = "Pages.Customers.ReceivePayments.Create";
        public const string Pages_Customers_ReceivePayments_View = "Pages.Customers.ReceivePayments.View";
        public const string Pages_Customers_ReceivePayments_Edit = "Pages.Customers.ReceivePayments.Edit";
        public const string Pages_Customers_ReceivePayments_EditAccount = "Pages.Customers.ReceivePayments.EditAccount";
        public const string Pages_Customers_ReceivePayments_SeeAccount = "Pages.Customers.ReceivePayments.SeeAccount";
        public const string Pages_Customers_ReceivePayments_Delete = "Pages.Customers.ReceivePayments.Delete";
        public const string Pages_Customers_ReceivePayments_ExportExcel = "Pages.Customers.ReceivePayments.ExportExcel";
        public const string Pages_Customers_ReceivePayments_Print = "Pages.Customers.ReceivePayments.Print";
        public const string Pages_Customers_ReceivePayments_Void = "Pages.Customers.ReceivePayments.Void";
        public const string Pages_Customers_ReceivePayments_Over24Modify = "Pages.Customers.ReceivePayments.Over24Modify";

        #endregion

        #region Sale Representative
        public const string Pages_SaleRepresentatives = "Pages.SaleRepresentatives";
        public const string Pages_SaleRepresentatives_Create = "Pages.SaleRepresentatives.Create";
        public const string Pages_SaleRepresentatives_View = "Pages.SaleRepresentatives.View";
        public const string Pages_SaleRepresentatives_Edit = "Pages.SaleRepresentatives.Edit";
        public const string Pages_SaleRepresentatives_Delete = "Pages.SaleRepresentatives.Delete";
        public const string Pages_SaleRepresentatives_ExportExcel = "Pages.SaleRepresentatives.ExportExcel";
        public const string Pages_SaleRepresentatives_ImportExcel = "Pages.SaleRepresentatives.ImportExcel";
        public const string Pages_SaleRepresentatives_Enable = "Pages.SaleRepresentatives.Enable";
        public const string Pages_SaleRepresentatives_Disable = "Pages.SaleRepresentatives.Disable";

        public const string Pages_SaleCommissions = "Pages.SaleCommissions";
        public const string Pages_SaleCommissions_Create = "Pages.SaleCommissions.Create";
        public const string Pages_SaleCommissions_View = "Pages.SaleCommissions.View";
        public const string Pages_SaleCommissions_Edit = "Pages.SaleCommissions.Edit";
        public const string Pages_SaleCommissions_Delete = "Pages.SaleCommissions.Delete";
        public const string Pages_SaleCommissions_ExportExcel = "Pages.SaleCommissions.ExportExcel";
        public const string Pages_SaleCommissions_Enable = "Pages.SaleCommissions.Enable";
        public const string Pages_SaleCommissions_Disable = "Pages.SaleCommissions.Disable";
        #endregion

        #region Employees
        public const string Pages_Employees = "Pages.Employees";

        #region Employee Info
        public const string Pages_Employees_List = "Pages.Employees.List";
        public const string Pages_Employees_List_Create = "Pages.Employees.List.Create";
        public const string Pages_Employees_List_View = "Pages.Employees.List.View";
        public const string Pages_Employees_List_Edit = "Pages.Employees.List.Edit";
        public const string Pages_Employees_List_EditAccount = "Pages.Employees.List.EditAccount";
        public const string Pages_Employees_List_SeeAccount = "Pages.Employees.List.SeeAccount";
        public const string Pages_Employees_List_Delete = "Pages.Employees.List.Delete";
        public const string Pages_Employees_List_ExportExcel = "Pages.Employees.List.ExportExcel";
        public const string Pages_Employees_List_ImportExcel = "Pages.Employees.List.ImportExcel";
        public const string Pages_Employees_List_Enable = "Pages.Employees.List.Enable";
        public const string Pages_Employees_List_Disable = "Pages.Employees.List.Disable";

        public const string Pages_Employees_Positions = "Pages.Employees.Positions";
        public const string Pages_Employees_Positions_Create = "Pages.Employees.Positions.Create";
        public const string Pages_Employees_Positions_View = "Pages.Employees.Positions.View";
        public const string Pages_Employees_Positions_Edit = "Pages.Employees.Positions.Edit";
        public const string Pages_Employees_Positions_Delete = "Pages.Employees.Positions.Delete";
        public const string Pages_Employees_Positions_ExportExcel = "Pages.Employees.Positions.ExportExcel";
        public const string Pages_Employees_Positions_ImportExcel = "Pages.Employees.Positions.ImportExcel";
        public const string Pages_Employees_Positions_Enable = "Pages.Employees.Positions.Enable";
        public const string Pages_Employees_Positions_Disable = "Pages.Employees.Positions.Disable";
        #endregion

        #region Attendance And Payroll
        //Public Holidays
        //Leave
        //Mission
        //Contract
        //Work Schedule Time Tables
        //Devices
        //Time Clock
        //Attandance
        //Over Time
        //Complain or Suggestion
        //Request Approval
        //Promotion
        //Incentive
        //Deduction
        //Payslip
        //Tax On Sallary
        //NSSF
        //Recruitment
        #endregion

        #endregion

        #region Accounting
        public const string Pages_Accounting = "Pages.Accounting";

        public const string Pages_Accounting_ChartOfAccounts = "Pages.Accounting.ChartOfAccounts";
        public const string Pages_Accounting_ChartOfAccounts_Create = "Pages.Accounting.ChartOfAccounts.Create";
        public const string Pages_Accounting_ChartOfAccounts_View = "Pages.Accounting.ChartOfAccounts.View";
        public const string Pages_Accounting_ChartOfAccounts_Edit = "Pages.Accounting.ChartOfAccounts.Edit";
        public const string Pages_Accounting_ChartOfAccounts_Delete = "Pages.Accounting.ChartOfAccounts.Delete";
        public const string Pages_Accounting_ChartOfAccounts_ExportExcel = "Pages.Accounting.ChartOfAccounts.ExportExcel";
        public const string Pages_Accounting_ChartOfAccounts_ImportExcel = "Pages.Accounting.ChartOfAccounts.ImportExcel";
        public const string Pages_Accounting_ChartOfAccounts_Enable = "Pages.Accounting.ChartOfAccounts.Enable";
        public const string Pages_Accounting_ChartOfAccounts_Disable = "Pages.Accounting.ChartOfAccounts.Disable";

        public const string Pages_Accounting_Journals = "Pages.Accounting.Journals";
        public const string Pages_Accounting_Journals_Create = "Pages.Accounting.Journals.Create";
        public const string Pages_Accounting_Journals_View = "Pages.Accounting.Journals.View";
        public const string Pages_Accounting_Journals_Edit = "Pages.Accounting.Journals.Edit";
        public const string Pages_Accounting_Journals_Delete = "Pages.Accounting.Journals.Delete";
        public const string Pages_Accounting_Journals_ExportExcel = "Pages.Accounting.Journals.ExportExcel";
        public const string Pages_Accounting_Journals_Print = "Pages.Accounting.Journals.Print";
        public const string Pages_Accounting_Journals_Void = "Pages.Accounting.Journals.Void";
        public const string Pages_Accounting_Journals_Over24Modify = "Pages.Accounting.Journals.Over24Modify";


        #region Banks
        public const string Pages_Accounting_Banks = "Pages.Accounting.Banks";
        public const string Pages_Accounting_Banks_CreateDeposit = "Pages.Accounting.Banks.CreateDeposit";
        public const string Pages_Accounting_Banks_CreateWithdraw = "Pages.Accounting.Banks.CreateWithdraw";
        public const string Pages_Accounting_Banks_CreateTransfer = "Pages.Accounting.Banks.CreateTransfer";
        public const string Pages_Accounting_Banks_View = "Pages.Accounting.Banks.View";
        public const string Pages_Accounting_Banks_Edit = "Pages.Accounting.Banks.Edit";
        public const string Pages_Accounting_Banks_Delete = "Pages.Accounting.Banks.Delete";
        public const string Pages_Accounting_Banks_ExportExcel = "Pages.Accounting.Banks.ExportExcel";
        public const string Pages_Accounting_Banks_Void = "Pages.Accounting.Banks.Void";
        public const string Pages_Accounting_Banks_Over24Modify = "Pages.Accounting.Banks.Over24Modify";
        #endregion

        #endregion


        #region Loans
        public const string Pages_Loans = "Pages.Loans";

        public const string Pages_Loans_Collaterals = "Pages.Loans.Collaterals";
        public const string Pages_Loans_Collaterals_Create = "Pages.Loans.Collaterals.Create";
        public const string Pages_Loans_Collaterals_View = "Pages.Loans.Collaterals.View";
        public const string Pages_Loans_Collaterals_Edit = "Pages.Loans.Collaterals.Edit";
        public const string Pages_Loans_Collaterals_Delete = "Pages.Loans.Collaterals.Delete";
        public const string Pages_Loans_Collaterals_ExportExcel = "Pages.Loans.Collaterals.ExportExcel";
        public const string Pages_Loans_Collaterals_ImportExcel = "Pages.Loans.Collaterals.ImportExcel";
        public const string Pages_Loans_Collaterals_Enable = "Pages.Loans.Collaterals.Enable";
        public const string Pages_Loans_Collaterals_Disable = "Pages.Loans.Collaterals.Disable";

        public const string Pages_Loans_InterestRates = "Pages.Loans.InterestRates";
        public const string Pages_Loans_InterestRates_Create = "Pages.Loans.InterestRates.Create";
        public const string Pages_Loans_InterestRates_View = "Pages.Loans.InterestRates.View";
        public const string Pages_Loans_InterestRates_Edit = "Pages.Loans.InterestRates.Edit";
        public const string Pages_Loans_InterestRates_Delete = "Pages.Loans.InterestRates.Delete";
        public const string Pages_Loans_InterestRates_ExportExcel = "Pages.Loans.InterestRates.ExportExcel";
        public const string Pages_Loans_InterestRates_ImportExcel = "Pages.Loans.InterestRates.ImportExcel";
        public const string Pages_Loans_InterestRates_Enable = "Pages.Loans.InterestRates.Enable";
        public const string Pages_Loans_InterestRates_Disable = "Pages.Loans.InterestRates.Disable";

        public const string Pages_Loans_Penalties = "Pages.Loans.Penalties";
        public const string Pages_Loans_Penalties_Create = "Pages.Loans.Penalties.Create";
        public const string Pages_Loans_Penalties_View = "Pages.Loans.Penalties.View";
        public const string Pages_Loans_Penalties_Edit = "Pages.Loans.Penalties.Edit";
        public const string Pages_Loans_Penalties_Delete = "Pages.Loans.Penalties.Delete";
        public const string Pages_Loans_Penalties_ExportExcel = "Pages.Loans.Penalties.ExportExcel";
        public const string Pages_Loans_Penalties_ImportExcel = "Pages.Loans.Penalties.ImportExcel";
        public const string Pages_Loans_Penalties_Enable = "Pages.Loans.Penalties.Enable";
        public const string Pages_Loans_Penalties_Disable = "Pages.Loans.Penalties.Disable";

        public const string Pages_Loans_Penalties_Alerts = "Pages.Loans.Penalties.Alerts";
        public const string Pages_Loans_Penalties_Alerts_Create = "Pages.Loans.Penalties.Alerts.Create";
        public const string Pages_Loans_Penalties_Alerts_View = "Pages.Loans.Penalties.Alerts.View";
        public const string Pages_Loans_Penalties_Alerts_Edit = "Pages.Loans.Penalties.Alerts.Edit";
        public const string Pages_Loans_Penalties_Alerts_Delete = "Pages.Loans.Penalties.Alerts.Delete";
        public const string Pages_Loans_Penalties_Alerts_ExportExcel = "Pages.Loans.Penalties.Alerts.ExportExcel";
        public const string Pages_Loans_Penalties_Alerts_ImportExcel = "Pages.Loans.Penalties.Alerts.ImportExcel";
        public const string Pages_Loans_Penalties_Alerts_Enable = "Pages.Loans.Penalties.Alerts.Enable";
        public const string Pages_Loans_Penalties_Alerts_Disable = "Pages.Loans.Penalties.Alerts.Disable";

        public const string Pages_Loans_Contracts = "Pages.Loans.Contracts";
        public const string Pages_Loans_Contracts_Create = "Pages.Loans.Contracts.Create";
        public const string Pages_Loans_Contracts_ConvertToLoan = "Pages.Loans.Contracts.ConvertToLoan";
        public const string Pages_Loans_Contracts_View = "Pages.Loans.Contracts.View";
        public const string Pages_Loans_Contracts_Edit = "Pages.Loans.Contracts.Edit";
        public const string Pages_Loans_Contracts_Delete = "Pages.Loans.Contracts.Delete";
        public const string Pages_Loans_Contracts_ExportExcel = "Pages.Loans.Contracts.ExportExcel";
        public const string Pages_Loans_Contracts_Void = "Pages.Loans.Contracts.Void";
        public const string Pages_Loans_Contracts_Enable = "Pages.Loans.Contracts.Enable";
        public const string Pages_Loans_Contracts_Disable = "Pages.Loans.Contracts.Disable";
        public const string Pages_Loans_Contracts_Renew = "Pages.Loans.Contracts.Renew";

        public const string Pages_Loans_List = "Pages.Loans.List";
        public const string Pages_Loans_List_Create = "Pages.Loans.List.Create";
        public const string Pages_Loans_List_Create_FromContract = "Pages.Loans.List.Create.FromContract";
        public const string Pages_Loans_List_View = "Pages.Loans.List.View";
        public const string Pages_Loans_List_Edit = "Pages.Loans.List.Edit";
        public const string Pages_Loans_List_EditAccount = "Pages.Loans.List.EditAccount";
        public const string Pages_Loans_List_SeeAccount = "Pages.Loans.List.SeeAccount";
        public const string Pages_Loans_List_Delete = "Pages.Loans.List.Delete";
        public const string Pages_Loans_List_ExportExcel = "Pages.Loans.List.ExportExcel";
        public const string Pages_Loans_List_Print = "Pages.Loans.List.Print";
        public const string Pages_Loans_List_Void = "Pages.Loans.List.Void";
        public const string Pages_Loans_List_Over24Modify = "Pages.Loans.List.Over24Modify";

        public const string Pages_Loans_Payments = "Pages.Loans.Payments";
        public const string Pages_Loans_Payments_Create = "Pages.Loans.Payments.Create";
        public const string Pages_Loans_Payments_View = "Pages.Loans.Payments.View";
        public const string Pages_Loans_Payments_Edit = "Pages.Loans.Payments.Edit";
        public const string Pages_Loans_Payments_EditAccount = "Pages.Loans.Payments.EditAccount";
        public const string Pages_Loans_Payments_SeeAccount = "Pages.Loans.Payments.SeeAccount";
        public const string Pages_Loans_Payments_Delete = "Pages.Loans.Payments.Delete";
        public const string Pages_Loans_Payments_ExportExcel = "Pages.Loans.Payments.ExportExcel";
        public const string Pages_Loans_Payments_Print = "Pages.Loans.Payments.Print";
        public const string Pages_Loans_Payments_Void = "Pages.Loans.Payments.Void";
        public const string Pages_Loans_Payments_Over24Modify = "Pages.Loans.Payments.Over24Modify";
        #endregion


        #region Inventories
        public const string Pages_Inventories = "Pages.Inventories";
        public const string Pages_Inventories_Common = "Pages.Inventories.Common";
        public const string Pages_Inventories_EditAccount = "Pages.Inventories.EditAccount";
        public const string Pages_Inventories_SeeAccount = "Pages.Inventories.SeeAccount";
        public const string Pages_Inventories_EditPrice = "Pages.Inventories.EditPrice";
        public const string Pages_Inventories_SeePrice = "Pages.Inventories.SeePrice";
        public const string Pages_Inventories_ExportExcel = "Pages.Inventories.ExportExcel";

        #region Item Receives
        public const string Pages_Inventories_Receipts = "Pages.Inventories_Receipts";

        public const string Pages_Inventories_Receipts_Purchases = "Pages.Inventories.Receipts.Purchases";
        public const string Pages_Inventories_Receipts_Purchases_Create = "Pages.Inventories.Receipts.Purchases.Create";
        public const string Pages_Inventories_Receipts_Purchases_View = "Pages.Inventories.Receipts.Purchases.View";
        public const string Pages_Inventories_Receipts_Purchases_Edit = "Pages.Inventories.Receipts.Purchases.Edit";
        public const string Pages_Inventories_Receipts_Purchases_Delete = "Pages.Inventories.Receipts.Purchases.Delete";
        public const string Pages_Inventories_Receipts_Purchases_Void = "Pages.Inventories.Receipts.Purchases.Void";
        public const string Pages_Inventories_Receipts_Purchases_Over24Modify = "Pages.Inventories.Receipts.Purchases.Over24Modify";

        public const string Pages_Inventories_Issues_PurchaseReturns = "Pages.Inventories.Issues.PurchaseReturns";
        public const string Pages_Inventories_Issues_PurchaseReturns_Create = "Pages.Inventories.Issues.PurchaseReturns.Create";
        public const string Pages_Inventories_Issues_PurchaseReturns_View = "Pages.Inventories.Issues.PurchaseReturns.View";
        public const string Pages_Inventories_Issues_PurchaseReturns_Edit = "Pages.Inventories.Issues.PurchaseReturns.Edit";
        public const string Pages_Inventories_Issues_PurchaseReturns_Delete = "Pages.Inventories.Issues.PurchaseReturns.Delete";
        public const string Pages_Inventories_Issues_PurchaseReturns_Void = "Pages.Inventories.Issues.PurchaseReturns.Void";
        public const string Pages_Inventories_Issues_PurchaseReturns_Over24Modify = "Pages.Inventories.Issues.PurchaseReturns.Over24Modify";

        public const string Pages_Inventories_Receipts_Adjustments = "Pages.Inventories.Receipts.Adjustments";
        public const string Pages_Inventories_Receipts_Adjustments_Create = "Pages.Inventories.Receipts.Adjustments.Create";
        public const string Pages_Inventories_Receipts_Adjustments_View = "Pages.Inventories.Receipts.Adjustments.View";
        public const string Pages_Inventories_Receipts_Adjustments_Edit = "Pages.Inventories.Receipts.Adjustments.Edit";
        public const string Pages_Inventories_Receipts_Adjustments_Delete = "Pages.Inventories.Receipts.Adjustments.Delete";
        public const string Pages_Inventories_Receipts_Adjustments_Void = "Pages.Inventories.Receipts.Adjustments.Void";
        public const string Pages_Inventories_Receipts_Adjustments_Over24Modify = "Pages.Inventories.Receipts.Adjustments.Over24Modify";

        public const string Pages_Inventories_Receipts_Others = "Pages.Inventories.Receipts.Others";
        public const string Pages_Inventories_Receipts_Others_Create = "Pages.Inventories.Receipts.Others.Create";
        public const string Pages_Inventories_Receipts_Others_View = "Pages.Inventories.Receipts.Others.View";
        public const string Pages_Inventories_Receipts_Others_Edit = "Pages.Inventories.Receipts.Others.Edit";
        public const string Pages_Inventories_Receipts_Others_Delete = "Pages.Inventories.Receipts.Others.Delete";
        public const string Pages_Inventories_Receipts_Others_Void = "Pages.Inventories.Receipts.Others.Void";
        public const string Pages_Inventories_Receipts_Others_ImportExcel = "Pages.Inventories.Receipts.Others.ImportExcel";
        public const string Pages_Inventories_Receipts_Others_Over24Modify = "Pages.Inventories.Receipts.Others.Over24Modify";
        #endregion

        #region Item Issues
        public const string Pages_Inventories_Issues = "Pages.Inventories.Issues";
     
        public const string Pages_Inventories_Issues_Sales = "Pages.Inventories.Issues.Sales";
        public const string Pages_Inventories_Issues_Sales_Create = "Pages.Inventories.Issues.Sales.Create";
        public const string Pages_Inventories_Issues_Sales_View = "Pages.Inventories.Issues.Sales.View";
        public const string Pages_Inventories_Issues_Sales_Edit = "Pages.Inventories.Issues.Sales.Edit";
        public const string Pages_Inventories_Issues_Sales_Delete = "Pages.Inventories.Issues.Sales.Delete";
        public const string Pages_Inventories_Issues_Sales_Void = "Pages.Inventories.Issues.Sales.Void";
        public const string Pages_Inventories_Issues_Sales_Over24Modify = "Pages.Inventories.Issues.Sales.Over24Modify";

        public const string Pages_Inventories_Receipts_SaleReturns = "Pages.Inventories.Receipts.SaleReturns";
        public const string Pages_Inventories_Receipts_SaleReturns_Create = "Pages.Inventories.Receipts.SaleReturns.Create";
        public const string Pages_Inventories_Receipts_SaleReturns_View = "Pages.Inventories.Receipts.SaleReturns.View";
        public const string Pages_Inventories_Receipts_SaleReturns_Edit = "Pages.Inventories.Receipts.SaleReturns.Edit";
        public const string Pages_Inventories_Receipts_SaleReturns_Delete = "Pages.Inventories.Receipts.SaleReturns.Delete";
        public const string Pages_Inventories_Receipts_SaleReturns_Void = "Pages.Inventories.Receipts.SaleReturns.Void";
        public const string Pages_Inventories_Receipts_SaleReturns_Over24Modify = "Pages.Inventories.Receipts.SaleReturns.Over24Modify";

        public const string Pages_Inventories_Issues_CustomerBorrows = "Pages.Inventories.Issues.CustomerBorrows";
        public const string Pages_Inventories_Issues_CustomerBorrows_Create = "Pages.Inventories.Issues.CustomerBorrows.Create";
        public const string Pages_Inventories_Issues_CustomerBorrows_View = "Pages.Inventories.Issues.CustomerBorrows.View";
        public const string Pages_Inventories_Issues_CustomerBorrows_Edit = "Pages.Inventories.Issues.CustomerBorrows.Edit";
        public const string Pages_Inventories_Issues_CustomerBorrows_Delete = "Pages.Inventories.Issues.CustomerBorrows.Delete";
        public const string Pages_Inventories_Issues_CustomerBorrows_Void = "Pages.Inventories.Issues.CustomerBorrows.Void";
        public const string Pages_Inventories_Issues_CustomerBorrows_Over24Modify = "Pages.Inventories.Issues.CustomerBorrows.Over24Modify";

        public const string Pages_Inventories_Receipts_CustomerBorrowReturns = "Pages.Inventories.Receipts.CustomerBorrowReturns";
        public const string Pages_Inventories_Receipts_CustomerBorrowReturns_Create = "Pages.Inventories.Receipts.CustomerBorrowReturns.Create";
        public const string Pages_Inventories_Receipts_CustomerBorrowReturns_View = "Pages.Inventories.Receipts.CustomerBorrowReturns.View";
        public const string Pages_Inventories_Receipts_CustomerBorrowReturns_Edit = "Pages.Inventories.Receipts.CustomerBorrowReturns.Edit";
        public const string Pages_Inventories_Receipts_CustomerBorrowReturns_Delete = "Pages.Inventories.Receipts.CustomerBorrowReturns.Delete";
        public const string Pages_Inventories_Receipts_CustomerBorrowReturns_Void = "Pages.Inventories.Receipts.CustomerBorrowReturns.Void";
        public const string Pages_Inventories_Receipts_CustomerBorrowReturns_Over24Modify = "Pages.Inventories.Receipts.CustomerBorrowReturns.Over24Modify";

        public const string Pages_Inventories_Issues_Adjustments = "Pages.Inventories.Issues.Adjustments";
        public const string Pages_Inventories_Issues_Adjustments_Create = "Pages.Inventories.Issues.Adjustments.Create";
        public const string Pages_Inventories_Issues_Adjustments_View = "Pages.Inventories.Issues.Adjustments.View";
        public const string Pages_Inventories_Issues_Adjustments_Edit = "Pages.Inventories.Issues.Adjustments.Edit";
        public const string Pages_Inventories_Issues_Adjustments_Delete = "Pages.Inventories.Issues.Adjustments.Delete";
        public const string Pages_Inventories_Issues_Adjustments_Void = "Pages.Inventories.Issues.Adjustments.Void";
        public const string Pages_Inventories_Issues_Adjustments_Over24Modify = "Pages.Inventories.Issues.Adjustments.Over24Modify";

        public const string Pages_Inventories_Issues_Others = "Pages.Inventories.Issues.Others";
        public const string Pages_Inventories_Issues_Others_Create = "Pages.Inventories.Issues.Others.Create";
        public const string Pages_Inventories_Issues_Others_View = "Pages.Inventories.Issues.Others.View";
        public const string Pages_Inventories_Issues_Others_Edit = "Pages.Inventories.Issues.Others.Edit";
        public const string Pages_Inventories_Issues_Others_Delete = "Pages.Inventories.Issues.Others.Delete";
        public const string Pages_Inventories_Issues_Others_Void = "Pages.Inventories.Issues.Others.Void";
        public const string Pages_Inventories_Issues_Others_Over24Modify = "Pages.Inventories.Issues.Others.Over24Modify";
        #endregion

        #region Transfers
        public const string Pages_Inventories_Transfers = "Pages.Inventories.Transfers";
        public const string Pages_Inventories_Transfers_Create = "Pages.Inventories.Transfers.Create";
        public const string Pages_Inventories_Transfers_View = "Pages.Inventories.Transfers.View";
        public const string Pages_Inventories_Transfers_Edit = "Pages.Inventories.Transfers.Edit";
        public const string Pages_Inventories_Transfers_Delete = "Pages.Inventories.Transfers.Delete";
        public const string Pages_Inventories_Transfers_Void = "Pages.Inventories.Transfers.Void";
        public const string Pages_Inventories_Transfers_Over24Modify = "Pages.Inventories.Transfers.Over24Modify";
        #endregion

        #region Exchanges
        public const string Pages_Inventories_Exchanges = "Pages.Inventories.Exchanges";
        public const string Pages_Inventories_Exchanges_Create = "Pages.Inventories.Exchanges.Create";
        public const string Pages_Inventories_Exchanges_View = "Pages.Inventories.Exchanges.View";
        public const string Pages_Inventories_Exchanges_Edit = "Pages.Inventories.Exchanges.Edit";
        public const string Pages_Inventories_Exchanges_Delete = "Pages.Inventories.Exchanges.Delete";
        public const string Pages_Inventories_Exchanges_Void = "Pages.Inventories.Exchanges.Void";
        public const string Pages_Inventories_Exchanges_Over24Modify = "Pages.Inventories.Exchanges.Over24Modify";
        #endregion

        #region Productions
        public const string Pages_Productions = "Pages.Productions";

        public const string Pages_Productions_Orders = "Pages.Productions.Orders";
        public const string Pages_Productions_Orders_Create = "Pages.Productions.Orders.Create";
        public const string Pages_Productions_Orders_View = "Pages.Productions.Orders.View";
        public const string Pages_Productions_Orders_Edit = "Pages.Productions.Orders.Edit";
        public const string Pages_Productions_Orders_Delete = "Pages.Productions.Orders.Delete";
        public const string Pages_Productions_Orders_Void = "Pages.Productions.Orders.Void";
        public const string Pages_Productions_Orders_Over24Modify = "Pages.Productions.Orders.Over24Modify";

        public const string Pages_Productions_Lines = "Pages.Productions.Lines";
        public const string Pages_Productions_Lines_Create = "Pages.Productions.Lines.Create";
        public const string Pages_Productions_Lines_View = "Pages.Productions.Lines.View";
        public const string Pages_Productions_Lines_Edit = "Pages.Productions.Lines.Edit";
        public const string Pages_Productions_Lines_Delete = "Pages.Productions.Lines.Delete";
        public const string Pages_Productions_Lines_Enable = "Pages.Productions.Lines.Enable";
        public const string Pages_Productions_Lines_Disable = "Pages.Productions.Lines.Disable";

        public const string Pages_Productions_List = "Pages.Productions_List";
        public const string Pages_Productions_List_Create = "Pages.Productions.List.Create";
        public const string Pages_Productions_List_View = "Pages.Productions.List.View";
        public const string Pages_Productions_List_Edit = "Pages.Productions.List.Edit";
        public const string Pages_Productions_List_Delete = "Pages.Productions.List.Delete";
        public const string Pages_Productions_List_Void = "Pages.Productions.List.Void";
        public const string Pages_Productions_List_Over24Modify = "Pages.Productions.List.Over24Modify";

        public const string Pages_Productions_Processes = "Pages.Productions.Processes";
        public const string Pages_Productions_Processes_Create = "Pages.Productions.Processes.Create";
        public const string Pages_Productions_Processes_View = "Pages.Productions.Processes.View";
        public const string Pages_Productions_Processes_Edit = "Pages.Productions.Processes.Edit";
        public const string Pages_Productions_Processes_Delete = "Pages.Productions.Processes.Delete";
        public const string Pages_Productions_Processes_Enable = "Pages.Productions.Processes.Enable";
        public const string Pages_Productions_Processes_Disable = "Pages.Productions.Processes.Disable";
        #endregion

        #endregion

        #region Reports
        public const string Pages_Reports = "Pages.Reports";

        public const string Pages_Reports_Common = "Pages.Reports.Common";
        public const string Pages_Reports_See = "Pages.Reports.See";
        public const string Pages_Reports_ModifyAllTemplates = "Pages.Reports.ModifyAllTemplates";

        #region Reports Vendors
        public const string Pages_Reports_Vendors = "Pages.Reports.Vendors";

        public const string Pages_Reports_Vendors_PurchaseBills = "Pages.Reports.Vendors.PurchaseBills";
        public const string Pages_Reports_Vendors_PurchaseBills_View = "Pages.Reports.Vendors.PurchaseBills.View";
        public const string Pages_Reports_Vendors_PurchaseBills_ExportExcel = "Pages.Reports.Vendors.PurchaseBills.ExportExcel";
        public const string Pages_Reports_Vendors_PurchaseBills_Print = "Pages.Reports.Vendors.PurchaseBills.Print";
        public const string Pages_Reports_Vendors_PurchaseBills_CreateTemplate = "Pages.Reports.Vendors.PurchaseBills.CreateTemplate";
        public const string Pages_Reports_Vendors_PurchaseBills_ModifyTemplate = "Pages.Reports.Vendors.PurchaseBills.ModifyTemplate";

        public const string Pages_Reports_Vendors_PurchaseBillDetail = "Pages.Reports.Vendors.PurchaseBillDetail";
        public const string Pages_Reports_Vendors_PurchaseBillDetail_View = "Pages.Reports.Vendors.PurchaseBillDetail.View";
        public const string Pages_Reports_Vendors_PurchaseBillDetail_ExportExcel = "Pages.Reports.Vendors.PurchaseBillDetail.ExportExcel";
        public const string Pages_Reports_Vendors_PurchaseBillDetail_Print = "Pages.Reports.Vendors.PurchaseBillDetail.Print";
        public const string Pages_Reports_Vendors_PurchaseBillDetail_CreateTemplate = "Pages.Reports.Vendors.PurchaseBillDetail.CreateTemplate";
        public const string Pages_Reports_Vendors_PurchaseBillDetail_ModifyTemplate = "Pages.Reports.Vendors.PurchaseBillDetail.ModifyTemplate";

        public const string Pages_Reports_Vendors_VendorAging = "Pages.Reports.Vendors.VendorAging";
        public const string Pages_Reports_Vendors_VendorAging_View = "Pages.Reports.Vendors.VendorAging.View";
        public const string Pages_Reports_Vendors_VendorAging_ExportExcel = "Pages.Reports.Vendors.VendorAging.ExportExcel";
        public const string Pages_Reports_Vendors_VendorAging_Print = "Pages.Reports.Vendors.VendorAging.Print";
        public const string Pages_Reports_Vendors_VendorAging_CreateTemplate = "Pages.Reports.Vendors.VendorAging.CreateTemplate";
        public const string Pages_Reports_Vendors_VendorAging_ModifyTemplate = "Pages.Reports.Vendors.VendorAging.ModifyTemplate";
        #endregion

        #region Reports Customers
        public const string Pages_Reports_Customers = "Pages.Reports.Customers";

        public const string Pages_Reports_Customers_SaleInvoices = "Pages.Reports.Customers.SaleInvoices";
        public const string Pages_Reports_Customers_SaleInvoices_View = "Pages.Reports.Customers.SaleInvoices.View";
        public const string Pages_Reports_Customers_SaleInvoices_ExportExcel = "Pages.Reports.Customers.SaleInvoices.ExportExcel";
        public const string Pages_Reports_Customers_SaleInvoices_Print = "Pages.Reports.Customers.SaleInvoices.Print";
        public const string Pages_Reports_Customers_SaleInvoices_CreateTemplate = "Pages.Reports.Customers.SaleInvoices.CreateTemplate";
        public const string Pages_Reports_Customers_SaleInvoices_ModifyTemplate = "Pages.Reports.Customers.SaleInvoices.ModifyTemplate";

        public const string Pages_Reports_Customers_SaleInvoiceDetail = "Pages.Reports.Customers.SaleInvoiceDetail";
        public const string Pages_Reports_Customers_SaleInvoiceDetail_View = "Pages.Reports.Customers.SaleInvoiceDetail.View";
        public const string Pages_Reports_Customers_SaleInvoiceDetail_ExportExcel = "Pages.Reports.Customers.SaleInvoiceDetail.ExportExcel";
        public const string Pages_Reports_Customers_SaleInvoiceDetail_Print = "Pages.Reports.Customers.SaleInvoiceDetail.Print";
        public const string Pages_Reports_Customers_SaleInvoiceDetail_CreateTemplate = "Pages.Reports.Customers.SaleInvoiceDetail.CreateTemplate";
        public const string Pages_Reports_Customers_SaleInvoiceDetail_ModifyTemplate = "Pages.Reports.Customers.SaleInvoiceDetail.ModifyTemplate";

        public const string Pages_Reports_Customers_CustomerAging = "Pages.Reports.Customers.CustomerAging";
        public const string Pages_Reports_Customers_CustomerAging_View = "Pages.Reports.Customers.CustomerAging.View";
        public const string Pages_Reports_Customers_CustomerAging_ExportExcel = "Pages.Reports.Customers.CustomerAging.ExportExcel";
        public const string Pages_Reports_Customers_CustomerAging_Print = "Pages.Reports.Customers.CustomerAging.Print";
        public const string Pages_Reports_Customers_CustomerAging_CreateTemplate = "Pages.Reports.Customers.CustomerAging.CreateTemplate";
        public const string Pages_Reports_Customers_CustomerAging_ModifyTemplate = "Pages.Reports.Customers.CustomerAging.ModifyTemplate";

        #endregion

        #region Reports Accounting
        public const string Pages_Reports_Accounting = "Pages.Reports.Accounting";

        public const string Pages_Reports_Accounting_Journals = "Pages.Reports.Accounting.Journals";
        public const string Pages_Reports_Accounting_Journals_View = "Pages.Reports.Accounting.Journals.View";
        public const string Pages_Reports_Accounting_Journals_EditAccount = "Pages.Reports.Accounting.Journals.EditAccount";
        public const string Pages_Reports_Accounting_Journals_ExportExcel = "Pages.Reports.Accounting.Journals.ExportExcel";
        public const string Pages_Reports_Accounting_Journals_Print = "Pages.Reports.Accounting.Journals.Print";
        public const string Pages_Reports_Accounting_Journals_CreateTemplate = "Pages.Reports.Accounting.Journals.CreateTemplate";
        public const string Pages_Reports_Accounting_Journals_ModifyTemplate = "Pages.Reports.Accounting.Journals.ModifyTemplate";

        public const string Pages_Reports_Accounting_Ledger = "Pages.Reports.Accounting.Ledger";
        public const string Pages_Reports_Accounting_Ledger_View = "Pages.Reports.Accounting.Ledger.View";
        public const string Pages_Reports_Accounting_Ledger_EditAccount = "Pages.Reports.Accounting.Ledger.EditAccount";
        public const string Pages_Reports_Accounting_Ledger_ExportExcel = "Pages.Reports.Accounting.Ledger.ExportExcel";
        public const string Pages_Reports_Accounting_Ledger_Print = "Pages.Reports.Accounting.Ledger.Print";
        public const string Pages_Reports_Accounting_Ledger_CreateTemplate = "Pages.Reports.Accounting.Ledger.CreateTemplate";
        public const string Pages_Reports_Accounting_Ledger_ModifyTemplate = "Pages.Reports.Accounting.Ledger.ModifyTemplate";

        public const string Pages_Reports_Accounting_ProfitLoss = "Pages.Reports.Accounting.ProfitLoss";
        public const string Pages_Reports_Accounting_ProfitLoss_View = "Pages.Reports.Accounting.ProfitLoss.View";
        public const string Pages_Reports_Accounting_ProfitLoss_ExportExcel = "Pages.Reports.Accounting.ProfitLoss.ExportExcel";
        public const string Pages_Reports_Accounting_ProfitLoss_Print = "Pages.Reports.Accounting.ProfitLoss.Print";
        public const string Pages_Reports_Accounting_ProfitLoss_CreateTemplate = "Pages.Reports.Accounting.ProfitLoss.CreateTemplate";
        public const string Pages_Reports_Accounting_ProfitLoss_ModifyTemplate = "Pages.Reports.Accounting.ProfitLoss.ModifyTemplate";

        public const string Pages_Reports_Accounting_RetainEarning = "Pages.Reports.Accounting.RetainEarning";
        public const string Pages_Reports_Accounting_RetainEarning_View = "Pages.Reports.Accounting.RetainEarning.View";
        public const string Pages_Reports_Accounting_RetainEarning_EditAccount = "Pages.Reports.Accounting.RetainEarning.EditAccount";
        public const string Pages_Reports_Accounting_RetainEarning_ExportExcel = "Pages.Reports.Accounting.RetainEarning.ExportExcel";
        public const string Pages_Reports_Accounting_RetainEarning_Print = "Pages.Reports.Accounting.RetainEarning.Print";
        public const string Pages_Reports_Accounting_RetainEarning_CreateTemplate = "Pages.Reports.Accounting.RetainEarning.CreateTemplate";
        public const string Pages_Reports_Accounting_RetainEarning_ModifyTemplate = "Pages.Reports.Accounting.RetainEarning.ModifyTemplate";

        public const string Pages_Reports_Accounting_BalanceSheet = "Pages.Reports.Accounting.BalanceSheet";
        public const string Pages_Reports_Accounting_BalanceSheet_View = "Pages.Reports.Accounting.BalanceSheet.View";
        public const string Pages_Reports_Accounting_BalanceSheet_ExportExcel = "Pages.Reports.Accounting.BalanceSheet.ExportExcel";
        public const string Pages_Reports_Accounting_BalanceSheet_Print = "Pages.Reports.Accounting.BalanceSheet.Print";
        public const string Pages_Reports_Accounting_BalanceSheet_CreateTemplate = "Pages.Reports.Accounting.BalanceSheet.CreateTemplate";
        public const string Pages_Reports_Accounting_BalanceSheet_ModifyTemplate = "Pages.Reports.Accounting.BalanceSheet.ModifyTemplate";

        public const string Pages_Reports_Accounting_TrialBalance = "Pages.Reports.Accounting.TrialBalance";
        public const string Pages_Reports_Accounting_TrialBalance_View = "Pages.Reports.Accounting.TrialBalance.View";
        public const string Pages_Reports_Accounting_TrialBalance_ExportExcel = "Pages.Reports.Accounting.TrialBalance.ExportExcel";
        public const string Pages_Reports_Accounting_TrialBalance_Print = "Pages.Reports.Accounting.TrialBalance.Print";
        public const string Pages_Reports_Accounting_TrialBalance_CreateTemplate = "Pages.Reports.Accounting.TrialBalance.CreateTemplate";
        public const string Pages_Reports_Accounting_TrialBalance_ModifyTemplate = "Pages.Reports.Accounting.TrialBalance.ModifyTemplate";

        public const string Pages_Reports_Accounting_CashFlow = "Pages.Reports.Accounting.CashFlow";
        public const string Pages_Reports_Accounting_CashFlow_View = "Pages.Reports.Accounting.CashFlow.View";
        public const string Pages_Reports_Accounting_CashFlow_ExportExcel = "Pages.Reports.Accounting.CashFlow.ExportExcel";
        public const string Pages_Reports_Accounting_CashFlow_Print = "Pages.Reports.Accounting.CashFlow.Print";
        public const string Pages_Reports_Accounting_CashFlow_CreateTemplate = "Pages.Reports.Accounting.CashFlow.CreateTemplate";
        public const string Pages_Reports_Accounting_CashFlow_ModifyTemplate = "Pages.Reports.Accounting.CashFlow.ModifyTemplate";

        public const string Pages_Reports_Accounting_CashFlowDetail = "Pages.Reports.Accounting.CashFlowDetail";
        public const string Pages_Reports_Accounting_CashFlowDetail_View = "Pages.Reports.Accounting.CashFlowDetail.View";
        public const string Pages_Reports_Accounting_CashFlowDetail_ExportExcel = "Pages.Reports.Accounting.CashFlowDetail.ExportExcel";
        public const string Pages_Reports_Accounting_CashFlowDetail_Print = "Pages.Reports.Accounting.CashFlowDetail.Print";
        public const string Pages_Reports_Accounting_CashFlowDetail_CreateTemplate = "Pages.Reports.Accounting.CashFlowDetail.CreateTemplate";
        public const string Pages_Reports_Accounting_CashFlowDetail_ModifyTemplate = "Pages.Reports.Accounting.CashFlowDetail.ModifyTemplate";
        #endregion

        #region Loans
        public const string Pages_Reports_Loans = "Pages.Reports.Loans";

        public const string Pages_Reports_Loans_Balances = "Pages.Reports.Loans.Balances";
        public const string Pages_Reports_Loans_Balances_View = "Pages.Reports.Loans.Balances.View";
        public const string Pages_Reports_Loans_Balances_ExportExcel = "Pages.Reports.Loans.Balances.ExportExcel";
        public const string Pages_Reports_Loans_Balances_Print = "Pages.Reports.Loans.Balances.Print";
        public const string Pages_Reports_Loans_Balances_CreateTemplate = "Pages.Reports.Loans.Balances.CreateTemplate";
        public const string Pages_Reports_Loans_Balances_ModifyTemplate = "Pages.Reports.Loans.Balances.ModifyTemplate";

        public const string Pages_Reports_Loans_Collections = "Pages.Reports.Loans.Collections";
        public const string Pages_Reports_Loans_Collections_View = "Pages.Reports.Loans.Collections.View";
        public const string Pages_Reports_Loans_Collections_ExportExcel = "Pages.Reports.Loans.Collections.ExportExcel";
        public const string Pages_Reports_Loans_Collections_Print = "Pages.Reports.Loans.Collections.Print";
        public const string Pages_Reports_Loans_Collections_CreateTemplate = "Pages.Reports.Loans.Collections.CreateTemplate";
        public const string Pages_Reports_Loans_Collections_ModifyTemplate = "Pages.Reports.Loans.Collections.ModifyTemplate";

        public const string Pages_Reports_Loans_Collaterals = "Pages.Reports.Loans.Collaterals";
        public const string Pages_Reports_Loans_Collaterals_View = "Pages.Reports.Loans.Collaterals.View";
        public const string Pages_Reports_Loans_Collaterals_ExportExcel = "Pages.Reports.Loans.Collaterals.ExportExcel";
        public const string Pages_Reports_Loans_Collaterals_Print = "Pages.Reports.Loans.Collaterals.Print";
        public const string Pages_Reports_Loans_Collaterals_CreateTemplate = "Pages.Reports.Loans.Collaterals.CreateTemplate";
        public const string Pages_Reports_Loans_Collaterals_ModifyTemplate = "Pages.Reports.Loans.Collaterals.ModifyTemplate";
        #endregion

        #region Reports Inventory
        public const string Pages_Reports_Inventories = "Pages.Reports.Inventories";

        public const string Pages_Reports_Inventories_StockBalance = "Pages.Reports.Inventories.StockBalance";
        public const string Pages_Reports_Inventories_StockBalance_View = "Pages.Reports.Inventories.StockBalance.View";
        public const string Pages_Reports_Inventories_StockBalance_ExportExcel = "Pages.Reports.Inventories.StockBalance.ExportExcel";
        public const string Pages_Reports_Inventories_StockBalance_Print = "Pages.Reports.Inventories.StockBalance.Print";
        public const string Pages_Reports_Inventories_StockBalance_CreateTemplate = "Pages.Reports.Inventories.StockBalance.CreateTemplate";
        public const string Pages_Reports_Inventories_StockBalance_ModifyTemplate = "Pages.Reports.Inventories.StockBalance.ModifyTemplate";

        public const string Pages_Reports_Inventories_Transactions = "Pages.Reports.Inventories.Transactions"; 
        public const string Pages_Reports_Inventories_Transactions_View = "Pages.Reports.Inventories.Transactions.View";
        public const string Pages_Reports_Inventories_Transactions_ExportExcel = "Pages.Reports.Inventories.Transactions.ExportExcel";
        public const string Pages_Reports_Inventories_Transactions_Print = "Pages.Reports.Inventories.Transactions.Print";
        public const string Pages_Reports_Inventories_Transactions_CreateTemplate = "Pages.Reports.Inventories.Transactions.CreateTemplate";
        public const string Pages_Reports_Inventories_Transactions_ModifyTemplate = "Pages.Reports.Inventories.Transactions.ModifyTemplate";

        public const string Pages_Reports_Inventories_TransactionDetail = "Pages.Reports.Inventories.TransactionDetail";
        public const string Pages_Reports_Inventories_TransactionDetail_View = "Pages.Reports.Inventories.TransactionDetail.View";
        public const string Pages_Reports_Inventories_TransactionDetail_ExportExcel = "Pages.Reports.Inventories.TransactionDetail.ExportExcel";
        public const string Pages_Reports_Inventories_TransactionDetail_Print = "Pages.Reports.Inventories.TransactionDetail.Print";
        public const string Pages_Reports_Inventories_TransactionDetail_CreateTemplate = "Pages.Reports.Inventories.TransactionDetail.CreateTemplate";
        public const string Pages_Reports_Inventories_TransactionDetail_ModifyTemplate = "Pages.Reports.Inventories.TransactionDetail.ModifyTemplate";

        public const string Pages_Reports_Inventories_CustomerBorrows = "Pages.Reports.Inventories.CustomerBorrows";
        public const string Pages_Reports_Inventories_CustomerBorrows_View = "Pages.Reports.Inventories.CustomerBorrows.View";
        public const string Pages_Reports_Inventories_CustomerBorrows_ExportExcel = "Pages.Reports.Inventories.CustomerBorrows.ExportExcel";
        public const string Pages_Reports_Inventories_CustomerBorrows_Print = "Pages.Reports.Inventories.CustomerBorrows.Print";
        public const string Pages_Reports_Inventories_CustomerBorrows_CreateTemplate = "Pages.Reports.Inventories.CustomerBorrows.CreateTemplate";
        public const string Pages_Reports_Inventories_CustomerBorrows_ModifyTemplate = "Pages.Reports.Inventories.CustomerBorrows.ModifyTemplate";

        public const string Pages_Reports_Inventories_CostSummary = "Pages.Reports.Inventories.CostSummary";
        public const string Pages_Reports_Inventories_CostSummary_View = "Pages.Reports.Inventories.CostSummary.View";
        public const string Pages_Reports_Inventories_CostSummary_ExportExcel = "Pages.Reports.Inventories.CostSummary.ExportExcel";
        public const string Pages_Reports_Inventories_CostSummary_Print = "Pages.Reports.Inventories.CostSummary.Print";
        public const string Pages_Reports_Inventories_CostSummary_CreateTemplate = "Pages.Reports.Inventories.CostSummary.CreateTemplate";
        public const string Pages_Reports_Inventories_CostSummary_ModifyTemplate = "Pages.Reports.Inventories.CostSummary.ModifyTemplate";

        public const string Pages_Reports_Inventories_CostDetail = "Pages.Reports.Inventories.CostDetail";
        public const string Pages_Reports_Inventories_CostDetail_View = "Pages.Reports.Inventories.CostDetail.View";
        public const string Pages_Reports_Inventories_CostDetail_ExportExcel = "Pages.Reports.Inventories.CostDetail.ExportExcel";
        public const string Pages_Reports_Inventories_CostDetail_Print = "Pages.Reports.Inventories.CostDetail.Print";
        public const string Pages_Reports_Inventories_CostDetail_CreateTemplate = "Pages.Reports.Inventories.CostDetail.CreateTemplate";
        public const string Pages_Reports_Inventories_CostDetail_ModifyTemplate = "Pages.Reports.Inventories.CostDetail.ModifyTemplate";
        #endregion

        #endregion


        #region Pages Administrations
        public const string Pages_Administrations = "Pages.Administrations";

        //public const string Pages_Administrations_Groups = "Pages.Administrations.Groups";
        //public const string Pages_Administrations_Groups_Create = "Pages.Administrations.Groups.Create";
        //public const string Pages_Administrations_Groups_View = "Pages.Administrations.Groups.View";
        //public const string Pages_Administrations_Groups_Edit = "Pages.Administrations.Groups.Edit";
        //public const string Pages_Administrations_Groups_Delete = "Pages.Administrations.Groups.Delete";
        //public const string Pages_Administrations_Groups_Enable = "Pages.Administrations.Groups.Enable";
        //public const string Pages_Administrations_Groups_Disable = "Pages.Administrations.Groups.Disable";
        //public const string Pages_Administrations_Groups_ChangeMember = "Pages.Administrations.Groups.ChangeMember";

        public const string Pages_Administrations_Users = "Pages.Administrations.Users";
        public const string Pages_Administrations_Users_Create = "Pages.Administrations.Users.Create";
        public const string Pages_Administrations_Users_View = "Pages.Administrations.Users.View";
        public const string Pages_Administrations_Users_Edit = "Pages.Administrations.Users.Edit";
        public const string Pages_Administrations_Users_Delete = "Pages.Administrations.Users.Delete";
        public const string Pages_Administrations_Users_Enable = "Pages.Administrations.Users.Enable";
        public const string Pages_Administrations_Users_Disable = "Pages.Administrations.Users.Disable";
        public const string Pages_Administrations_Users_Activate = "Pages.Administrations.Users.Activate";
        public const string Pages_Administrations_Users_Deactivate = "Pages.Administrations.Users.Deactivate";
        public const string Pages_Administrations_Users_ResetPassword = "Pages.Administrations.Users.ResetPassword";
        public const string Pages_Administrations_Users_ChangePermissions = "Pages.Administrations.Users.ChangePermissions";
        public const string Pages_Administrations_Users_Impersonation = "Pages.Administrations.Users.Impersonation";

        public const string Pages_Administrations_Roles = "Pages.Administrations.Roles";
        public const string Pages_Administrations_Roles_Create = "Pages.Administrations.Roles.Create";
        public const string Pages_Administrations_Roles_View = "Pages.Administrations.Roles.View";
        public const string Pages_Administrations_Roles_Edit = "Pages.Administrations.Roles.Edit";
        public const string Pages_Administrations_Roles_Delete = "Pages.Administrations.Roles.Delete";
        public const string Pages_Administrations_Roles_Enable = "Pages.Administrations.Roles.Enable";
        public const string Pages_Administrations_Roles_Disable = "Pages.Administrations.Roles.Disable";

        public const string Pages_Administrations_AuditLogs = "Pages.Administrations.AuditLogs";
        public const string Pages_Administrations_AuditLogs_View = "Pages.Administrations.AuditLogs.View";
        public const string Pages_Administrations_AuditLogs_ExportExcel = "Pages.Administrations.AuditLogs.ExportExcel";

        public const string Pages_Administrations_OrganizationUnits = "Pages.Administrations.OrganizationUnits";
        public const string Pages_Administrations_OrganizationUnits_Create = "Pages.Administrations.OrganizationUnits.Create";
        public const string Pages_Administrations_OrganizationUnits_View = "Pages.Administrations.OrganizationUnits.View";
        public const string Pages_Administrations_OrganizationUnits_Edit = "Pages.Administrations.OrganizationUnits.Edit";
        public const string Pages_Administrations_OrganizationUnits_Delete = "Pages.Administrations.OrganizationUnits.Delete";
        public const string Pages_Administrations_OrganizationUnits_Enable = "Pages.Administrations.OrganizationUnits.Enable";
        public const string Pages_Administrations_OrganizationUnits_Disable = "Pages.Administrations.OrganizationUnits.Disable";
        public const string Pages_Administrations_OrganizationUnits_ManageOrganizationTree = "Pages.Administrations.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administrations_OrganizationUnits_ManageMembers = "Pages.Administrations.OrganizationUnits.ManageMembers";
        #endregion


    }
}
