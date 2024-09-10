using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageVault.RequestMetricsService.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnonymousRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Endpoint = table.Column<string>(type: "text", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    Method = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiKeyLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeyLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserUsageMetrics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TotalImageUploaded = table.Column<long>(type: "bigint", nullable: false),
                    TotalStorageUsed = table.Column<long>(type: "bigint", nullable: false),
                    TotalRequests = table.Column<long>(type: "bigint", nullable: false),
                    LastAggregationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUsageMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiKeyLogList",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApiKeysLogId = table.Column<string>(type: "text", nullable: false),
                    ApiKeyLogsId = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "UsersDailyUsageMetrics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TotalImageUploaded = table.Column<long>(type: "bigint", nullable: false),
                    TotalStorageUsed = table.Column<long>(type: "bigint", nullable: false),
                    TotalRequests = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsageMetricsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDailyUsageMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersDailyUsageMetrics_UserUsageMetrics_UsageMetricsId",
                        column: x => x.UsageMetricsId,
                        principalTable: "UserUsageMetrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DailyUsageMetricsId = table.Column<string>(type: "text", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Endpoint = table.Column<string>(type: "text", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    Method = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_UsersDailyUsageMetrics_DailyUsageMetricsId",
                        column: x => x.DailyUsageMetricsId,
                        principalTable: "UsersDailyUsageMetrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeyLogList_ApiKeyLogsId",
                table: "ApiKeyLogList",
                column: "ApiKeyLogsId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DailyUsageMetricsId",
                table: "Requests",
                column: "DailyUsageMetricsId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDailyUsageMetrics_UsageMetricsId",
                table: "UsersDailyUsageMetrics",
                column: "UsageMetricsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnonymousRequests");

            migrationBuilder.DropTable(
                name: "ApiKeyLogList");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "ApiKeyLogs");

            migrationBuilder.DropTable(
                name: "UsersDailyUsageMetrics");

            migrationBuilder.DropTable(
                name: "UserUsageMetrics");
        }
    }
}
