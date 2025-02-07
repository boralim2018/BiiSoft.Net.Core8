using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiiSoft.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItems : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_PurchaseTaxId",
                table: "BiiItems",
                column: "PurchaseTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_BiiItems_SaleTaxId",
                table: "BiiItems",
                column: "SaleTaxId");

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
