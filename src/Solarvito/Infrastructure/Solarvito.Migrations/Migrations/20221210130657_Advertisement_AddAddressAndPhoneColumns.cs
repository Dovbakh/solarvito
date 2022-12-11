using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solarvito.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AdvertisementAddAddressAndPhoneColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Advertisements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Advertisements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Advertisements");
        }
    }
}
