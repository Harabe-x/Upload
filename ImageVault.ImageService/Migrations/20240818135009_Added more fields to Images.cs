using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageVault.ImageService.Migrations
{
    /// <inheritdoc />
    public partial class AddedmorefieldstoImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApiKeyId",
                table: "Images",
                newName: "ImageFormat");

            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DownloadUrl",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ImageSize",
                table: "Images",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ImageCollections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DownloadUrl",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageSize",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ImageCollections");

            migrationBuilder.RenameColumn(
                name: "ImageFormat",
                table: "Images",
                newName: "ApiKeyId");
        }
    }
}
