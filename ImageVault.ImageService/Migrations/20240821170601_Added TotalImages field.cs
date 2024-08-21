using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageVault.ImageService.Migrations
{
    /// <inheritdoc />
    public partial class AddedTotalImagesfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TotalImages",
                table: "ImageCollections",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalImages",
                table: "ImageCollections");
        }
    }
}
