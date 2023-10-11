using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class secondinitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true),
                    BlogMainImage = table.Column<string>(type: "longtext", nullable: true),
                    BlogMainText = table.Column<string>(type: "longtext", nullable: true),
                    IsPublished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    Message = table.Column<string>(type: "longtext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    HotelName = table.Column<string>(type: "longtext", nullable: true),
                    HotelDescription = table.Column<string>(type: "longtext", nullable: true),
                    Price = table.Column<float>(type: "float", nullable: true),
                    Discount = table.Column<float>(type: "float", nullable: true),
                    HotelMainImage = table.Column<string>(type: "longtext", nullable: true),
                    HotelImage1 = table.Column<string>(type: "longtext", nullable: true),
                    HotelImage2 = table.Column<string>(type: "longtext", nullable: true),
                    HotelImage3 = table.Column<string>(type: "longtext", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    NumberOfBeds = table.Column<int>(type: "int", nullable: true),
                    NumberOfSofas = table.Column<int>(type: "int", nullable: true),
                    NumberOfBathrooms = table.Column<int>(type: "int", nullable: true),
                    IsPublished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsWifi = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsTV = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsAC = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsRoomHeater = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    PackageTypeId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<float>(type: "float", nullable: true),
                    Discount = table.Column<float>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: true),
                    NumberOfNights = table.Column<int>(type: "int", nullable: true),
                    PackageMainImage = table.Column<string>(type: "longtext", nullable: true),
                    PackageImage1 = table.Column<string>(type: "longtext", nullable: true),
                    PackageImage2 = table.Column<string>(type: "longtext", nullable: true),
                    PackageImage3 = table.Column<string>(type: "longtext", nullable: true),
                    HotelName = table.Column<string>(type: "longtext", nullable: true),
                    NumberOfAdults = table.Column<int>(type: "int", nullable: true),
                    NumberOfChildren = table.Column<int>(type: "int", nullable: true),
                    HotelDescription = table.Column<string>(type: "longtext", nullable: true),
                    HotelMainImage = table.Column<string>(type: "longtext", nullable: true),
                    HotelImage1 = table.Column<string>(type: "longtext", nullable: true),
                    HotelImage2 = table.Column<string>(type: "longtext", nullable: true),
                    HotelImage3 = table.Column<string>(type: "longtext", nullable: true),
                    NumberOfBeds = table.Column<int>(type: "int", nullable: true),
                    NumberOfSofas = table.Column<int>(type: "int", nullable: true),
                    NumberOfBathrooms = table.Column<int>(type: "int", nullable: true),
                    IsPublished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsVip = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsWifi = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsTV = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsAC = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsRoomHeater = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProviderServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    HotelName = table.Column<string>(type: "longtext", nullable: true),
                    HotelDescription = table.Column<string>(type: "longtext", nullable: true),
                    Price = table.Column<float>(type: "float", nullable: true),
                    Discount = table.Column<float>(type: "float", nullable: true),
                    HotelMainImage = table.Column<string>(type: "longtext", nullable: true),
                    HotelImage1 = table.Column<string>(type: "longtext", nullable: true),
                    HotelImage2 = table.Column<string>(type: "longtext", nullable: true),
                    HotelImage3 = table.Column<string>(type: "longtext", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    NumberOfBeds = table.Column<int>(type: "int", nullable: true),
                    NumberOfSofas = table.Column<int>(type: "int", nullable: true),
                    NumberOfBathrooms = table.Column<int>(type: "int", nullable: true),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: true),
                    IsPublished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsWifi = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsTV = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsAC = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsRoomHeater = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsShowNotification = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderServices", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PackageOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageOrders_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PackageOrders_PackageId",
                table: "PackageOrders",
                column: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropTable(
                name: "PackageOrders");

            migrationBuilder.DropTable(
                name: "ProviderServices");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
