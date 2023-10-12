using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_EPS_EPSId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_ERN_ERNId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERN",
                table: "ERN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EPS",
                table: "EPS");

            migrationBuilder.RenameTable(
                name: "ERN",
                newName: "Ern");

            migrationBuilder.RenameTable(
                name: "EPS",
                newName: "Eps");

            migrationBuilder.RenameColumn(
                name: "ERNId",
                table: "Addresses",
                newName: "ErnId");

            migrationBuilder.RenameColumn(
                name: "EPSId",
                table: "Addresses",
                newName: "EpsId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ERNId",
                table: "Addresses",
                newName: "IX_Addresses_ErnId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_EPSId",
                table: "Addresses",
                newName: "IX_Addresses_EpsId");

            migrationBuilder.AddColumn<int>(
                name: "SpaceType",
                table: "Space",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AdministrativeAreaId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EpsId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ErnId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LandPlotId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MunicipalAreaId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SettlementId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TerritoryId",
                table: "DataSource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuildingType",
                table: "Building",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ern",
                table: "Ern",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Eps",
                table: "Eps",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_AddressId",
                table: "DataSource",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_AdministrativeAreaId",
                table: "DataSource",
                column: "AdministrativeAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_BuildingId",
                table: "DataSource",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_CityId",
                table: "DataSource",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_CountryId",
                table: "DataSource",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_EpsId",
                table: "DataSource",
                column: "EpsId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_ErnId",
                table: "DataSource",
                column: "ErnId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_LandPlotId",
                table: "DataSource",
                column: "LandPlotId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_MunicipalAreaId",
                table: "DataSource",
                column: "MunicipalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_RegionId",
                table: "DataSource",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_SettlementId",
                table: "DataSource",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_TerritoryId",
                table: "DataSource",
                column: "TerritoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Eps_EpsId",
                table: "Addresses",
                column: "EpsId",
                principalTable: "Eps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Ern_ErnId",
                table: "Addresses",
                column: "ErnId",
                principalTable: "Ern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_Addresses_AddressId",
                table: "DataSource",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_AdministrativeArea_AdministrativeAreaId",
                table: "DataSource",
                column: "AdministrativeAreaId",
                principalTable: "AdministrativeArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_Building_BuildingId",
                table: "DataSource",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_City_CityId",
                table: "DataSource",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_Country_CountryId",
                table: "DataSource",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_Eps_EpsId",
                table: "DataSource",
                column: "EpsId",
                principalTable: "Eps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_Ern_ErnId",
                table: "DataSource",
                column: "ErnId",
                principalTable: "Ern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_LandPlot_LandPlotId",
                table: "DataSource",
                column: "LandPlotId",
                principalTable: "LandPlot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_MunicipalArea_MunicipalAreaId",
                table: "DataSource",
                column: "MunicipalAreaId",
                principalTable: "MunicipalArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_Region_RegionId",
                table: "DataSource",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_Settlement_SettlementId",
                table: "DataSource",
                column: "SettlementId",
                principalTable: "Settlement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_Territory_TerritoryId",
                table: "DataSource",
                column: "TerritoryId",
                principalTable: "Territory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Eps_EpsId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Ern_ErnId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Addresses_AddressId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_AdministrativeArea_AdministrativeAreaId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Building_BuildingId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_City_CityId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Country_CountryId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Eps_EpsId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Ern_ErnId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_LandPlot_LandPlotId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_MunicipalArea_MunicipalAreaId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Region_RegionId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Settlement_SettlementId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Territory_TerritoryId",
                table: "DataSource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ern",
                table: "Ern");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Eps",
                table: "Eps");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_AddressId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_AdministrativeAreaId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_BuildingId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_CityId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_CountryId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_EpsId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_ErnId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_LandPlotId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_MunicipalAreaId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_RegionId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_SettlementId",
                table: "DataSource");

            migrationBuilder.DropIndex(
                name: "IX_DataSource_TerritoryId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "SpaceType",
                table: "Space");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "AdministrativeAreaId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "EpsId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "ErnId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "LandPlotId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "MunicipalAreaId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "TerritoryId",
                table: "DataSource");

            migrationBuilder.DropColumn(
                name: "BuildingType",
                table: "Building");

            migrationBuilder.RenameTable(
                name: "Ern",
                newName: "ERN");

            migrationBuilder.RenameTable(
                name: "Eps",
                newName: "EPS");

            migrationBuilder.RenameColumn(
                name: "ErnId",
                table: "Addresses",
                newName: "ERNId");

            migrationBuilder.RenameColumn(
                name: "EpsId",
                table: "Addresses",
                newName: "EPSId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ErnId",
                table: "Addresses",
                newName: "IX_Addresses_ERNId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_EpsId",
                table: "Addresses",
                newName: "IX_Addresses_EPSId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERN",
                table: "ERN",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EPS",
                table: "EPS",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_EPS_EPSId",
                table: "Addresses",
                column: "EPSId",
                principalTable: "EPS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_ERN_ERNId",
                table: "Addresses",
                column: "ERNId",
                principalTable: "ERN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
