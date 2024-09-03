using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripGuide.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlogPostmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_TouristObjects_TouristObjectId",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TouristObjectId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_TouristObjects_TouristObjectId",
                table: "BlogPosts",
                column: "TouristObjectId",
                principalTable: "TouristObjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_TouristObjects_TouristObjectId",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TouristObjectId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_TouristObjects_TouristObjectId",
                table: "BlogPosts",
                column: "TouristObjectId",
                principalTable: "TouristObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
