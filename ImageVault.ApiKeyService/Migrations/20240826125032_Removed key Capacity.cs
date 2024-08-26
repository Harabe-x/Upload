using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageVault.ApiKeyService.Migrations
{
    /// <inheritdoc />
    public partial class RemovedkeyCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageCapacity",
                table: "ApiKeys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StorageCapacity",
                table: "ApiKeys",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
