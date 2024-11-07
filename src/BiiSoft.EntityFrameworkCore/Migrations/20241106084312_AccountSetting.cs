using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class AccountSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiiCompanyAccountSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CustomAccountCodeEnable = table.Column<bool>(type: "boolean", nullable: false),
                    DefaultAPAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultARAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultPurchaseDiscountAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultSaleDiscountAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultInventoryPurchaseAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultBillPaymentAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultReceivePaymentAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultRetainEarningAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultExchangeLossGainAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultItemReceiptAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultItemIssueAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultItemAdjustmentAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultItemTransferAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultItemProductionAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultItemExchangeAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultCashTransferAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultCashExchangeAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiCompanyAccountSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultAPAccou~",
                        column: x => x.DefaultAPAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultARAccou~",
                        column: x => x.DefaultARAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultBillPay~",
                        column: x => x.DefaultBillPaymentAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultCashExc~",
                        column: x => x.DefaultCashExchangeAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultCashTra~",
                        column: x => x.DefaultCashTransferAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultExchang~",
                        column: x => x.DefaultExchangeLossGainAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultInvento~",
                        column: x => x.DefaultInventoryPurchaseAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultItemAdj~",
                        column: x => x.DefaultItemAdjustmentAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultItemExc~",
                        column: x => x.DefaultItemExchangeAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultItemIss~",
                        column: x => x.DefaultItemIssueAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultItemPro~",
                        column: x => x.DefaultItemProductionAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultItemRec~",
                        column: x => x.DefaultItemReceiptAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultItemTra~",
                        column: x => x.DefaultItemTransferAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultPurchas~",
                        column: x => x.DefaultPurchaseDiscountAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultReceipt~",
                        column: x => x.DefaultReceivePaymentAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultRetainE~",
                        column: x => x.DefaultRetainEarningAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiCompanyAccountSettings_BiiChartOfAccounts_DefaultSaleDis~",
                        column: x => x.DefaultSaleDiscountAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultAPAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultAPAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultARAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultARAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultBillPaymentAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultBillPaymentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultCashExchangeAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultCashExchangeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultCashTransferAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultCashTransferAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultExchangeLossGainAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultExchangeLossGainAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultInventoryPurchaseAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultInventoryPurchaseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultItemAdjustmentAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultItemAdjustmentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultItemExchangeAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultItemExchangeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultItemIssueAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultItemIssueAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultItemProductionAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultItemProductionAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultItemReceiptAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultItemReceiptAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultItemTransferAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultItemTransferAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultPurchaseDiscountAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultPurchaseDiscountAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultReceivePaymentAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultReceivePaymentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultRetainEarningAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultRetainEarningAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCompanyAccountSettings_DefaultSaleDiscountAccountId",
                table: "BiiCompanyAccountSettings",
                column: "DefaultSaleDiscountAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiiCompanyAccountSettings");
        }
    }
}
