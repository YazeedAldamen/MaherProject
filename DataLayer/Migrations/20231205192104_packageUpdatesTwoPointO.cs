using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class packageUpdatesTwoPointO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackageDetails",
                table: "Packages",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomClassId",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_RoomClassId",
                table: "Packages",
                column: "RoomClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_RoomClasses_RoomClassId",
                table: "Packages",
                column: "RoomClassId",
                principalTable: "RoomClasses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_RoomClasses_RoomClassId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_RoomClassId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "PackageDetails",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "RoomClassId",
                table: "Packages");
        }
    }
}
