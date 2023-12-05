using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class blogUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondaryDescription",
                table: "Blogs",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Blogs",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondaryDescription",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Blogs");
        }
    }
}
