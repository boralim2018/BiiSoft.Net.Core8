using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnBarcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "BiiItems",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_Barcode",
                table: "BiiItems",
                column: "Barcode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BiiItems_Barcode",
                table: "BiiItems");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "BiiItems");
        }
    }
}
