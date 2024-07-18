using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageVault.RequestMetricsService.Migrations
{
    /// <inheritdoc />
    public partial class RequeststableChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "Requests");
        }
    }
}
