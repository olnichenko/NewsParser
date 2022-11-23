using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsParserDAL.Migrations
{
    /// <inheritdoc />
    public partial class Addedurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePublished",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSaved",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsTelegramSended",
                table: "Articles",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DatePublished",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DateSaved",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsTelegramSended",
                table: "Articles");
        }
    }
}
