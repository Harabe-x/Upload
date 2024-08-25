using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageVault.ImageService.Migrations
{
    /// <inheritdoc />
    public partial class Addedrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ImageCollection_ImageCollectionId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageCollection",
                table: "ImageCollection");

            migrationBuilder.RenameTable(
                name: "ImageCollection",
                newName: "ImageCollections");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ImageCollections",
                newName: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageCollections",
                table: "ImageCollections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ImageCollections_ImageCollectionId",
                table: "Images",
                column: "ImageCollectionId",
                principalTable: "ImageCollections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ImageCollections_ImageCollectionId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageCollections",
                table: "ImageCollections");

            migrationBuilder.RenameTable(
                name: "ImageCollections",
                newName: "ImageCollection");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "ImageCollection",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageCollection",
                table: "ImageCollection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ImageCollection_ImageCollectionId",
                table: "Images",
                column: "ImageCollectionId",
                principalTable: "ImageCollection",
                principalColumn: "Id");
        }
    }
}
