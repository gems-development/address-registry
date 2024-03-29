using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_LandPlots_LandPlotId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Spaces_SpaceId",
                table: "DataSource");

            migrationBuilder.DropTable(
                name: "LandPlots");

            migrationBuilder.DropTable(
                name: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_LandPlotId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_SpaceId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "LandPlotId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "SpaceId",
                table: "DataSource");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LandPlotId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SpaceId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LandPlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandPlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true),
                    Number = table.Column<string>(type: "text", nullable: false),
                    SpaceType = table.Column<int>(type: "integer", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_LandPlotId",
                table: "DataSource",
                column: "LandPlotId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_SpaceId",
                table: "DataSource",
                column: "SpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_LandPlots_LandPlotId",
                table: "DataSource",
                column: "LandPlotId",
                principalTable: "LandPlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_Spaces_SpaceId",
                table: "DataSource",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
