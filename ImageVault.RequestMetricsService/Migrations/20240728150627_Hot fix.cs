using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageVault.RequestMetricsService.Migrations
{
    /// <inheritdoc />
    public partial class Hotfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StorageUsed",
                table: "ApiKeyResourcesUsageMetrics",
                newName: "StorageUsedGb");

            migrationBuilder.RenameColumn(
                name: "StorageAvailable",
                table: "ApiKeyResourcesUsageMetrics",
                newName: "StorageAvailableGb");

            migrationBuilder.RenameColumn(
                name: "ApiKeyId",
                table: "ApiKeyResourcesUsageMetrics",
                newName: "ApiKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StorageUsedGb",
                table: "ApiKeyResourcesUsageMetrics",
                newName: "StorageUsed");

            migrationBuilder.RenameColumn(
                name: "StorageAvailableGb",
                table: "ApiKeyResourcesUsageMetrics",
                newName: "StorageAvailable");

            migrationBuilder.RenameColumn(
                name: "ApiKey",
                table: "ApiKeyResourcesUsageMetrics",
                newName: "ApiKeyId");
        }
    }
}
