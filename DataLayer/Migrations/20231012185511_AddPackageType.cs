using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPackageType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackageOrderId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageOrderId",
                table: "PackageOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PackageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTypes", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PackageOrderId",
                table: "Reviews",
                column: "PackageOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_PackageTypeId",
                table: "Packages",
                column: "PackageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageOrders_PackageOrderId",
                table: "PackageOrders",
                column: "PackageOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageOrders_PackageOrders_PackageOrderId",
                table: "PackageOrders",
                column: "PackageOrderId",
                principalTable: "PackageOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_PackageTypes_PackageTypeId",
                table: "Packages",
                column: "PackageTypeId",
                principalTable: "PackageTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_PackageOrders_PackageOrderId",
                table: "Reviews",
                column: "PackageOrderId",
                principalTable: "PackageOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageOrders_PackageOrders_PackageOrderId",
                table: "PackageOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_PackageTypes_PackageTypeId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_PackageOrders_PackageOrderId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "PackageTypes");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PackageOrderId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Packages_PackageTypeId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_PackageOrders_PackageOrderId",
                table: "PackageOrders");

            migrationBuilder.DropColumn(
                name: "PackageOrderId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PackageOrderId",
                table: "PackageOrders");
        }
    }
}
