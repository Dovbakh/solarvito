using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solarvito.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class DeletecountcolumnsinUsertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRates",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfRates",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Users",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
