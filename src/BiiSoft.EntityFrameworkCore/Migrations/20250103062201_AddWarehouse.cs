using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiiUserBranchs");

            migrationBuilder.AddColumn<int>(
                name: "Sharing",
                table: "BiiBranches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BiiBranchUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiBranchUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiBranchUsers_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiBranchUsers_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiBranchUsers_AbpUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiBranchUsers_BiiBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BiiBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiItemCodeFormulas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    ItemTypes = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Prefix = table.Column<string>(type: "text", nullable: true),
                    Digits = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiItemCodeFormulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemCodeFormulas_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemCodeFormulas_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CodeFormalaEnable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiItemSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemSettings_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemSettings_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Sharing = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiWarehouses_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiWarehouses_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiWarehouseBranchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiWarehouseBranchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiWarehouseBranchs_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiWarehouseBranchs_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiWarehouseBranchs_BiiBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BiiBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiWarehouseBranchs_BiiWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "BiiWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiZones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiZones_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiZones_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiZones_BiiWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "BiiWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiItemZones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiItemZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemZones_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemZones_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemZones_BiiItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "BiiItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItemZones_BiiZones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "BiiZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranches_Sharing",
                table: "BiiBranches",
                column: "Sharing");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranchUsers_BranchId",
                table: "BiiBranchUsers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranchUsers_CreatorUserId",
                table: "BiiBranchUsers",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranchUsers_LastModifierUserId",
                table: "BiiBranchUsers",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranchUsers_MemberId",
                table: "BiiBranchUsers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemCodeFormulas_CreatorUserId",
                table: "BiiItemCodeFormulas",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemCodeFormulas_LastModifierUserId",
                table: "BiiItemCodeFormulas",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSettings_CreatorUserId",
                table: "BiiItemSettings",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSettings_LastModifierUserId",
                table: "BiiItemSettings",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemZones_CreatorUserId",
                table: "BiiItemZones",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemZones_ItemId",
                table: "BiiItemZones",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemZones_LastModifierUserId",
                table: "BiiItemZones",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemZones_ZoneId",
                table: "BiiItemZones",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouseBranchs_BranchId",
                table: "BiiWarehouseBranchs",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouseBranchs_CreatorUserId",
                table: "BiiWarehouseBranchs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouseBranchs_LastModifierUserId",
                table: "BiiWarehouseBranchs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouseBranchs_WarehouseId",
                table: "BiiWarehouseBranchs",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouses_Code",
                table: "BiiWarehouses",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouses_CreatorUserId",
                table: "BiiWarehouses",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouses_DisplayName",
                table: "BiiWarehouses",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouses_LastModifierUserId",
                table: "BiiWarehouses",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouses_Name",
                table: "BiiWarehouses",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouses_No",
                table: "BiiWarehouses",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiWarehouses_Sharing",
                table: "BiiWarehouses",
                column: "Sharing");

            migrationBuilder.CreateIndex(
                name: "IX_BiiZones_CreatorUserId",
                table: "BiiZones",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiZones_DisplayName",
                table: "BiiZones",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiZones_LastModifierUserId",
                table: "BiiZones",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiZones_Name",
                table: "BiiZones",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiZones_No",
                table: "BiiZones",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiZones_WarehouseId",
                table: "BiiZones",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiiBranchUsers");

            migrationBuilder.DropTable(
                name: "BiiItemCodeFormulas");

            migrationBuilder.DropTable(
                name: "BiiItemSettings");

            migrationBuilder.DropTable(
                name: "BiiItemZones");

            migrationBuilder.DropTable(
                name: "BiiWarehouseBranchs");

            migrationBuilder.DropTable(
                name: "BiiZones");

            migrationBuilder.DropTable(
                name: "BiiWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_BiiBranches_Sharing",
                table: "BiiBranches");

            migrationBuilder.DropColumn(
                name: "Sharing",
                table: "BiiBranches");

            migrationBuilder.CreateTable(
                name: "BiiUserBranchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiUserBranchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiUserBranchs_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiUserBranchs_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiUserBranchs_AbpUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiUserBranchs_BiiBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BiiBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiiUserBranchs_BranchId",
                table: "BiiUserBranchs",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUserBranchs_CreatorUserId",
                table: "BiiUserBranchs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUserBranchs_LastModifierUserId",
                table: "BiiUserBranchs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUserBranchs_MemberId",
                table: "BiiUserBranchs",
                column: "MemberId");
        }
    }
}
