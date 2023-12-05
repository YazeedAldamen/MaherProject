using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class packageUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageOrders_AspNetUsers_AspNetUserId",
                table: "PackageOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomsOrders_AspNetUsers_AspNetUserId",
                table: "RoomsOrders");

            migrationBuilder.DropIndex(
                name: "IX_RoomsOrders_AspNetUserId",
                table: "RoomsOrders");

            migrationBuilder.DropIndex(
                name: "IX_PackageOrders_AspNetUserId",
                table: "PackageOrders");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "RoomsOrders");

            migrationBuilder.DropColumn(
                name: "HotelDescription",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "HotelImage1",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "HotelImage2",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "HotelImage3",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "HotelMainImage",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "HotelName",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsAC",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsRoomHeater",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsTV",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsVip",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsWifi",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "NumberOfAdults",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "NumberOfBathrooms",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "NumberOfBeds",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "NumberOfChildren",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "NumberOfSofas",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "PackageImage2",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "PackageImage3",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "PackageOrders");

            migrationBuilder.CreateTable(
                name: "RoomClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomClasses", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsOrders_UserId",
                table: "RoomsOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageOrders_UserId",
                table: "PackageOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageOrders_AspNetUsers_UserId",
                table: "PackageOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsOrders_AspNetUsers_UserId",
                table: "RoomsOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageOrders_AspNetUsers_UserId",
                table: "PackageOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomsOrders_AspNetUsers_UserId",
                table: "RoomsOrders");

            migrationBuilder.DropTable(
                name: "RoomClasses");

            migrationBuilder.DropIndex(
                name: "IX_RoomsOrders_UserId",
                table: "RoomsOrders");

            migrationBuilder.DropIndex(
                name: "IX_PackageOrders_UserId",
                table: "PackageOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "AspNetUserId",
                table: "RoomsOrders",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelDescription",
                table: "Packages",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelImage1",
                table: "Packages",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelImage2",
                table: "Packages",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelImage3",
                table: "Packages",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelMainImage",
                table: "Packages",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelName",
                table: "Packages",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAC",
                table: "Packages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRoomHeater",
                table: "Packages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTV",
                table: "Packages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVip",
                table: "Packages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWifi",
                table: "Packages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdults",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBathrooms",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBeds",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChildren",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSofas",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageImage2",
                table: "Packages",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageImage3",
                table: "Packages",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AspNetUserId",
                table: "PackageOrders",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomsOrders_AspNetUserId",
                table: "RoomsOrders",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageOrders_AspNetUserId",
                table: "PackageOrders",
                column: "AspNetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageOrders_AspNetUsers_AspNetUserId",
                table: "PackageOrders",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsOrders_AspNetUsers_AspNetUserId",
                table: "RoomsOrders",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
