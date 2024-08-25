using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripGuide.Migrations
{
    /// <inheritdoc />
    public partial class Addnewmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripRoutes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripRoutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TripRouteTouristObjects",
                columns: table => new
                {
                    TripRouteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TouristObjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripRouteTouristObjects", x => new { x.TripRouteId, x.TouristObjectId });
                    table.ForeignKey(
                        name: "FK_TripRouteTouristObjects_TouristObjects_TouristObjectId",
                        column: x => x.TouristObjectId,
                        principalTable: "TouristObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripRouteTouristObjects_TripRoutes_TripRouteId",
                        column: x => x.TripRouteId,
                        principalTable: "TripRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripRouteTouristObjects_TouristObjectId",
                table: "TripRouteTouristObjects",
                column: "TouristObjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripRouteTouristObjects");

            migrationBuilder.DropTable(
                name: "TripRoutes");
        }
    }
}
