using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "RoomsOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "RoomClasses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "ProviderServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "PackageTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "PackageOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "HotelRooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Blogs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "RoomsOrders");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "RoomClasses");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "ProviderServices");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "PackageTypes");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "PackageOrders");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Blogs");
        }
    }
}
