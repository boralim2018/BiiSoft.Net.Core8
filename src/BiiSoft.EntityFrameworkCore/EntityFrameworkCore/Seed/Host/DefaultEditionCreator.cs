using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Features;
using BiiSoft.Editions;
using BiiSoft.Features;
using Abp.Application.Editions;

namespace BiiSoft.EntityFrameworkCore.Seed.Host
{
    public class DefaultEditionCreator
    {
        private readonly BiiSoftDbContext _context;

        public DefaultEditionCreator(BiiSoftDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            #region Standard Edition
            var defaultEdition = CreateEditionIfNotExists(EditionManager.StandardEditionName, EditionManager.StandardEditionDisplayName);
            /* Add desired features to the standard edition, if wanted... */
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.MaxBranchCount, 1);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.MaxWarehouseCount, 1);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.MaxUserCount, 1);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Chat, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Chat_TenantToTenant, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Chat_TenantToHost, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Loctions, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_Groups, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_Units, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_Models, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_Grades, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_Sizes, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_ColorPatterns, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_Brands, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_Series, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_FieldAs, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_FieldBs, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_FieldCs, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_PriceLevels, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Items_Promotions, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_PaymentMethods, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Classes, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_FormTemplates, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Setup_Taxes, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Company, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Files, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.POS, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.POS_Counters, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.POS_Invoices, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.POS_SaleReturns, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Vendors, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Vendors_Groups, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Vendors_Bills, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Vendors_Bills_AutoInventory, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Vendors_BillPayments, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Customers, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Customers_Groups, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Customers_SaleTypes, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Customers_Invoices, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Customers_Invoices_AutoInventory, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Customers_ReceivePayments, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Accounting, true);           
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Accounting_ChartOfAccounts, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Accounting_Journals, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Accounting_Banks, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories_Receipts, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories_Receipts_Purchases, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories_Receipts_Adjustments, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories_Receipts_Others, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories_Issues, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories_Issues_Sales, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories_Issues_Adjustments, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories_Issues_Others, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories_Transfers, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Inventories_Exchanges, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Vendors, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Vendors_PurchaseBills, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Vendors_PurchaseBillDetail, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Vendors_Aging, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Customers, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Customers_SaleInvoices, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Customers_SaleInvoiceDetail, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Customers_Aging, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Accounting, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Accounting_Journals, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Accounting_Ledger, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Accounting_ProfitLoss, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Accounting_RetainEarning, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Accounting_BalanceSheet, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Accounting_TrialBalance, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Accounting_CashFlow, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Accounting_CashFlowDetail, true);

            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Inventories, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Inventories_StockBalance, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Inventories_Transactions, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Inventories_TransactionDetail, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Inventories_CustomerBorrows, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Inventories_CostSummary, true);
            CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.Reports_Inventories_CostDetail, true);
            #endregion

            #region Gold Edition
            var goldEdition = CreateEditionIfNotExists(EditionManager.GoldEditionName, EditionManager.GoldEditionDisplayName);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.MaxWarehouseCount, 2);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.MaxWarehouseCount, 4);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.MaxUserCount, 5);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Chat, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Chat_TenantToTenant, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Chat_TenantToHost, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Administration, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Administration_Roles, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Administration_Users, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Administration_AuditLogs, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Administration_OrganizationUnits, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Loctions, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_Groups, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_Units, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_Models, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_Grades, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_Sizes, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_ColorPatterns, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_Brands, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_Series, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_FieldAs, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_FieldBs, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_FieldCs, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_PriceLevels, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Items_Promotions, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_PaymentMethods, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Classes, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Warehouses, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Warehouses_Slots, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_FormTemplates, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Setup_Taxes, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Company, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Company_MultiCurrencies, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Files, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.POS, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.POS_Counters, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.POS_Invoices, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.POS_SaleReturns, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Vendors, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Vendors_Groups, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Vendors_Bills, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Vendors_BillPayments, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Customers, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Customers_Groups, true);         
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Customers_SaleTypes, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Customers_Invoices, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Customers_ReceivePayments, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.SaleRepresentatives, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.SaleCommissions, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Accounting, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Accounting_ChartOfAccounts, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Accounting_Journals, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Accounting_Banks, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories_Receipts, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories_Receipts_Purchases, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories_Receipts_Adjustments, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories_Receipts_Others, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories_Issues, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories_Issues_Sales, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories_Issues_Adjustments, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories_Issues_Others, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories_Transfers, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Inventories_Exchanges, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Productions, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Productions_Orders, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Productions_Lines, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Productions_Processes, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Vendors, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Vendors_PurchaseBills, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Vendors_PurchaseBillDetail, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Vendors_Aging, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Customers, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Customers_SaleInvoices, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Customers_SaleInvoiceDetail, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Customers_Aging, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Accounting, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Accounting_Journals, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Accounting_Ledger, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Accounting_ProfitLoss, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Accounting_RetainEarning, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Accounting_BalanceSheet, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Accounting_TrialBalance, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Accounting_CashFlow, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Accounting_CashFlowDetail, true);

            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Inventories, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Inventories_StockBalance, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Inventories_Transactions, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Inventories_TransactionDetail, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Inventories_CustomerBorrows, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Inventories_CostSummary, true);
            CreateFeatureIfNotExists(goldEdition.Id, AppFeatures.Reports_Inventories_CostDetail, true);
            #endregion

            #region Diamond Edition
            var diamondEdition = CreateEditionIfNotExists(EditionManager.DiamondEditionName, EditionManager.DiamondEditionDisplayName);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.MaxBranchCount, 0);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.MaxWarehouseCount, 0);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.MaxUserCount, 0);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Chat, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Chat_TenantToTenant, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Chat_TenantToHost, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Administration, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Administration_Roles, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Administration_Users, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Administration_AuditLogs, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Administration_OrganizationUnits, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Loctions, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_Groups, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_Units, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_Models, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_Grades, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_Sizes, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_ColorPatterns, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_Brands, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_Series, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_FieldAs, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_FieldBs, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_FieldCs, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_PriceLevels, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Items_Promotions, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_PaymentMethods, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Classes, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Warehouses, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Warehouses_Slots, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_FormTemplates, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Setup_Taxes, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Company, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Company_Branches, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Company_MultiCurrencies, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Files, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Over24Modify, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.POS, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.POS_Tables, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.POS_Tables_Groups, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.POS_Rooms, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.POS_Rooms_Groups, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.POS_Counters, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.POS_SaleOrders, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.POS_Invoices, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.POS_SaleReturns, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Vendors, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Vendors_Groups, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Vendors_PurchaseTypes, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Vendors_PurchaseOrders, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Vendors_Bills, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Vendors_BillPayments, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Customers, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Customers_Groups, true);         
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Customers_SaleTypes, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Customers_SaleOrders, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Customers_Quotations, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Customers_Contracts, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Customers_Contracts_Alerts, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Customers_Invoices, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Customers_ReceivePayments, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.SaleRepresentatives, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.SaleCommissions, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Employees, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Employees_Positons, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Accounting, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Accounting_ChartOfAccounts, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Accounting_Journals, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Accounting_Banks, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Loans, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Loans_Collaterals, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Loans_InterestRates, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Loans_Penalties, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Loans_Penalties_Alerts, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Loans_Collaterals, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Loans_Payments, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Receipts, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Receipts_Purchases, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Receipts_Adjustments, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Receipts_Others, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Issues, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Issues_Sales, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Issues_CustomerBorrows, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Issues_Adjustments, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Issues_Others, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Transfers, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Inventories_Exchanges, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Productions, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Productions_Orders, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Productions_Lines, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Productions_Processes, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Vendors, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Vendors_PurchaseBills, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Vendors_PurchaseBillDetail, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Vendors_Aging, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Customers, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Customers_SaleInvoices, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Customers_SaleInvoiceDetail, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Customers_Aging, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Accounting, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Accounting_Journals, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Accounting_Ledger, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Accounting_ProfitLoss, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Accounting_RetainEarning, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Accounting_BalanceSheet, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Accounting_TrialBalance, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Accounting_CashFlow, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Accounting_CashFlowDetail, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Loans, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Loans_Balance, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Loans_Collections, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Loans_Collaterals, true);

            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Inventories, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Inventories_StockBalance, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Inventories_Transactions, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Inventories_TransactionDetail, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Inventories_CustomerBorrows, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Inventories_CostSummary, true);
            CreateFeatureIfNotExists(diamondEdition.Id, AppFeatures.Reports_Inventories_CostDetail, true);
            #endregion

        }

        private void CreateFeatureIfNotExists(int editionId, string featureName, bool isEnabled)
        {
            if (_context.EditionFeatureSettings.IgnoreQueryFilters().Any(ef => ef.EditionId == editionId && ef.Name == featureName))
            {
                return;
            }

            _context.EditionFeatureSettings.Add(new EditionFeatureSetting
            {
                Name = featureName,
                Value = isEnabled.ToString(),
                EditionId = editionId
            });
            _context.SaveChanges();
        }

        private void CreateFeatureIfNotExists(int editionId, string featureName, int value)
        {
            if (_context.EditionFeatureSettings.IgnoreQueryFilters().Any(ef => ef.EditionId == editionId && ef.Name == featureName))
            {
                return;
            }

            _context.EditionFeatureSettings.Add(new EditionFeatureSetting
            {
                Name = featureName,
                Value = value.ToString(),
                EditionId = editionId
            });
            _context.SaveChanges();
        }

        private Edition CreateEditionIfNotExists(string name, string displayName = "")
        {
            var edition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == name);
            if (edition == null)
            {
                edition = new Edition { Name = name, DisplayName = displayName == "" ? name : displayName };
                _context.Editions.Add(edition);
                _context.SaveChanges();
            }
            return edition;
        }
    }
}
