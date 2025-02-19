using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompanyAndItemFieldSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BiiItems_BiiTaxes_PurchaseTaxId",
                table: "BiiItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiItems_BiiTaxes_SaleTaxId",
                table: "BiiItems");

            migrationBuilder.DropTable(
                name: "BiiItemGalleries");

            migrationBuilder.DropIndex(
                name: "IX_BiiItems_PurchaseTaxId",
                table: "BiiItems");

            migrationBuilder.DropIndex(
                name: "IX_BiiItems_SaleTaxId",
                table: "BiiItems");

            migrationBuilder.DropColumn(
                name: "PurchaseTaxId",
                table: "BiiItems");

            migrationBuilder.DropColumn(
                name: "SaleTaxId",
                table: "BiiItems");

            migrationBuilder.DropColumn(
                name: "FieldALabel",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "FieldBLabel",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "FieldCLabel",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseBattery",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseBrand",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseCPU",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseCamera",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseColorPattern",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseFieldA",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseFieldB",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseFieldC",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseGrade",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseHDD",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseItemGroup",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseModel",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseRAM",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseScreen",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseSeries",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseSize",
                table: "BiiItemFieldSettings");

            migrationBuilder.DropColumn(
                name: "UseVGA",
                table: "BiiItemFieldSettings");

            migrationBuilder.RenameColumn(
                name: "CodeFormalaEnable",
                table: "BiiItemSettings",
                newName: "WidthRequired");

            migrationBuilder.AddColumn<bool>(
                name: "AreaRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BatchNoRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BatteryRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BrandRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CPURequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CameraRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ColorPatternRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DiameterRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ExpiredRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FieldALabel",
                table: "BiiItemSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FieldARequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FieldBLabel",
                table: "BiiItemSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FieldBRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FieldCLabel",
                table: "BiiItemSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FieldCRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GradeRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GrossWeightRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HDDRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HeightRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InventoryStatusRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ItemGroupRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LengthRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MaxStockRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MinStockRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ModelRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NetWeightRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RAMRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReorderStockRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ScreenRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SerialRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SeriesRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SizeRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseArea",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseBatchNo",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseBattery",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseBrand",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseCPU",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseCamera",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseCodeFormula",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseColorPattern",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseDiameter",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseExpired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseFieldA",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseFieldB",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseFieldC",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseGrade",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseGrossWeight",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseHDD",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseHeight",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseInventoryStatus",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseItemGroup",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseLength",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseMaxStock",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseMinStock",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseModel",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseNetWeight",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseRAM",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseReorderStock",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseScreen",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseSerial",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseSeries",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseSize",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseVGA",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseVolume",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseWidth",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VGARequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VolumeRequired",
                table: "BiiItemSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TaxEnable",
                table: "BiiCompanyAdvanceSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TaxType",
                table: "BiiCompanyAdvanceSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "BatchNoRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "BatteryRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "BrandRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "CPURequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "CameraRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "ColorPatternRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "DiameterRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "ExpiredRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "FieldALabel",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "FieldARequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "FieldBLabel",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "FieldBRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "FieldCLabel",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "FieldCRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "GradeRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "GrossWeightRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "HDDRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "HeightRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "InventoryStatusRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "ItemGroupRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "LengthRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "MaxStockRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "MinStockRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "ModelRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "NetWeightRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "RAMRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "ReorderStockRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "ScreenRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "SerialRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "SeriesRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "SizeRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseArea",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseBatchNo",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseBattery",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseBrand",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseCPU",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseCamera",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseCodeFormula",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseColorPattern",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseDiameter",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseExpired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseFieldA",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseFieldB",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseFieldC",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseGrade",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseGrossWeight",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseHDD",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseHeight",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseInventoryStatus",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseItemGroup",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseLength",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseMaxStock",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseMinStock",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseModel",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseNetWeight",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseRAM",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseReorderStock",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseScreen",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseSerial",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseSeries",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseSize",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseVGA",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseVolume",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "UseWidth",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "VGARequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "VolumeRequired",
                table: "BiiItemSettings");

            migrationBuilder.DropColumn(
                name: "TaxEnable",
                table: "BiiCompanyAdvanceSettings");

            migrationBuilder.DropColumn(
                name: "TaxType",
                table: "BiiCompanyAdvanceSettings");

            migrationBuilder.RenameColumn(
                name: "WidthRequired",
                table: "BiiItemSettings",
                newName: "CodeFormalaEnable");

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseTaxId",
                table: "BiiItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SaleTaxId",
                table: "BiiItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldALabel",
                table: "BiiItemFieldSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldBLabel",
                table: "BiiItemFieldSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldCLabel",
                table: "BiiItemFieldSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseBattery",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseBrand",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseCPU",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseCamera",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseColorPattern",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseFieldA",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseFieldB",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseFieldC",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseGrade",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseHDD",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseItemGroup",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseModel",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseRAM",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseScreen",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseSeries",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseSize",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseVGA",
                table: "BiiItemFieldSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BiiItemGalleries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    GalleryId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiItemGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemGalleries_BiiItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "BiiItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_PurchaseTaxId",
                table: "BiiItems",
                column: "PurchaseTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_SaleTaxId",
                table: "BiiItems",
                column: "SaleTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGalleries_GalleryId",
                table: "BiiItemGalleries",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGalleries_ItemId",
                table: "BiiItemGalleries",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGalleries_No",
                table: "BiiItemGalleries",
                column: "No");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiItems_BiiTaxes_PurchaseTaxId",
                table: "BiiItems",
                column: "PurchaseTaxId",
                principalTable: "BiiTaxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BiiItems_BiiTaxes_SaleTaxId",
                table: "BiiItems",
                column: "SaleTaxId",
                principalTable: "BiiTaxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
