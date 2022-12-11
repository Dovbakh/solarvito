using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solarvito.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AdvertisementAddPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Advertisements",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Advertisements");
        }
    }
}
