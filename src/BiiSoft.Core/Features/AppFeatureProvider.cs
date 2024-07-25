using Abp;
using Abp.Application.Features;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.Runtime.Validation;
using Abp.UI.Inputs;
using System.Collections.Generic;

namespace BiiSoft.Features
{
    public class AppFeatureProvider : FeatureProvider
    {
        public override void SetFeatures(IFeatureDefinitionContext context)
        {
            #region ComboBox Sample
            //context.Create(
            //    AppFeatures.Over24Modify, 
            //    defaultValue: "", 
            //    displayName: L("Over24Modify"), 
            //    inputType: new ComboboxInputType() {
            //        Validator = new NumericValueValidator(0, int.MaxValue),
            //        ItemSource = new StaticLocalizableComboboxItemSource(                
            //            new LocalizableComboboxItem("", L("None")),
            //            new LocalizableComboboxItem("24", L("Over24Modify")),
            //            new LocalizableComboboxItem("48", L("Over48Modify")),
            //            new LocalizableComboboxItem("72", L("Over72Modify")))                    
            //    }
            //);
            #endregion

            var administrationFeature = context.Create(AppFeatures.Administration, defaultValue: "false", displayName: L("Administration"), inputType: new CheckboxInputType());
            var roleFeature = administrationFeature.CreateChildFeature(AppFeatures.Administration_Roles, defaultValue: "false", displayName: L("Roles"), inputType: new CheckboxInputType());
            var userFeature = administrationFeature.CreateChildFeature(AppFeatures.Administration_Users, defaultValue: "false", displayName: L("Users"), inputType: new CheckboxInputType());
            userFeature.CreateChildFeature(
                AppFeatures.MaxUserCount,
                defaultValue: "0", //0 = unlimited
                displayName: L("MaximumUserCount"),
                description: L("MaximumCount_Description"),
                inputType: new SingleLineStringInputType(new NumericValueValidator(0, int.MaxValue))
            )
            [FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
            {
                ValueTextNormalizer = value => value == "0" ? L("Unlimited") : new FixedLocalizableString(value),
                IsVisibleOnPricingTable = true
            };
            //userFeature.CreateChildFeature(AppFeatures.Administration_Users_Groups, defaultValue: "false", displayName: L("Groups"), inputType: new CheckboxInputType());
            administrationFeature.CreateChildFeature(AppFeatures.Administration_AuditLogs, defaultValue: "false", displayName: L("AuditLogs"), inputType: new CheckboxInputType());
            administrationFeature.CreateChildFeature(AppFeatures.Administration_OrganizationUnits, defaultValue: "false", displayName: L("OrganizationUnits"), inputType: new CheckboxInputType());

            var chatFeature = context.Create(AppFeatures.Chat, defaultValue: "false", displayName: L("Chat"), inputType: new CheckboxInputType());
            chatFeature.CreateChildFeature(AppFeatures.Chat_TenantToTenant, defaultValue: "false", displayName: L("TenantToTenant"), inputType: new CheckboxInputType());
            chatFeature.CreateChildFeature(AppFeatures.Chat_TenantToHost, defaultValue: "false", displayName: L("TenantToHost"), inputType: new CheckboxInputType());

            var setupFeature = context.Create(AppFeatures.Setup, defaultValue: "false", displayName: L("Setup"), inputType: new CheckboxInputType());            
            setupFeature.CreateChildFeature(AppFeatures.Setup_Loctions, defaultValue: "false", displayName: L("Locations"), inputType: new CheckboxInputType());

            var itemFeature = setupFeature.CreateChildFeature(AppFeatures.Setup_Items, defaultValue: "false", displayName: L("Items"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_Groups, defaultValue: "false", displayName: L("Groups"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_WeightUnits, defaultValue: "false", displayName: L("WeightUnits"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_Units, defaultValue: "false", displayName: L("Units"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_Grades, defaultValue: "false", displayName: L("Grades"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_Sizes, defaultValue: "false", displayName: L("Sizes"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_ColorPatterns, defaultValue: "false", displayName: L("ColorPatterns"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_Brands, defaultValue: "false", displayName: L("Brands"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_FieldAs, defaultValue: "false", displayName: L("FieldAs"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_FieldBs, defaultValue: "false", displayName: L("FieldBs"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_FieldCs, defaultValue: "false", displayName: L("FieldCs"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_PriceLevels, defaultValue: "false", displayName: L("PriceLevels"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_Promotions, defaultValue: "false", displayName: L("Promotions"), inputType: new CheckboxInputType());
            itemFeature.CreateChildFeature(AppFeatures.Setup_Items_Scores, defaultValue: "false", displayName: L("Scores"), inputType: new CheckboxInputType());
            setupFeature.CreateChildFeature(AppFeatures.Setup_PaymentMethods, defaultValue: "false", displayName: L("PaymentMethods"), inputType: new CheckboxInputType());
            setupFeature.CreateChildFeature(AppFeatures.Setup_Classes, defaultValue: "false", displayName: L("Classes"), inputType: new CheckboxInputType());
            setupFeature.CreateChildFeature(AppFeatures.Setup_Taxes, defaultValue: "false", displayName: L("Taxes"), inputType: new CheckboxInputType());

            var warehouseFeature = setupFeature.CreateChildFeature(AppFeatures.Setup_Warehouses, defaultValue: "false", displayName: L("Warehouses"), inputType: new CheckboxInputType());
            warehouseFeature.CreateChildFeature(AppFeatures.Setup_Warehouses_Slots, defaultValue: "false", displayName: L("Slots"), inputType: new CheckboxInputType());
            warehouseFeature.CreateChildFeature(
                AppFeatures.MaxWarehouseCount,
                defaultValue: "0", //0 = unlimited
                displayName: L("MaximumWarehouseCount"),
                description: L("MaximumCount_Description"),
                inputType: new SingleLineStringInputType(new NumericValueValidator(0, int.MaxValue))
            )
            [FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
            {
                ValueTextNormalizer = value => value == "0" ? L("Unlimited") : new FixedLocalizableString(value),
                IsVisibleOnPricingTable = true
            };

            setupFeature.CreateChildFeature(AppFeatures.Setup_FormTemplates, defaultValue: "false", displayName: L("FormTemplates"), inputType: new CheckboxInputType());

            var companyFeature = context.Create(AppFeatures.Company, defaultValue: "false", displayName: L("Company"), inputType: new CheckboxInputType());
            var branchFeature = companyFeature.CreateChildFeature(AppFeatures.Company_Branches, defaultValue: "false", displayName: L("Branches"), inputType: new CheckboxInputType());
            branchFeature.CreateChildFeature(
                AppFeatures.MaxBranchCount,
                defaultValue: "0", //0 = unlimited
                displayName: L("MaximumBranchCount"),
                description: L("MaximumCount_Description"),
                inputType: new SingleLineStringInputType(new NumericValueValidator(0, int.MaxValue))
            )
            [FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
            {
                ValueTextNormalizer = value => value == "0" ? L("Unlimited") : new FixedLocalizableString(value),
                IsVisibleOnPricingTable = true
            };
            companyFeature.CreateChildFeature(AppFeatures.Company_MultiCurrencies, defaultValue: "false", displayName: L("MultiCurrencies"), inputType: new CheckboxInputType());

            context.Create(AppFeatures.Files, defaultValue: "false", displayName: L("Files"), inputType: new CheckboxInputType());
            context.Create(AppFeatures.Over24Modify, defaultValue: "false", displayName: L("Over24Modify"), inputType: new CheckboxInputType());
            
            var posFeature = context.Create(AppFeatures.POS, defaultValue: "false", displayName: L("POS"), inputType: new CheckboxInputType());
            var tableFeature = posFeature.CreateChildFeature(AppFeatures.POS_Tables, defaultValue: "false", displayName: L("Tables"), inputType: new CheckboxInputType());
            tableFeature.CreateChildFeature(AppFeatures.POS_Tables_Groups, defaultValue: "false", displayName: L("TableGroups"), inputType: new CheckboxInputType());
            var roomFeature = posFeature.CreateChildFeature(AppFeatures.POS_Rooms, defaultValue: "false", displayName: L("Rooms"), inputType: new CheckboxInputType());
            roomFeature.CreateChildFeature(AppFeatures.POS_Rooms_Groups, defaultValue: "false", displayName: L("RoomGroups"), inputType: new CheckboxInputType());
            posFeature.CreateChildFeature(AppFeatures.POS_Counters, defaultValue: "false", displayName: L("Counters"), inputType: new CheckboxInputType());
            posFeature.CreateChildFeature(AppFeatures.POS_MembersCards, defaultValue: "false", displayName: L("MembersCards"), inputType: new CheckboxInputType());
            posFeature.CreateChildFeature(AppFeatures.POS_SaleOrders, defaultValue: "false", displayName: L("SaleOrders"), inputType: new CheckboxInputType());
            posFeature.CreateChildFeature(AppFeatures.POS_Invoices, defaultValue: "false", displayName: L("Invoices"), inputType: new CheckboxInputType());
            posFeature.CreateChildFeature(AppFeatures.POS_SaleReturns, defaultValue: "false", displayName: L("SaleReturns"), inputType: new CheckboxInputType());

            var vendorFeature = context.Create(AppFeatures.Vendors, defaultValue: "false", displayName: L("Vendors"), inputType: new CheckboxInputType());
            vendorFeature.CreateChildFeature(AppFeatures.Vendors_Groups, defaultValue: "false", displayName: L("Groups"), inputType: new CheckboxInputType());
            vendorFeature.CreateChildFeature(AppFeatures.Vendors_PurchaseTypes, defaultValue: "false", displayName: L("PurchaseTypes"), inputType: new CheckboxInputType());
            vendorFeature.CreateChildFeature(AppFeatures.Vendors_PurchaseOrders, defaultValue: "false", displayName: L("PurchaseOrders"), inputType: new CheckboxInputType());
            var billFeature = vendorFeature.CreateChildFeature(AppFeatures.Vendors_Bills, defaultValue: "false", displayName: L("Bills"), inputType: new CheckboxInputType());
            billFeature.CreateChildFeature(AppFeatures.Vendors_Bills_AutoInventory, defaultValue: "false", displayName: L("AutoInventory"), inputType: new CheckboxInputType());
            vendorFeature.CreateChildFeature(AppFeatures.Vendors_BillPayments, defaultValue: "false", displayName: L("BillPayments"), inputType: new CheckboxInputType());

            var customerFeature = context.Create(AppFeatures.Customers, defaultValue: "false", displayName: L("Customers"), inputType: new CheckboxInputType());
            customerFeature.CreateChildFeature(AppFeatures.Customers_Groups, defaultValue: "false", displayName: L("Groups"), inputType: new CheckboxInputType());
            customerFeature.CreateChildFeature(AppFeatures.Customers_SaleTypes, defaultValue: "false", displayName: L("SaleTypes"), inputType: new CheckboxInputType());
            customerFeature.CreateChildFeature(AppFeatures.Customers_Quotations, defaultValue: "false", displayName: L("Quotations"), inputType: new CheckboxInputType());
            var customerContractFeature = customerFeature.CreateChildFeature(AppFeatures.Customers_Contracts, defaultValue: "false", displayName: L("Contracts"), inputType: new CheckboxInputType());
            customerContractFeature.CreateChildFeature(AppFeatures.Customers_Contracts_Alerts, defaultValue: "false", displayName: L("Alerts"), inputType: new CheckboxInputType());
            customerFeature.CreateChildFeature(AppFeatures.Customers_SaleOrders, defaultValue: "false", displayName: L("SaleOrders"), inputType: new CheckboxInputType());
            var invoiceFeature = customerFeature.CreateChildFeature(AppFeatures.Customers_Invoices, defaultValue: "false", displayName: L("Invoices"), inputType: new CheckboxInputType());
            invoiceFeature.CreateChildFeature(AppFeatures.Customers_Invoices_AutoInventory, defaultValue: "false", displayName: L("AutoInventory"), inputType: new CheckboxInputType());
            customerFeature.CreateChildFeature(AppFeatures.Customers_ReceivePayments, defaultValue: "false", displayName: L("ReceivePayments"), inputType: new CheckboxInputType());

            context.Create(AppFeatures.SaleRepresentatives, defaultValue: "false", displayName: L("SaleRepresentatives"), inputType: new CheckboxInputType());
            context.Create(AppFeatures.SaleCommissions, defaultValue: "false", displayName: L("SaleCommissions"), inputType: new CheckboxInputType());

            var employeeFeature = context.Create(AppFeatures.Employees, defaultValue: "false", displayName: L("Employees"), inputType: new CheckboxInputType());
            employeeFeature.CreateChildFeature(AppFeatures.Employees_Positons, defaultValue: "false", displayName: L("Positions"), inputType: new CheckboxInputType());

            var accountingFeature = context.Create(AppFeatures.Accounting, defaultValue: "false", displayName: L("Accounting"), inputType: new CheckboxInputType());
            accountingFeature.CreateChildFeature(AppFeatures.Accounting_ChartOfAccounts, defaultValue: "false", displayName: L("ChartOfAccounts"), inputType: new CheckboxInputType());
            accountingFeature.CreateChildFeature(AppFeatures.Accounting_Journals, defaultValue: "false", displayName: L("Journals"), inputType: new CheckboxInputType());
            accountingFeature.CreateChildFeature(AppFeatures.Accounting_Banks, defaultValue: "false", displayName: L("Banks"), inputType: new CheckboxInputType());

            var loanFeature = context.Create(AppFeatures.Loans, defaultValue: "false", displayName: L("Loans"), inputType: new CheckboxInputType());
            loanFeature.CreateChildFeature(AppFeatures.Loans_Collaterals, defaultValue: "false", displayName: L("Collaterals"), inputType: new CheckboxInputType());
            loanFeature.CreateChildFeature(AppFeatures.Loans_InterestRates, defaultValue: "false", displayName: L("InterestRates"), inputType: new CheckboxInputType());
            var loanPenaltyFeature = loanFeature.CreateChildFeature(AppFeatures.Loans_Penalties, defaultValue: "false", displayName: L("Penalties"), inputType: new CheckboxInputType());
            loanPenaltyFeature.CreateChildFeature(AppFeatures.Loans_Penalties_Alerts, defaultValue: "false", displayName: L("Alerts"), inputType: new CheckboxInputType());
            loanFeature.CreateChildFeature(AppFeatures.Loans_Contracts, defaultValue: "false", displayName: L("Contracts"), inputType: new CheckboxInputType());
            loanFeature.CreateChildFeature(AppFeatures.Loans_Payments, defaultValue: "false", displayName: L("Payments"), inputType: new CheckboxInputType());

            var inventoryFeature = context.Create(AppFeatures.Inventories, defaultValue: "false", displayName: L("Inventories"), inputType: new CheckboxInputType());
            
            var itemReceiptFeature = inventoryFeature.CreateChildFeature(AppFeatures.Inventories_Receipts, defaultValue: "false", displayName: L("Receipts"), inputType: new CheckboxInputType());
            itemReceiptFeature.CreateChildFeature(AppFeatures.Inventories_Receipts_Purchases, defaultValue: "false", displayName: L("Purchases"), inputType: new CheckboxInputType());
            itemReceiptFeature.CreateChildFeature(AppFeatures.Inventories_Receipts_Adjustments, defaultValue: "false", displayName: L("ReceiptAdjustments"), inputType: new CheckboxInputType());
            itemReceiptFeature.CreateChildFeature(AppFeatures.Inventories_Receipts_Others, defaultValue: "false", displayName: L("ReceiptOthers"), inputType: new CheckboxInputType());
            
            var itemIssueFeature = inventoryFeature.CreateChildFeature(AppFeatures.Inventories_Issues, defaultValue: "false", displayName: L("Issues"), inputType: new CheckboxInputType());
            itemIssueFeature.CreateChildFeature(AppFeatures.Inventories_Issues_Sales, defaultValue: "false", displayName: L("Sales"), inputType: new CheckboxInputType());
            itemIssueFeature.CreateChildFeature(AppFeatures.Inventories_Issues_Adjustments, defaultValue: "false", displayName: L("IssueAdjustments"), inputType: new CheckboxInputType());
            itemIssueFeature.CreateChildFeature(AppFeatures.Inventories_Issues_CustomerBorrows, defaultValue: "false", displayName: L("CustomerBorrows"), inputType: new CheckboxInputType());
            itemIssueFeature.CreateChildFeature(AppFeatures.Inventories_Issues_Others, defaultValue: "false", displayName: L("IssueOthers"), inputType: new CheckboxInputType());
            
            inventoryFeature.CreateChildFeature(AppFeatures.Inventories_Transfers, defaultValue: "false", displayName: L("Transfers"), inputType: new CheckboxInputType());
            inventoryFeature.CreateChildFeature(AppFeatures.Inventories_Exchanges, defaultValue: "false", displayName: L("Exchanges"), inputType: new CheckboxInputType());

            var productionFeature = context.Create(AppFeatures.Productions, defaultValue: "false", displayName: L("Productions"), inputType: new CheckboxInputType());
            productionFeature.CreateChildFeature(AppFeatures.Productions_Orders, defaultValue: "false", displayName: L("ProductionOrders"), inputType: new CheckboxInputType());
            productionFeature.CreateChildFeature(AppFeatures.Productions_Lines, defaultValue: "false", displayName: L("ProductionLines"), inputType: new CheckboxInputType());
            productionFeature.CreateChildFeature(AppFeatures.Productions_Processes, defaultValue: "false", displayName: L("ProductionProcesses"), inputType: new CheckboxInputType());

            var reportFeature = context.Create(AppFeatures.Reports, defaultValue: "false", displayName: L("Reports"), inputType: new CheckboxInputType());
            
            var vendorReportFeature =  reportFeature.CreateChildFeature(AppFeatures.Reports_Vendors, defaultValue: "false", displayName: L("Vendors"), inputType: new CheckboxInputType());
            vendorReportFeature.CreateChildFeature(AppFeatures.Reports_Vendors_PurchaseBills, defaultValue: "false", displayName: L("PurchaseBills"), inputType: new CheckboxInputType());
            vendorReportFeature.CreateChildFeature(AppFeatures.Reports_Vendors_PurchaseBillDetail, defaultValue: "false", displayName: L("PurchaseBillDetail"), inputType: new CheckboxInputType());
            vendorReportFeature.CreateChildFeature(AppFeatures.Reports_Vendors_Aging, defaultValue: "false", displayName: L("Aging"), inputType: new CheckboxInputType());

            var customerReportFeature = reportFeature.CreateChildFeature(AppFeatures.Reports_Customers, defaultValue: "false", displayName: L("Customers"), inputType: new CheckboxInputType());
            customerReportFeature.CreateChildFeature(AppFeatures.Reports_Customers_SaleInvoices, defaultValue: "false", displayName: L("SaleInvoices"), inputType: new CheckboxInputType());
            customerReportFeature.CreateChildFeature(AppFeatures.Reports_Customers_SaleInvoiceDetail, defaultValue: "false", displayName: L("SaleInvoiceDetail"), inputType: new CheckboxInputType());
            customerReportFeature.CreateChildFeature(AppFeatures.Reports_Customers_Aging, defaultValue: "false", displayName: L("Aging"), inputType: new CheckboxInputType());

            var accountingReportFeature = reportFeature.CreateChildFeature(AppFeatures.Reports_Accounting, defaultValue: "false", displayName: L("Accounting"), inputType: new CheckboxInputType());
            accountingReportFeature.CreateChildFeature(AppFeatures.Reports_Accounting_Journals, defaultValue: "false", displayName: L("Journals"), inputType: new CheckboxInputType());
            accountingReportFeature.CreateChildFeature(AppFeatures.Reports_Accounting_Ledger, defaultValue: "false", displayName: L("Ledger"), inputType: new CheckboxInputType());
            accountingReportFeature.CreateChildFeature(AppFeatures.Reports_Accounting_ProfitLoss, defaultValue: "false", displayName: L("ProfitLoss"), inputType: new CheckboxInputType());
            accountingReportFeature.CreateChildFeature(AppFeatures.Reports_Accounting_RetainEarning, defaultValue: "false", displayName: L("RetainEarning"), inputType: new CheckboxInputType());
            accountingReportFeature.CreateChildFeature(AppFeatures.Reports_Accounting_BalanceSheet, defaultValue: "false", displayName: L("BalanceSheet"), inputType: new CheckboxInputType());
            accountingReportFeature.CreateChildFeature(AppFeatures.Reports_Accounting_TrialBalance, defaultValue: "false", displayName: L("TrialBalance"), inputType: new CheckboxInputType());
            accountingReportFeature.CreateChildFeature(AppFeatures.Reports_Accounting_CashFlow, defaultValue: "false", displayName: L("CashFlow"), inputType: new CheckboxInputType());
            accountingReportFeature.CreateChildFeature(AppFeatures.Reports_Accounting_CashFlowDetail, defaultValue: "false", displayName: L("CashFlowDetail"), inputType: new CheckboxInputType());

            var loadReportFeature = reportFeature.CreateChildFeature(AppFeatures.Reports_Loans, defaultValue: "false", displayName: L("Loans"), inputType: new CheckboxInputType());
            loadReportFeature.CreateChildFeature(AppFeatures.Reports_Loans_Balance, defaultValue: "false", displayName: L("Balance"), inputType: new CheckboxInputType());
            loadReportFeature.CreateChildFeature(AppFeatures.Reports_Loans_Collections, defaultValue: "false", displayName: L("Collections"), inputType: new CheckboxInputType());
            loadReportFeature.CreateChildFeature(AppFeatures.Reports_Loans_Collaterals, defaultValue: "false", displayName: L("Collaterals"), inputType: new CheckboxInputType());

            var inventoryReportFeature = reportFeature.CreateChildFeature(AppFeatures.Reports_Inventories, defaultValue: "false", displayName: L("Inventories"), inputType: new CheckboxInputType());
            inventoryReportFeature.CreateChildFeature(AppFeatures.Reports_Inventories_StockBalance, defaultValue: "false", displayName: L("StockBalance"), inputType: new CheckboxInputType());
            inventoryReportFeature.CreateChildFeature(AppFeatures.Reports_Inventories_Transactions, defaultValue: "false", displayName: L("Transactions"), inputType: new CheckboxInputType());
            inventoryReportFeature.CreateChildFeature(AppFeatures.Reports_Inventories_TransactionDetail, defaultValue: "false", displayName: L("TransactionDetail"), inputType: new CheckboxInputType());
            inventoryReportFeature.CreateChildFeature(AppFeatures.Reports_Inventories_CustomerBorrows, defaultValue: "false", displayName: L("CustomerBorrows"), inputType: new CheckboxInputType());
            inventoryReportFeature.CreateChildFeature(AppFeatures.Reports_Inventories_CostSummary, defaultValue: "false", displayName: L("CostSummary"), inputType: new CheckboxInputType());
            inventoryReportFeature.CreateChildFeature(AppFeatures.Reports_Inventories_CostDetail, defaultValue: "false", displayName: L("CostDetail"), inputType: new CheckboxInputType());

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BiiSoftConsts.LocalizationSourceName);
        }
    }
}
