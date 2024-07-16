using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageVault.UserService.Migrations
{
    /// <inheritdoc />
    public partial class ApiKeysTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyCapacity",
                table: "ApiKey",
                newName: "KeyStorageCapacity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyStorageCapacity",
                table: "ApiKey",
                newName: "KeyCapacity");
        }
    }
}
