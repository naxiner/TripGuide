using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripGuide.Migrations
{
    /// <inheritdoc />
    public partial class Renamefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TouristOjbects",
                table: "TouristOjbects");

            migrationBuilder.RenameTable(
                name: "TouristOjbects",
                newName: "TouristObjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TouristObjects",
                table: "TouristObjects",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TouristObjects",
                table: "TouristObjects");

            migrationBuilder.RenameTable(
                name: "TouristObjects",
                newName: "TouristOjbects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TouristOjbects",
                table: "TouristOjbects",
                column: "Id");
        }
    }
}
