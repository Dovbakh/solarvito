using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solarvito.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AdvertisementDeleteImagePathColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Advertisements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Advertisements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
