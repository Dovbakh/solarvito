using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solarvito.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AdvertisementAddUserNamecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Advertisements",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Advertisements");
        }
    }
}
