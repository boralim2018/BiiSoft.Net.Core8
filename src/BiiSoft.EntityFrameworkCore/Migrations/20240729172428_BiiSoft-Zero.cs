using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class BiiSoftZero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiiFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    StorageFolder = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    FilePath = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    FileType = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    FileExtension = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    FileStorage = table.Column<int>(type: "integer", nullable: false),
                    UploadSource = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BiiLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CannotEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CannotDelete = table.Column<bool>(type: "boolean", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
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
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BiiCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryCode = table.Column<int>(type: "integer", maxLength: 3, nullable: false),
                    ISO2 = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    ISO = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    PhonePrefix = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CannotEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CannotDelete = table.Column<bool>(type: "boolean", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiCountries_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiCityProvinces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ISO = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CannotEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CannotDelete = table.Column<bool>(type: "boolean", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiCityProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiCityProvinces_BiiCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "BiiCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiKhanDistricts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityProvinceId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CannotEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CannotDelete = table.Column<bool>(type: "boolean", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiKhanDistricts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiKhanDistricts_BiiCityProvinces_CityProvinceId",
                        column: x => x.CityProvinceId,
                        principalTable: "BiiCityProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiKhanDistricts_BiiCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "BiiCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiSangkatCommunes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityProvinceId = table.Column<Guid>(type: "uuid", nullable: true),
                    KhanDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CannotEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CannotDelete = table.Column<bool>(type: "boolean", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiSangkatCommunes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiSangkatCommunes_BiiCityProvinces_CityProvinceId",
                        column: x => x.CityProvinceId,
                        principalTable: "BiiCityProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiSangkatCommunes_BiiCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "BiiCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiSangkatCommunes_BiiKhanDistricts_KhanDistrictId",
                        column: x => x.KhanDistrictId,
                        principalTable: "BiiKhanDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiVillages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityProvinceId = table.Column<Guid>(type: "uuid", nullable: true),
                    KhanDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    SangkatCommuneId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CannotEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CannotDelete = table.Column<bool>(type: "boolean", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric(19,8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiVillages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiVillages_BiiCityProvinces_CityProvinceId",
                        column: x => x.CityProvinceId,
                        principalTable: "BiiCityProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiVillages_BiiCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "BiiCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiVillages_BiiKhanDistricts_KhanDistrictId",
                        column: x => x.KhanDistrictId,
                        principalTable: "BiiKhanDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiVillages_BiiSangkatCommunes_SangkatCommuneId",
                        column: x => x.SangkatCommuneId,
                        principalTable: "BiiSangkatCommunes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityProvinceId = table.Column<Guid>(type: "uuid", nullable: true),
                    KhanDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    SangkatCommuneId = table.Column<Guid>(type: "uuid", nullable: true),
                    VillageId = table.Column<Guid>(type: "uuid", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    HouseNo = table.Column<string>(type: "text", nullable: true),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_BiiCityProvinces_CityProvinceId",
                        column: x => x.CityProvinceId,
                        principalTable: "BiiCityProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_BiiCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "BiiCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_BiiKhanDistricts_KhanDistrictId",
                        column: x => x.KhanDistrictId,
                        principalTable: "BiiKhanDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_BiiLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "BiiLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_BiiSangkatCommunes_SangkatCommuneId",
                        column: x => x.SangkatCommuneId,
                        principalTable: "BiiSangkatCommunes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_BiiVillages_VillageId",
                        column: x => x.VillageId,
                        principalTable: "BiiVillages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiBranches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    BusinessId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Email = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Website = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TaxRegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    BillingAddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    SameAsBillingAddress = table.Column<bool>(type: "boolean", nullable: false),
                    ShippingAddressId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_BiiBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiBranches_ContactAddresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "ContactAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiBranches_ContactAddresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "ContactAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiUserBranchs",
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
                    table.PrimaryKey("PK_BiiUserBranchs", x => x.Id);
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
                name: "IX_BiiBranches_BillingAddressId",
                table: "BiiBranches",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranches_No",
                table: "BiiBranches",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranches_ShippingAddressId",
                table: "BiiBranches",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranches_TenantId_DisplayName",
                table: "BiiBranches",
                columns: new[] { "TenantId", "DisplayName" });

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranches_TenantId_Name",
                table: "BiiBranches",
                columns: new[] { "TenantId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiCityProvinces_Code",
                table: "BiiCityProvinces",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiCityProvinces_CountryId",
                table: "BiiCityProvinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCityProvinces_DisplayName",
                table: "BiiCityProvinces",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCityProvinces_ISO",
                table: "BiiCityProvinces",
                column: "ISO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiCityProvinces_Name",
                table: "BiiCityProvinces",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCityProvinces_No",
                table: "BiiCityProvinces",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_Code",
                table: "BiiCountries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_CountryCode",
                table: "BiiCountries",
                column: "CountryCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_CurrencyId",
                table: "BiiCountries",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_DisplayName",
                table: "BiiCountries",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_ISO",
                table: "BiiCountries",
                column: "ISO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_ISO2",
                table: "BiiCountries",
                column: "ISO2",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_Name",
                table: "BiiCountries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_No",
                table: "BiiCountries",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_PhonePrefix",
                table: "BiiCountries",
                column: "PhonePrefix");

            migrationBuilder.CreateIndex(
                name: "IX_BiiFiles_DisplayName_TenantId",
                table: "BiiFiles",
                columns: new[] { "DisplayName", "TenantId" });

            migrationBuilder.CreateIndex(
                name: "IX_BiiFiles_FilePath_TenantId",
                table: "BiiFiles",
                columns: new[] { "FilePath", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiFiles_Name_TenantId",
                table: "BiiFiles",
                columns: new[] { "Name", "TenantId" });

            migrationBuilder.CreateIndex(
                name: "IX_BiiKhanDistricts_CityProvinceId",
                table: "BiiKhanDistricts",
                column: "CityProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiKhanDistricts_Code",
                table: "BiiKhanDistricts",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiKhanDistricts_CountryId",
                table: "BiiKhanDistricts",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiKhanDistricts_DisplayName",
                table: "BiiKhanDistricts",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiKhanDistricts_Name",
                table: "BiiKhanDistricts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiKhanDistricts_No",
                table: "BiiKhanDistricts",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiLocations_Code_TenantId",
                table: "BiiLocations",
                columns: new[] { "Code", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiLocations_DisplayName",
                table: "BiiLocations",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiLocations_Name",
                table: "BiiLocations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiLocations_No",
                table: "BiiLocations",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiSangkatCommunes_CityProvinceId",
                table: "BiiSangkatCommunes",
                column: "CityProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiSangkatCommunes_Code",
                table: "BiiSangkatCommunes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiSangkatCommunes_CountryId",
                table: "BiiSangkatCommunes",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiSangkatCommunes_DisplayName",
                table: "BiiSangkatCommunes",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiSangkatCommunes_KhanDistrictId",
                table: "BiiSangkatCommunes",
                column: "KhanDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiSangkatCommunes_Name",
                table: "BiiSangkatCommunes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiSangkatCommunes_No",
                table: "BiiSangkatCommunes",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUserBranchs_BranchId",
                table: "BiiUserBranchs",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUserBranchs_MemberId",
                table: "BiiUserBranchs",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVillages_CityProvinceId",
                table: "BiiVillages",
                column: "CityProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVillages_Code",
                table: "BiiVillages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiVillages_CountryId",
                table: "BiiVillages",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVillages_DisplayName",
                table: "BiiVillages",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVillages_KhanDistrictId",
                table: "BiiVillages",
                column: "KhanDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVillages_Name",
                table: "BiiVillages",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVillages_No",
                table: "BiiVillages",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVillages_SangkatCommuneId",
                table: "BiiVillages",
                column: "SangkatCommuneId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_CityProvinceId",
                table: "ContactAddresses",
                column: "CityProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_CountryId",
                table: "ContactAddresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_KhanDistrictId",
                table: "ContactAddresses",
                column: "KhanDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_LocationId",
                table: "ContactAddresses",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_SangkatCommuneId",
                table: "ContactAddresses",
                column: "SangkatCommuneId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_VillageId",
                table: "ContactAddresses",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_DisplayName",
                table: "Currencies",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Name",
                table: "Currencies",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiiFiles");

            migrationBuilder.DropTable(
                name: "BiiUserBranchs");

            migrationBuilder.DropTable(
                name: "BiiBranches");

            migrationBuilder.DropTable(
                name: "ContactAddresses");

            migrationBuilder.DropTable(
                name: "BiiLocations");

            migrationBuilder.DropTable(
                name: "BiiVillages");

            migrationBuilder.DropTable(
                name: "BiiSangkatCommunes");

            migrationBuilder.DropTable(
                name: "BiiKhanDistricts");

            migrationBuilder.DropTable(
                name: "BiiCityProvinces");

            migrationBuilder.DropTable(
                name: "BiiCountries");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
