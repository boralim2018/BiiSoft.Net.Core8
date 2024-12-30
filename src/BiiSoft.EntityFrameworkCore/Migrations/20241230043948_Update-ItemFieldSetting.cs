using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemFieldSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseStorage",
                table: "BiiItemFieldSettings",
                newName: "UseItemGroup");

            migrationBuilder.RenameColumn(
                name: "UseOS",
                table: "BiiItemFieldSettings",
                newName: "UseHDD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseItemGroup",
                table: "BiiItemFieldSettings",
                newName: "UseStorage");

            migrationBuilder.RenameColumn(
                name: "UseHDD",
                table: "BiiItemFieldSettings",
                newName: "UseOS");
        }
    }
}
