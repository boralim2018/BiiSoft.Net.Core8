using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class AddItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiiBatteries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiBatteries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiBatteries_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiBatteries_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiCameras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiCameras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiCameras_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiCameras_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiColorPatterns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiColorPatterns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiColorPatterns_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiColorPatterns_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiCPUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiCPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiCPUs_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiCPUs_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiHDDs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiHDDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiHDDs_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiHDDs_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemBrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiItemBrands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemBrands_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemBrands_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemFieldAs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiItemFieldAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemFieldAs_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemFieldAs_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemFieldBs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiItemFieldBs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemFieldBs_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemFieldBs_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemFieldCs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiItemFieldCs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemFieldCs_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemFieldCs_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemFieldSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    UseBrand = table.Column<bool>(type: "boolean", nullable: false),
                    UseModel = table.Column<bool>(type: "boolean", nullable: false),
                    UseSeries = table.Column<bool>(type: "boolean", nullable: false),
                    UseColorPattern = table.Column<bool>(type: "boolean", nullable: false),
                    UseSize = table.Column<bool>(type: "boolean", nullable: false),
                    UseGrade = table.Column<bool>(type: "boolean", nullable: false),
                    UseCPU = table.Column<bool>(type: "boolean", nullable: false),
                    UseRAM = table.Column<bool>(type: "boolean", nullable: false),
                    UseVGA = table.Column<bool>(type: "boolean", nullable: false),
                    UseCamera = table.Column<bool>(type: "boolean", nullable: false),
                    UseScreen = table.Column<bool>(type: "boolean", nullable: false),
                    UseStorage = table.Column<bool>(type: "boolean", nullable: false),
                    UseBattery = table.Column<bool>(type: "boolean", nullable: false),
                    UseOS = table.Column<bool>(type: "boolean", nullable: false),
                    UseFieldA = table.Column<bool>(type: "boolean", nullable: false),
                    UseFieldB = table.Column<bool>(type: "boolean", nullable: false),
                    UseFieldC = table.Column<bool>(type: "boolean", nullable: false),
                    FieldALabel = table.Column<string>(type: "text", nullable: true),
                    FieldBLabel = table.Column<string>(type: "text", nullable: true),
                    FieldCLabel = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiiItemFieldSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemFieldSettings_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemFieldSettings_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemGrades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiItemGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemGrades_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemGrades_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiItemGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemGroups_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemGroups_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiItemModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemModels_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemModels_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiItemSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemSeries_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemSeries_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItemSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiItemSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItemSizes_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItemSizes_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiRAMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiRAMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiRAMs_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiRAMs_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiScreens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiScreens_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiScreens_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiUnits_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiUnits_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiVGAs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_BiiVGAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiVGAs_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiVGAs_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BiiItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemType = table.Column<int>(type: "integer", nullable: false),
                    ItemCategory = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    NetWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    GrossWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    Length = table.Column<decimal>(type: "numeric", nullable: false),
                    Width = table.Column<decimal>(type: "numeric", nullable: false),
                    Height = table.Column<decimal>(type: "numeric", nullable: false),
                    Diameter = table.Column<decimal>(type: "numeric", nullable: false),
                    Area = table.Column<decimal>(type: "numeric", nullable: false),
                    Volume = table.Column<decimal>(type: "numeric", nullable: false),
                    WeightUnit = table.Column<int>(type: "integer", nullable: false),
                    LengthUnit = table.Column<int>(type: "integer", nullable: false),
                    AreaUnit = table.Column<int>(type: "integer", nullable: false),
                    VolumeUnit = table.Column<int>(type: "integer", nullable: false),
                    TrackSerial = table.Column<bool>(type: "boolean", nullable: false),
                    TrackExpired = table.Column<bool>(type: "boolean", nullable: false),
                    TrackBatchNo = table.Column<bool>(type: "boolean", nullable: false),
                    TrackInventoryStatus = table.Column<bool>(type: "boolean", nullable: false),
                    ReorderStock = table.Column<decimal>(type: "numeric", nullable: false),
                    MinStock = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxStock = table.Column<decimal>(type: "numeric", nullable: false),
                    ItemGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemBrandId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemGradeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemSizeId = table.Column<Guid>(type: "uuid", nullable: true),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    ColorPatternId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemSeriesId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemModelId = table.Column<Guid>(type: "uuid", nullable: true),
                    CPUId = table.Column<Guid>(type: "uuid", nullable: true),
                    RAMId = table.Column<Guid>(type: "uuid", nullable: true),
                    VGAId = table.Column<Guid>(type: "uuid", nullable: true),
                    HDDId = table.Column<Guid>(type: "uuid", nullable: true),
                    BatteryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CameraId = table.Column<Guid>(type: "uuid", nullable: true),
                    ScreenId = table.Column<Guid>(type: "uuid", nullable: true),
                    FieldAId = table.Column<Guid>(type: "uuid", nullable: true),
                    FieldBId = table.Column<Guid>(type: "uuid", nullable: true),
                    FieldCId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true),
                    PurchaseTaxId = table.Column<Guid>(type: "uuid", nullable: true),
                    SaleTaxId = table.Column<Guid>(type: "uuid", nullable: true),
                    PurchaseAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    SaleAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    InventoryAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
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
                    table.PrimaryKey("PK_BiiItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiiItems_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItems_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiBatteries_BatteryId",
                        column: x => x.BatteryId,
                        principalTable: "BiiBatteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiCPUs_CPUId",
                        column: x => x.CPUId,
                        principalTable: "BiiCPUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiCameras_CameraId",
                        column: x => x.CameraId,
                        principalTable: "BiiCameras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiChartOfAccounts_InventoryAccountId",
                        column: x => x.InventoryAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiChartOfAccounts_PurchaseAccountId",
                        column: x => x.PurchaseAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiChartOfAccounts_SaleAccountId",
                        column: x => x.SaleAccountId,
                        principalTable: "BiiChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiColorPatterns_ColorPatternId",
                        column: x => x.ColorPatternId,
                        principalTable: "BiiColorPatterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiHDDs_HDDId",
                        column: x => x.HDDId,
                        principalTable: "BiiHDDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiItemBrands_ItemBrandId",
                        column: x => x.ItemBrandId,
                        principalTable: "BiiItemBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiItemFieldAs_FieldAId",
                        column: x => x.FieldAId,
                        principalTable: "BiiItemFieldAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiItemFieldBs_FieldBId",
                        column: x => x.FieldBId,
                        principalTable: "BiiItemFieldBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiItemFieldCs_FieldCId",
                        column: x => x.FieldCId,
                        principalTable: "BiiItemFieldCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiItemGrades_ItemGradeId",
                        column: x => x.ItemGradeId,
                        principalTable: "BiiItemGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiItemGroups_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "BiiItemGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiItemModels_ItemModelId",
                        column: x => x.ItemModelId,
                        principalTable: "BiiItemModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiItemSeries_ItemSeriesId",
                        column: x => x.ItemSeriesId,
                        principalTable: "BiiItemSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiItemSizes_ItemSizeId",
                        column: x => x.ItemSizeId,
                        principalTable: "BiiItemSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiRAMs_RAMId",
                        column: x => x.RAMId,
                        principalTable: "BiiRAMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiScreens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "BiiScreens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiTaxes_PurchaseTaxId",
                        column: x => x.PurchaseTaxId,
                        principalTable: "BiiTaxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiTaxes_SaleTaxId",
                        column: x => x.SaleTaxId,
                        principalTable: "BiiTaxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "BiiUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiiItems_BiiVGAs_VGAId",
                        column: x => x.VGAId,
                        principalTable: "BiiVGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiiItemGalleries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    GalleryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
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
                name: "IX_BiiBatteries_CreatorUserId",
                table: "BiiBatteries",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBatteries_DisplayName",
                table: "BiiBatteries",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBatteries_LastModifierUserId",
                table: "BiiBatteries",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBatteries_Name",
                table: "BiiBatteries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBatteries_No",
                table: "BiiBatteries",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCameras_CreatorUserId",
                table: "BiiCameras",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCameras_DisplayName",
                table: "BiiCameras",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCameras_LastModifierUserId",
                table: "BiiCameras",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCameras_Name",
                table: "BiiCameras",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCameras_No",
                table: "BiiCameras",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiColorPatterns_CreatorUserId",
                table: "BiiColorPatterns",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiColorPatterns_DisplayName",
                table: "BiiColorPatterns",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiColorPatterns_LastModifierUserId",
                table: "BiiColorPatterns",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiColorPatterns_Name",
                table: "BiiColorPatterns",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiColorPatterns_No",
                table: "BiiColorPatterns",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCPUs_CreatorUserId",
                table: "BiiCPUs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCPUs_DisplayName",
                table: "BiiCPUs",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCPUs_LastModifierUserId",
                table: "BiiCPUs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCPUs_Name",
                table: "BiiCPUs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCPUs_No",
                table: "BiiCPUs",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiHDDs_CreatorUserId",
                table: "BiiHDDs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiHDDs_DisplayName",
                table: "BiiHDDs",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiHDDs_LastModifierUserId",
                table: "BiiHDDs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiHDDs_Name",
                table: "BiiHDDs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiHDDs_No",
                table: "BiiHDDs",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemBrands_CreatorUserId",
                table: "BiiItemBrands",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemBrands_DisplayName",
                table: "BiiItemBrands",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemBrands_LastModifierUserId",
                table: "BiiItemBrands",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemBrands_Name",
                table: "BiiItemBrands",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemBrands_No",
                table: "BiiItemBrands",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldAs_CreatorUserId",
                table: "BiiItemFieldAs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldAs_DisplayName",
                table: "BiiItemFieldAs",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldAs_LastModifierUserId",
                table: "BiiItemFieldAs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldAs_Name",
                table: "BiiItemFieldAs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldAs_No",
                table: "BiiItemFieldAs",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldBs_CreatorUserId",
                table: "BiiItemFieldBs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldBs_DisplayName",
                table: "BiiItemFieldBs",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldBs_LastModifierUserId",
                table: "BiiItemFieldBs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldBs_Name",
                table: "BiiItemFieldBs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldBs_No",
                table: "BiiItemFieldBs",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldCs_CreatorUserId",
                table: "BiiItemFieldCs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldCs_DisplayName",
                table: "BiiItemFieldCs",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldCs_LastModifierUserId",
                table: "BiiItemFieldCs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldCs_Name",
                table: "BiiItemFieldCs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldCs_No",
                table: "BiiItemFieldCs",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldSettings_CreatorUserId",
                table: "BiiItemFieldSettings",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemFieldSettings_LastModifierUserId",
                table: "BiiItemFieldSettings",
                column: "LastModifierUserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGrades_CreatorUserId",
                table: "BiiItemGrades",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGrades_DisplayName",
                table: "BiiItemGrades",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGrades_LastModifierUserId",
                table: "BiiItemGrades",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGrades_Name",
                table: "BiiItemGrades",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGrades_No",
                table: "BiiItemGrades",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGroups_CreatorUserId",
                table: "BiiItemGroups",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGroups_DisplayName",
                table: "BiiItemGroups",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGroups_LastModifierUserId",
                table: "BiiItemGroups",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGroups_Name",
                table: "BiiItemGroups",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemGroups_No",
                table: "BiiItemGroups",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemModels_CreatorUserId",
                table: "BiiItemModels",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemModels_DisplayName",
                table: "BiiItemModels",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemModels_LastModifierUserId",
                table: "BiiItemModels",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemModels_Name",
                table: "BiiItemModels",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemModels_No",
                table: "BiiItemModels",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_BatteryId",
                table: "BiiItems",
                column: "BatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_CameraId",
                table: "BiiItems",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_ColorPatternId",
                table: "BiiItems",
                column: "ColorPatternId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_CPUId",
                table: "BiiItems",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_CreatorUserId",
                table: "BiiItems",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_DisplayName",
                table: "BiiItems",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_FieldAId",
                table: "BiiItems",
                column: "FieldAId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_FieldBId",
                table: "BiiItems",
                column: "FieldBId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_FieldCId",
                table: "BiiItems",
                column: "FieldCId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_HDDId",
                table: "BiiItems",
                column: "HDDId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_InventoryAccountId",
                table: "BiiItems",
                column: "InventoryAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_ItemBrandId",
                table: "BiiItems",
                column: "ItemBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_ItemCategory",
                table: "BiiItems",
                column: "ItemCategory");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_ItemGradeId",
                table: "BiiItems",
                column: "ItemGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_ItemGroupId",
                table: "BiiItems",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_ItemModelId",
                table: "BiiItems",
                column: "ItemModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_ItemSeriesId",
                table: "BiiItems",
                column: "ItemSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_ItemSizeId",
                table: "BiiItems",
                column: "ItemSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_ItemType",
                table: "BiiItems",
                column: "ItemType");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_LastModifierUserId",
                table: "BiiItems",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_Name",
                table: "BiiItems",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_No",
                table: "BiiItems",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_PurchaseAccountId",
                table: "BiiItems",
                column: "PurchaseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_PurchaseTaxId",
                table: "BiiItems",
                column: "PurchaseTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_RAMId",
                table: "BiiItems",
                column: "RAMId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_SaleAccountId",
                table: "BiiItems",
                column: "SaleAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_SaleTaxId",
                table: "BiiItems",
                column: "SaleTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_ScreenId",
                table: "BiiItems",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_UnitId",
                table: "BiiItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_VGAId",
                table: "BiiItems",
                column: "VGAId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSeries_CreatorUserId",
                table: "BiiItemSeries",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSeries_DisplayName",
                table: "BiiItemSeries",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSeries_LastModifierUserId",
                table: "BiiItemSeries",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSeries_Name",
                table: "BiiItemSeries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSeries_No",
                table: "BiiItemSeries",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSizes_CreatorUserId",
                table: "BiiItemSizes",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSizes_DisplayName",
                table: "BiiItemSizes",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSizes_LastModifierUserId",
                table: "BiiItemSizes",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSizes_Name",
                table: "BiiItemSizes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItemSizes_No",
                table: "BiiItemSizes",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiRAMs_CreatorUserId",
                table: "BiiRAMs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiRAMs_DisplayName",
                table: "BiiRAMs",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiRAMs_LastModifierUserId",
                table: "BiiRAMs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiRAMs_Name",
                table: "BiiRAMs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiRAMs_No",
                table: "BiiRAMs",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiScreens_CreatorUserId",
                table: "BiiScreens",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiScreens_DisplayName",
                table: "BiiScreens",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiScreens_LastModifierUserId",
                table: "BiiScreens",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiScreens_Name",
                table: "BiiScreens",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiScreens_No",
                table: "BiiScreens",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUnits_CreatorUserId",
                table: "BiiUnits",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUnits_DisplayName",
                table: "BiiUnits",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUnits_LastModifierUserId",
                table: "BiiUnits",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUnits_Name",
                table: "BiiUnits",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUnits_No",
                table: "BiiUnits",
                column: "No");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVGAs_CreatorUserId",
                table: "BiiVGAs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVGAs_DisplayName",
                table: "BiiVGAs",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVGAs_LastModifierUserId",
                table: "BiiVGAs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVGAs_Name",
                table: "BiiVGAs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVGAs_No",
                table: "BiiVGAs",
                column: "No");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiiItemFieldSettings");

            migrationBuilder.DropTable(
                name: "BiiItemGalleries");

            migrationBuilder.DropTable(
                name: "BiiItems");

            migrationBuilder.DropTable(
                name: "BiiBatteries");

            migrationBuilder.DropTable(
                name: "BiiCPUs");

            migrationBuilder.DropTable(
                name: "BiiCameras");

            migrationBuilder.DropTable(
                name: "BiiColorPatterns");

            migrationBuilder.DropTable(
                name: "BiiHDDs");

            migrationBuilder.DropTable(
                name: "BiiItemBrands");

            migrationBuilder.DropTable(
                name: "BiiItemFieldAs");

            migrationBuilder.DropTable(
                name: "BiiItemFieldBs");

            migrationBuilder.DropTable(
                name: "BiiItemFieldCs");

            migrationBuilder.DropTable(
                name: "BiiItemGrades");

            migrationBuilder.DropTable(
                name: "BiiItemGroups");

            migrationBuilder.DropTable(
                name: "BiiItemModels");

            migrationBuilder.DropTable(
                name: "BiiItemSeries");

            migrationBuilder.DropTable(
                name: "BiiItemSizes");

            migrationBuilder.DropTable(
                name: "BiiRAMs");

            migrationBuilder.DropTable(
                name: "BiiScreens");

            migrationBuilder.DropTable(
                name: "BiiUnits");

            migrationBuilder.DropTable(
                name: "BiiVGAs");
        }
    }
}
