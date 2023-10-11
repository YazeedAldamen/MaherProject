using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesAddingTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ProviderServices",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Packages",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Packages",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckIn",
                table: "PackageOrders",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOut",
                table: "PackageOrders",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdults",
                table: "PackageOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChildren",
                table: "PackageOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "PackageOrders",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PackageOrders",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "HotelRooms",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: true),
                    PackageId = table.Column<int>(type: "int", nullable: true),
                    ReviewText = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_HotelRooms_HotelId",
                        column: x => x.HotelId,
                        principalTable: "HotelRooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoomsOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    NumberOfAdults = table.Column<int>(type: "int", nullable: true),
                    NumberOfChildren = table.Column<int>(type: "int", nullable: true),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomsOrders_HotelRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "HotelRooms",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderServices_UserId",
                table: "ProviderServices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_UserId",
                table: "Packages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_UserId",
                table: "HotelRooms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HotelId",
                table: "Reviews",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PackageId",
                table: "Reviews",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsOrders_RoomId",
                table: "RoomsOrders",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_AspNetUsers_UserId",
                table: "HotelRooms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_AspNetUsers_UserId",
                table: "Packages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderServices_AspNetUsers_UserId",
                table: "ProviderServices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_AspNetUsers_UserId",
                table: "HotelRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_AspNetUsers_UserId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderServices_AspNetUsers_UserId",
                table: "ProviderServices");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RoomsOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProviderServices_UserId",
                table: "ProviderServices");

            migrationBuilder.DropIndex(
                name: "IX_Packages_UserId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_HotelRooms_UserId",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProviderServices");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "PackageOrders");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "PackageOrders");

            migrationBuilder.DropColumn(
                name: "NumberOfAdults",
                table: "PackageOrders");

            migrationBuilder.DropColumn(
                name: "NumberOfChildren",
                table: "PackageOrders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "PackageOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PackageOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HotelRooms");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Packages",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
