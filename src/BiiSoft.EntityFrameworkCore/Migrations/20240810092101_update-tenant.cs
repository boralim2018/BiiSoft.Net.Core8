using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class updatetenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "BiiCompanyGeneralSettings");

            migrationBuilder.AddColumn<Guid>(
                name: "LogoId",
                table: "AbpTenants",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "AbpTenants");

            migrationBuilder.AddColumn<Guid>(
                name: "LogoId",
                table: "BiiCompanyGeneralSettings",
                type: "uuid",
                nullable: true);
        }
    }
}
