using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class changeTopTenToInteger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTopTen",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsTopTen",
                table: "HotelRooms");

            migrationBuilder.AddColumn<int>(
                name: "TopTen",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopTen",
                table: "HotelRooms",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopTen",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "TopTen",
                table: "HotelRooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsTopTen",
                table: "Packages",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTopTen",
                table: "HotelRooms",
                type: "tinyint(1)",
                nullable: true);
        }
    }
}
