using Abp;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Enums
{

    public enum AccountType
    {
        Cash = 10,
        Bank = 11,
        AccountReceivable = 12,
        Inventory = 13,
        CurrentAsset = 14,
        FixedAsset = 15,
        NoneCurrentAsset = 16,
        AccountPayable = 20,
        CreditCard = 21,
        CurrentLiability = 22,
        NoneCurrentLiability = 23,
        Equity = 30,
        Revenue = 40,
        OtherRevenue = 41,
        CostOfSale = 50,
        Expense = 51,
        OtherExpense = 52
    }

    public enum SubAccountType
    {
        CashOnHand = 1000,
        CashEquivalent = 1001,
        ClientTrustAccount = 1002,
        MoneyMarket = 1003,
        RentsHeldInTrust = 1004,

        Bank = 1100,
        Saving = 1101,

        AccountReceivable = 1200,

        Inventory = 1300,

        AssetAvailableForSale = 1400,
        DevelopmentCost = 1401,
        EmployeeCashAdvance = 1402,
        InvestmentOther = 1403,
        LoansToOfficer = 1404,
        LoansToOther = 1405,
        LoansToShareholder = 1406,
        PrepaidExpense = 1407,
        Retainage = 1408,
        UndepositedFund = 1409,
        AllowanceForBadDebt = 1410,
        OtherCurrentAsset = 1411,

        Building = 1500,
        Land = 1501,
        Vehicle = 1502,
        MachinaryAndEquipment = 1503,
        FurnitureAndFixture = 1504,
        DepletableAsset = 1505,
        LeaseholdImprovement = 1506,
        AccumulatedDepletion = 1507,
        AccumulatedDepreciation = 1508,
        OtherFixedAsset = 1509,

        AssetHeldForSale = 1600,
        DefferedTax = 1601,
        Goodwill = 1602,
        IntangibleAsset = 1603,
        LeaseBuyout = 1604,
        Licence = 1605,
        LongTermInvestment = 1606,
        OrganazationalCost = 1607,
        SecurityDeposti = 1608,
        AccumulatedAmotization = 1609,
        OtherNoneCurrentAsset = 1610,

        AccountPayable = 2000,

        CreditCard = 2100,

        DividentPayable = 2200,
        InsurancePayable = 2201,
        LoanPayable = 2202,
        PrepaidExpensePayable = 2203,
        IncomeTaxPayable = 2204,
        SaleAndServiceTaxPayable = 2205,
        LineOfCredit = 2206,
        PayrollClearing = 2207,
        PayrollLiability = 2208,
        AccruedLiability = 2209,
        CurrentPotionOfObligationUnderFinanceLease = 2210,
        CurrentTaxLiability = 2211,
        ClientTrustAccountLiability = 2212,
        RentInTrustLiability = 2213,
        OtherCurrentLiability = 2214,

        NotePayable = 2300,
        ShareholderNotePayable = 2301,
        AccruedHolidayPayable = 2302,
        LongTermDebt = 2303,
        LiabilityRelatedToAssetHeldForSale = 2304,
        AccruedNoneCurrentLiability = 2305,
        OtherNoneCurrentLiability = 2306,

        OpeningBalanceEquity = 3000,
        OwnersEquity = 3001,
        PartnersEquity = 3002,
        PartnerContribution = 3003,
        PartnerDistribution = 3004,
        DividendDisbursed = 3005,
        ShareCapital = 3006,
        OrdinaryShare = 3007,
        PreferredShare = 3008,
        TreasuryShare = 3009,
        AccumulatedAdjustment = 3010,
        OtherComprehensiveIncome = 3011,
        PaidInCapitalOrSurplus = 3012,
        RetainedEarning = 3013,
        EquityInEarningOfSubsidiaries = 3014,

        SaleOfProductIncome = 4000,
        ServiceFeeIncome = 4001,
        SaleRetail = 4002,
        SaleWholesale = 4003,
        GeneralRevenue = 4004,
        OtherPrimaryIncome = 4005,
        NonProfitIncome = 4006,
        DiscountOrRefundGiven = 4007,
        UnappliedCashPaymentIncome = 4008,

        DividendIncome = 4100,
        InterestEarned = 4101,
        OtherInvestmentIncome = 4102,
        OtherOperatingIncome = 4103,
        OtherMiscellaneousIncome = 4104,
        TaxExemptInterest = 4105,
        LossOnDisposalOfAsset = 4106,
        UnrealisedLossOnSecurityNetOfTax = 4107,

        LaborCost = 5000,
        SuppliesAndMaterialsCost = 5001,
        EquipmentRentalCost = 5002,
        FreightAndDeliveryCost = 5003,
        OtherCostOfSale = 5004,


        AdministrativeExpense = 5100,
        AdvertisingPromotional = 5101,
        AmortizationExpense = 5102,
        Auto = 5103,
        BadDebt = 5104,
        BankCharge = 5105,
        CharitableContribution = 5106,
        CommisionAndFee = 5107,
        LaborExpense = 5108,
        DueAndSubscription = 5109,
        EquipmentRental = 5110,
        FinanceCost = 5111,
        IncomeTaxExpense = 5112,
        Insurance = 5113,
        InterestPaid = 5114,
        LegalAndProfessionalFee = 5115,
        LossOnDiscontinuedOperationNetOfTax = 5116,
        ManagementCompensation = 5117,
        MealAndEntertai = 5118,
        OtherMiscelleneousServiceCost = 5119,
        OtherSellingExpense = 5120,
        PayrollExpense = 5121,
        RentOrLeaseOfBuilding = 5122,
        RepairAndMaintenance = 5123,
        ShipingAndDeliveryExpense = 5124,
        SuppliesAndMaterials = 5125,
        TaxPaid = 5126,
        TravelGeneralAndAdminExpense = 5127,
        TravelSellExpense = 5128,
        UnappliedCashBillPaymentExpense = 5129,
        Utilities = 5130,

        Amortization = 5200,
        Depreciation = 5201,
        ExchangeLossGain = 5202,
        PenaltiesAndSettlements = 5203,
        OtherExpense = 5204
    }

}
