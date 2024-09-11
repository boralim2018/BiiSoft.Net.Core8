using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactAddressLevel",
                table: "BiiCompanyAdvanceSettings");

            migrationBuilder.AddColumn<int>(
                name: "ContactAddressLevel",
                table: "BiiCompanyGeneralSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_CreatorUserId",
                table: "Currencies",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_LastModifierUserId",
                table: "Currencies",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVillages_CreatorUserId",
                table: "BiiVillages",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiVillages_LastModifierUserId",
                table: "BiiVillages",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUserBranchs_CreatorUserId",
                table: "BiiUserBranchs",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiUserBranchs_LastModifierUserId",
                table: "BiiUserBranchs",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiSangkatCommunes_CreatorUserId",
                table: "BiiSangkatCommunes",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiSangkatCommunes_LastModifierUserId",
                table: "BiiSangkatCommunes",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiLocations_CreatorUserId",
                table: "BiiLocations",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiLocations_LastModifierUserId",
                table: "BiiLocations",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiKhanDistricts_CreatorUserId",
                table: "BiiKhanDistricts",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiKhanDistricts_LastModifierUserId",
                table: "BiiKhanDistricts",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiFiles_CreatorUserId",
                table: "BiiFiles",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiFiles_LastModifierUserId",
                table: "BiiFiles",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_CreatorUserId",
                table: "BiiCountries",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCountries_LastModifierUserId",
                table: "BiiCountries",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCityProvinces_CreatorUserId",
                table: "BiiCityProvinces",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiCityProvinces_LastModifierUserId",
                table: "BiiCityProvinces",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranches_CreatorUserId",
                table: "BiiBranches",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiBranches_LastModifierUserId",
                table: "BiiBranches",
                column: "LastModifierUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiBranches_AbpUsers_CreatorUserId",
                table: "BiiBranches",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiBranches_AbpUsers_LastModifierUserId",
                table: "BiiBranches",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiCityProvinces_AbpUsers_CreatorUserId",
                table: "BiiCityProvinces",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiCityProvinces_AbpUsers_LastModifierUserId",
                table: "BiiCityProvinces",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiCountries_AbpUsers_CreatorUserId",
                table: "BiiCountries",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiCountries_AbpUsers_LastModifierUserId",
                table: "BiiCountries",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiFiles_AbpUsers_CreatorUserId",
                table: "BiiFiles",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiFiles_AbpUsers_LastModifierUserId",
                table: "BiiFiles",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiKhanDistricts_AbpUsers_CreatorUserId",
                table: "BiiKhanDistricts",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiKhanDistricts_AbpUsers_LastModifierUserId",
                table: "BiiKhanDistricts",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiLocations_AbpUsers_CreatorUserId",
                table: "BiiLocations",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiLocations_AbpUsers_LastModifierUserId",
                table: "BiiLocations",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiSangkatCommunes_AbpUsers_CreatorUserId",
                table: "BiiSangkatCommunes",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiSangkatCommunes_AbpUsers_LastModifierUserId",
                table: "BiiSangkatCommunes",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiUserBranchs_AbpUsers_CreatorUserId",
                table: "BiiUserBranchs",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiUserBranchs_AbpUsers_LastModifierUserId",
                table: "BiiUserBranchs",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiVillages_AbpUsers_CreatorUserId",
                table: "BiiVillages",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BiiVillages_AbpUsers_LastModifierUserId",
                table: "BiiVillages",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_AbpUsers_CreatorUserId",
                table: "Currencies",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_AbpUsers_LastModifierUserId",
                table: "Currencies",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BiiBranches_AbpUsers_CreatorUserId",
                table: "BiiBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiBranches_AbpUsers_LastModifierUserId",
                table: "BiiBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiCityProvinces_AbpUsers_CreatorUserId",
                table: "BiiCityProvinces");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiCityProvinces_AbpUsers_LastModifierUserId",
                table: "BiiCityProvinces");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiCountries_AbpUsers_CreatorUserId",
                table: "BiiCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiCountries_AbpUsers_LastModifierUserId",
                table: "BiiCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiFiles_AbpUsers_CreatorUserId",
                table: "BiiFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiFiles_AbpUsers_LastModifierUserId",
                table: "BiiFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiKhanDistricts_AbpUsers_CreatorUserId",
                table: "BiiKhanDistricts");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiKhanDistricts_AbpUsers_LastModifierUserId",
                table: "BiiKhanDistricts");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiLocations_AbpUsers_CreatorUserId",
                table: "BiiLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiLocations_AbpUsers_LastModifierUserId",
                table: "BiiLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiSangkatCommunes_AbpUsers_CreatorUserId",
                table: "BiiSangkatCommunes");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiSangkatCommunes_AbpUsers_LastModifierUserId",
                table: "BiiSangkatCommunes");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiUserBranchs_AbpUsers_CreatorUserId",
                table: "BiiUserBranchs");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiUserBranchs_AbpUsers_LastModifierUserId",
                table: "BiiUserBranchs");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiVillages_AbpUsers_CreatorUserId",
                table: "BiiVillages");

            migrationBuilder.DropForeignKey(
                name: "FK_BiiVillages_AbpUsers_LastModifierUserId",
                table: "BiiVillages");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_AbpUsers_CreatorUserId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_AbpUsers_LastModifierUserId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_CreatorUserId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_LastModifierUserId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_BiiVillages_CreatorUserId",
                table: "BiiVillages");

            migrationBuilder.DropIndex(
                name: "IX_BiiVillages_LastModifierUserId",
                table: "BiiVillages");

            migrationBuilder.DropIndex(
                name: "IX_BiiUserBranchs_CreatorUserId",
                table: "BiiUserBranchs");

            migrationBuilder.DropIndex(
                name: "IX_BiiUserBranchs_LastModifierUserId",
                table: "BiiUserBranchs");

            migrationBuilder.DropIndex(
                name: "IX_BiiSangkatCommunes_CreatorUserId",
                table: "BiiSangkatCommunes");

            migrationBuilder.DropIndex(
                name: "IX_BiiSangkatCommunes_LastModifierUserId",
                table: "BiiSangkatCommunes");

            migrationBuilder.DropIndex(
                name: "IX_BiiLocations_CreatorUserId",
                table: "BiiLocations");

            migrationBuilder.DropIndex(
                name: "IX_BiiLocations_LastModifierUserId",
                table: "BiiLocations");

            migrationBuilder.DropIndex(
                name: "IX_BiiKhanDistricts_CreatorUserId",
                table: "BiiKhanDistricts");

            migrationBuilder.DropIndex(
                name: "IX_BiiKhanDistricts_LastModifierUserId",
                table: "BiiKhanDistricts");

            migrationBuilder.DropIndex(
                name: "IX_BiiFiles_CreatorUserId",
                table: "BiiFiles");

            migrationBuilder.DropIndex(
                name: "IX_BiiFiles_LastModifierUserId",
                table: "BiiFiles");

            migrationBuilder.DropIndex(
                name: "IX_BiiCountries_CreatorUserId",
                table: "BiiCountries");

            migrationBuilder.DropIndex(
                name: "IX_BiiCountries_LastModifierUserId",
                table: "BiiCountries");

            migrationBuilder.DropIndex(
                name: "IX_BiiCityProvinces_CreatorUserId",
                table: "BiiCityProvinces");

            migrationBuilder.DropIndex(
                name: "IX_BiiCityProvinces_LastModifierUserId",
                table: "BiiCityProvinces");

            migrationBuilder.DropIndex(
                name: "IX_BiiBranches_CreatorUserId",
                table: "BiiBranches");

            migrationBuilder.DropIndex(
                name: "IX_BiiBranches_LastModifierUserId",
                table: "BiiBranches");

            migrationBuilder.DropColumn(
                name: "ContactAddressLevel",
                table: "BiiCompanyGeneralSettings");

            migrationBuilder.AddColumn<int>(
                name: "ContactAddressLevel",
                table: "BiiCompanyAdvanceSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
