using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_LandPlots_LandPlotId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Spaces_SpaceId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_LandPlotId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_SpaceId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LandPlotId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "SpaceId",
                table: "Addresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LandPlotId",
                table: "Addresses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SpaceId",
                table: "Addresses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_LandPlotId",
                table: "Addresses",
                column: "LandPlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SpaceId",
                table: "Addresses",
                column: "SpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_LandPlots_LandPlotId",
                table: "Addresses",
                column: "LandPlotId",
                principalTable: "LandPlots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Spaces_SpaceId",
                table: "Addresses",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id");
        }
    }
}
