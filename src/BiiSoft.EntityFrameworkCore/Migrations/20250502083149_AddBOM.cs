using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class AddBOM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiiBOMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_BiiBOMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiBOMs_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiBOMs_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiBOMs_BiiItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "BiiItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiBOMItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    BOMId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Qty = table.Column<decimal>(type: "numeric", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiBOMItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiBOMItems_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiBOMItems_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiBOMItems_BiiBOMs_BOMId",
                        column: x => x.BOMId,
                        principalTable: "BiiBOMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiBOMItems_BiiItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "BiiItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMItems_BOMId",
                table: "BiiBOMItems",
                column: "BOMId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMItems_CreatorUserId",
                table: "BiiBOMItems",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMItems_ItemId",
                table: "BiiBOMItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMItems_LastModifierUserId",
                table: "BiiBOMItems",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMs_CreatorUserId",
                table: "BiiBOMs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMs_DisplayName",
                table: "BiiBOMs",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMs_IsDefault",
                table: "BiiBOMs",
                column: "IsDefault",
                filter: "\"IsDefault\"=true");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMs_ItemId",
                table: "BiiBOMs",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMs_LastModifierUserId",
                table: "BiiBOMs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMs_No",
                table: "BiiBOMs",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMs_TenantId_Name_ItemId",
                table: "BiiBOMs",
                columns: new[] { "TenantId", "Name", "ItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiBOMs_Type",
                table: "BiiBOMs",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiiBOMItems");

            migrationBuilder.DropTable(
                name: "BiiBOMs");
        }
    }
}
