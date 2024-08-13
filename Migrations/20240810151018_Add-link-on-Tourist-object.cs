using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripGuide.Migrations
{
    /// <inheritdoc />
    public partial class AddlinkonTouristobject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TouristObjectId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_TouristObjectId",
                table: "BlogPosts",
                column: "TouristObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_TouristObjects_TouristObjectId",
                table: "BlogPosts",
                column: "TouristObjectId",
                principalTable: "TouristObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_TouristObjects_TouristObjectId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_TouristObjectId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "TouristObjectId",
                table: "BlogPosts");
        }
    }
}
