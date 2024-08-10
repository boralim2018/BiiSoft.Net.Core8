using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class updatesetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoundCostDigts",
                table: "BiiCompanyGeneralSettings",
                newName: "RoundCostDigits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoundCostDigits",
                table: "BiiCompanyGeneralSettings",
                newName: "RoundCostDigts");
        }
    }
}
