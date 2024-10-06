using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class ChartOfAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiiChartOfAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    AccountType = table.Column<int>(type: "integer", nullable: false),
                    SubAccountType = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CannotEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CannotDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiChartOfAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiChartOfAccounts_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiChartOfAccounts_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiChartOfAccounts_BiiChartOfAccounts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiiChartOfAccounts_AccountType",
                table: "BiiChartOfAccounts",
                column: "AccountType");

            migrationBuilder.CreateIndex(
                name: "IX_BiiChartOfAccounts_Code_TenantId",
                table: "BiiChartOfAccounts",
                columns: new[] { "Code", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiChartOfAccounts_CreatorUserId",
                table: "BiiChartOfAccounts",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiChartOfAccounts_DisplayName",
                table: "BiiChartOfAccounts",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiChartOfAccounts_LastModifierUserId",
                table: "BiiChartOfAccounts",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiChartOfAccounts_Name",
                table: "BiiChartOfAccounts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiChartOfAccounts_ParentId",
                table: "BiiChartOfAccounts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiChartOfAccounts_SubAccountType",
                table: "BiiChartOfAccounts",
                column: "SubAccountType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiiChartOfAccounts");
        }
    }
}
