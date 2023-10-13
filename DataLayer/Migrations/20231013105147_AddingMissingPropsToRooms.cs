using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddingMissingPropsToRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AspNetUserId",
                table: "RoomsOrders",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AspNetUserId",
                table: "PackageOrders",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdults",
                table: "HotelRooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChildren",
                table: "HotelRooms",
                type: "int",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "AspNetUserId",
                table: "PackageOrders");

            migrationBuilder.DropColumn(
                name: "NumberOfAdults",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "NumberOfChildren",
                table: "HotelRooms");
        }
    }
}
