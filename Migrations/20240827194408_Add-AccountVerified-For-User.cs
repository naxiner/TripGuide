using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripGuide.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountVerifiedForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccountVerified",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountVerified",
                table: "AspNetUsers");
        }
    }
}
