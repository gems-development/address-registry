using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class typeUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PlaningStructureElements_EpsId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_RoadNetworkElements_ErnId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "ErnId",
                table: "Addresses",
                newName: "RoadNetworkElementId");

            migrationBuilder.RenameColumn(
                name: "EpsId",
                table: "Addresses",
                newName: "PlaningStructureElementId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ErnId",
                table: "Addresses",
                newName: "IX_Addresses_RoadNetworkElementId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_EpsId",
                table: "Addresses",
                newName: "IX_Addresses_PlaningStructureElementId");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "Territories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Spaces",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "Spaces",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "Settlements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "RoadNetworkElements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "Regions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "PlaningStructureElements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "MunicipalAreas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "LandPlots",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "Countries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Buildings",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "Buildings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "AdministrativeAreas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeoJson",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PlaningStructureElements_PlaningStructureElementId",
                table: "Addresses",
                column: "PlaningStructureElementId",
                principalTable: "PlaningStructureElements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_RoadNetworkElements_RoadNetworkElementId",
                table: "Addresses",
                column: "RoadNetworkElementId",
                principalTable: "RoadNetworkElements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PlaningStructureElements_PlaningStructureElementId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_RoadNetworkElements_RoadNetworkElementId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "Settlements");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "RoadNetworkElements");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "PlaningStructureElements");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "MunicipalAreas");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "LandPlots");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "AdministrativeAreas");

            migrationBuilder.DropColumn(
                name: "GeoJson",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "RoadNetworkElementId",
                table: "Addresses",
                newName: "ErnId");

            migrationBuilder.RenameColumn(
                name: "PlaningStructureElementId",
                table: "Addresses",
                newName: "EpsId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_RoadNetworkElementId",
                table: "Addresses",
                newName: "IX_Addresses_ErnId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_PlaningStructureElementId",
                table: "Addresses",
                newName: "IX_Addresses_EpsId");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Spaces",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Buildings",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PlaningStructureElements_EpsId",
                table: "Addresses",
                column: "EpsId",
                principalTable: "PlaningStructureElements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_RoadNetworkElements_ErnId",
                table: "Addresses",
                column: "ErnId",
                principalTable: "RoadNetworkElements",
                principalColumn: "Id");
        }
    }
}
