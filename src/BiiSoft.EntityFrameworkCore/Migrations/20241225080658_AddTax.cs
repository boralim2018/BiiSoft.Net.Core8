using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class AddTax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiiTaxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rate = table.Column<decimal>(type: "numeric", nullable: false),
                    PurchaseAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    SaleAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CannotEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CannotDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiTaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiTaxes_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiTaxes_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiTaxes_BiiChartOfAccounts_PurchaseAccountId",
                        column: x => x.PurchaseAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiTaxes_BiiChartOfAccounts_SaleAccountId",
                        column: x => x.SaleAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiiTaxes_CreatorUserId",
                table: "BiiTaxes",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiTaxes_DisplayName",
                table: "BiiTaxes",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiTaxes_LastModifierUserId",
                table: "BiiTaxes",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiTaxes_Name",
                table: "BiiTaxes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiTaxes_PurchaseAccountId",
                table: "BiiTaxes",
                column: "PurchaseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiTaxes_SaleAccountId",
                table: "BiiTaxes",
                column: "SaleAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiiTaxes");
        }
    }
}
