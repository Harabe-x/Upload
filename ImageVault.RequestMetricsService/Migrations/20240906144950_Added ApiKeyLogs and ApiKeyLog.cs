using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageVault.RequestMetricsService.Migrations
{
    /// <inheritdoc />
    public partial class AddedApiKeyLogsandApiKeyLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastAggregationDate",
                table: "UserUsageMetrics",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApiKeyLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeyLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiKeyLogList",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApiKeysLogId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKeyLogsId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeyLogList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiKeyLogList_ApiKeyLogs_ApiKeyLogsId",
                        column: x => x.ApiKeyLogsId,
                        principalTable: "ApiKeyLogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeyLogList_ApiKeyLogsId",
                table: "ApiKeyLogList",
                column: "ApiKeyLogsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeyLogList");

            migrationBuilder.DropTable(
                name: "ApiKeyLogs");

            migrationBuilder.DropColumn(
                name: "LastAggregationDate",
                table: "UserUsageMetrics");
        }
    }
}
