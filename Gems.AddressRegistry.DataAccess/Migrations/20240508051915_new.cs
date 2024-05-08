using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Country",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "Country",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Country",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Country",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Country");
        }
    }
}
