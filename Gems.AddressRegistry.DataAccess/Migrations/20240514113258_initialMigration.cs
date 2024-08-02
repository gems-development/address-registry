using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdministrativeAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministrativeAreas_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MunicipalAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MunicipalAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MunicipalAreas_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Territories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MunicipalAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Territories_MunicipalAreas_MunicipalAreaId",
                        column: x => x.MunicipalAreaId,
                        principalTable: "MunicipalAreas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TerritoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    MunicipalAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    AdministrativeAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_AdministrativeAreas_AdministrativeAreaId",
                        column: x => x.AdministrativeAreaId,
                        principalTable: "AdministrativeAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cities_MunicipalAreas_MunicipalAreaId",
                        column: x => x.MunicipalAreaId,
                        principalTable: "MunicipalAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cities_Territories_TerritoryId",
                        column: x => x.TerritoryId,
                        principalTable: "Territories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    TerritoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    MunicipalAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settlements_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Settlements_MunicipalAreas_MunicipalAreaId",
                        column: x => x.MunicipalAreaId,
                        principalTable: "MunicipalAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Settlements_Territories_TerritoryId",
                        column: x => x.TerritoryId,
                        principalTable: "Territories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlaningStructureElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    SettlementId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaningStructureElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaningStructureElements_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlaningStructureElements_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoadNetworkElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoadNetworkElementType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    SettlementId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlaningStructureElementId = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoadNetworkElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoadNetworkElements_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoadNetworkElements_PlaningStructureElements_PlaningStructu~",
                        column: x => x.PlaningStructureElementId,
                        principalTable: "PlaningStructureElements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoadNetworkElements_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Postcode = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    RoadNetworkElementId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlaningStructureElementId = table.Column<Guid>(type: "uuid", nullable: true),
                    BuildingType = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buildings_PlaningStructureElements_PlaningStructureElementId",
                        column: x => x.PlaningStructureElementId,
                        principalTable: "PlaningStructureElements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Buildings_RoadNetworkElements_RoadNetworkElementId",
                        column: x => x.RoadNetworkElementId,
                        principalTable: "RoadNetworkElements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false),
                    MunicipalAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    AdministrativeAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    TerritoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    SettlementId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlaningStructureElementId = table.Column<Guid>(type: "uuid", nullable: true),
                    RoadNetworkElementId = table.Column<Guid>(type: "uuid", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeoJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AdministrativeAreas_AdministrativeAreaId",
                        column: x => x.AdministrativeAreaId,
                        principalTable: "AdministrativeAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_MunicipalAreas_MunicipalAreaId",
                        column: x => x.MunicipalAreaId,
                        principalTable: "MunicipalAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_PlaningStructureElements_PlaningStructureElementId",
                        column: x => x.PlaningStructureElementId,
                        principalTable: "PlaningStructureElements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_RoadNetworkElements_RoadNetworkElementId",
                        column: x => x.RoadNetworkElementId,
                        principalTable: "RoadNetworkElements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Territories_TerritoryId",
                        column: x => x.TerritoryId,
                        principalTable: "Territories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataSource",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    SourceType = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true),
                    AdministrativeAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    EpsId = table.Column<Guid>(type: "uuid", nullable: true),
                    ErnId = table.Column<Guid>(type: "uuid", nullable: true),
                    MunicipalAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: true),
                    SettlementId = table.Column<Guid>(type: "uuid", nullable: true),
                    TerritoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSource", x => new { x.Id, x.SourceType });
                    table.ForeignKey(
                        name: "FK_DataSource_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSource_AdministrativeAreas_AdministrativeAreaId",
                        column: x => x.AdministrativeAreaId,
                        principalTable: "AdministrativeAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSource_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSource_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSource_MunicipalAreas_MunicipalAreaId",
                        column: x => x.MunicipalAreaId,
                        principalTable: "MunicipalAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSource_PlaningStructureElements_EpsId",
                        column: x => x.EpsId,
                        principalTable: "PlaningStructureElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSource_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSource_RoadNetworkElements_ErnId",
                        column: x => x.ErnId,
                        principalTable: "RoadNetworkElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSource_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSource_Territories_TerritoryId",
                        column: x => x.TerritoryId,
                        principalTable: "Territories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AdministrativeAreaId",
                table: "Addresses",
                column: "AdministrativeAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_BuildingId",
                table: "Addresses",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_MunicipalAreaId",
                table: "Addresses",
                column: "MunicipalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PlaningStructureElementId",
                table: "Addresses",
                column: "PlaningStructureElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RegionId",
                table: "Addresses",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RoadNetworkElementId",
                table: "Addresses",
                column: "RoadNetworkElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SettlementId",
                table: "Addresses",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TerritoryId",
                table: "Addresses",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativeAreas_RegionId",
                table: "AdministrativeAreas",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_PlaningStructureElementId",
                table: "Buildings",
                column: "PlaningStructureElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_RoadNetworkElementId",
                table: "Buildings",
                column: "RoadNetworkElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_AdministrativeAreaId",
                table: "Cities",
                column: "AdministrativeAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_MunicipalAreaId",
                table: "Cities",
                column: "MunicipalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_TerritoryId",
                table: "Cities",
                column: "TerritoryId");

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
                name: "IX_DataSource_EpsId",
                table: "DataSource",
                column: "EpsId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSource_ErnId",
                table: "DataSource",
                column: "ErnId");

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

            migrationBuilder.CreateIndex(
                name: "IX_MunicipalAreas_RegionId",
                table: "MunicipalAreas",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaningStructureElements_CityId",
                table: "PlaningStructureElements",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaningStructureElements_SettlementId",
                table: "PlaningStructureElements",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadNetworkElements_CityId",
                table: "RoadNetworkElements",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadNetworkElements_PlaningStructureElementId",
                table: "RoadNetworkElements",
                column: "PlaningStructureElementId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadNetworkElements_SettlementId",
                table: "RoadNetworkElements",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_CityId",
                table: "Settlements",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_MunicipalAreaId",
                table: "Settlements",
                column: "MunicipalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_TerritoryId",
                table: "Settlements",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Territories_MunicipalAreaId",
                table: "Territories",
                column: "MunicipalAreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "DataSource");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "RoadNetworkElements");

            migrationBuilder.DropTable(
                name: "PlaningStructureElements");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "AdministrativeAreas");

            migrationBuilder.DropTable(
                name: "Territories");

            migrationBuilder.DropTable(
                name: "MunicipalAreas");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
