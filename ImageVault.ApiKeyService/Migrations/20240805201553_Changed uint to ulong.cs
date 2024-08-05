using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageVault.ApiKeyService.Migrations
{
    /// <inheritdoc />
    public partial class Changeduinttoulong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "StorageCapacity",
                table: "ApiKeys",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<decimal>(
                name: "StorageUsed",
                table: "ApiKeys",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageUsed",
                table: "ApiKeys");

            migrationBuilder.AlterColumn<long>(
                name: "StorageCapacity",
                table: "ApiKeys",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");
        }
    }
}
