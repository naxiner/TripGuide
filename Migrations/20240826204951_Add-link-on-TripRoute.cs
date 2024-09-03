using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripGuide.Migrations
{
    /// <inheritdoc />
    public partial class AddlinkonTripRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TripRouteId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_TripRouteId",
                table: "BlogPosts",
                column: "TripRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_TripRoutes_TripRouteId",
                table: "BlogPosts",
                column: "TripRouteId",
                principalTable: "TripRoutes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_TripRoutes_TripRouteId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_TripRouteId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "TripRouteId",
                table: "BlogPosts");
        }
    }
}
