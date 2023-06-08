using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstoqueWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductModel_SupplierModel_SuplierId",
                table: "ProductModel");

            migrationBuilder.RenameColumn(
                name: "SuplierId",
                table: "ProductModel",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductModel_SuplierId",
                table: "ProductModel",
                newName: "IX_ProductModel_SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductModel_SupplierModel_SupplierId",
                table: "ProductModel",
                column: "SupplierId",
                principalTable: "SupplierModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductModel_SupplierModel_SupplierId",
                table: "ProductModel");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "ProductModel",
                newName: "SuplierId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductModel_SupplierId",
                table: "ProductModel",
                newName: "IX_ProductModel_SuplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductModel_SupplierModel_SuplierId",
                table: "ProductModel",
                column: "SuplierId",
                principalTable: "SupplierModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
