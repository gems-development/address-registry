using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_District_DistrictId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_House_HouseId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Street_StreetId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "House");

            migrationBuilder.DropTable(
                name: "Street");

            migrationBuilder.RenameColumn(
                name: "StreetId",
                table: "Addresses",
                newName: "ERNId");

            migrationBuilder.RenameColumn(
                name: "HouseId",
                table: "Addresses",
                newName: "TerritoryId");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "Addresses",
                newName: "SpaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_StreetId",
                table: "Addresses",
                newName: "IX_Addresses_ERNId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_HouseId",
                table: "Addresses",
                newName: "IX_Addresses_TerritoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_DistrictId",
                table: "Addresses",
                newName: "IX_Addresses_SpaceId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Region",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Region",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Country",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "AdministrativeAreaId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EPSId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LandPlotId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MunicipalAreaId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SettlementId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AdministrativeArea",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Postcode = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EPS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ERN",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERN", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LandPlot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandPlot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MunicipalArea",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MunicipalArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settlement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Space",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Space", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Territory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territory", x => x.Id);
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
                name: "IX_Addresses_EPSId",
                table: "Addresses",
                column: "EPSId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_LandPlotId",
                table: "Addresses",
                column: "LandPlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_MunicipalAreaId",
                table: "Addresses",
                column: "MunicipalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SettlementId",
                table: "Addresses",
                column: "SettlementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AdministrativeArea_AdministrativeAreaId",
                table: "Addresses",
                column: "AdministrativeAreaId",
                principalTable: "AdministrativeArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Building_BuildingId",
                table: "Addresses",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_LandPlot_LandPlotId",
                table: "Addresses",
                column: "LandPlotId",
                principalTable: "LandPlot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_MunicipalArea_MunicipalAreaId",
                table: "Addresses",
                column: "MunicipalAreaId",
                principalTable: "MunicipalArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Settlement_SettlementId",
                table: "Addresses",
                column: "SettlementId",
                principalTable: "Settlement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Space_SpaceId",
                table: "Addresses",
                column: "SpaceId",
                principalTable: "Space",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Territory_TerritoryId",
                table: "Addresses",
                column: "TerritoryId",
                principalTable: "Territory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AdministrativeArea_AdministrativeAreaId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Building_BuildingId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_EPS_EPSId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_ERN_ERNId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_LandPlot_LandPlotId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_MunicipalArea_MunicipalAreaId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Settlement_SettlementId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Space_SpaceId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Territory_TerritoryId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "AdministrativeArea");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "EPS");

            migrationBuilder.DropTable(
                name: "ERN");

            migrationBuilder.DropTable(
                name: "LandPlot");

            migrationBuilder.DropTable(
                name: "MunicipalArea");

            migrationBuilder.DropTable(
                name: "Settlement");

            migrationBuilder.DropTable(
                name: "Space");

            migrationBuilder.DropTable(
                name: "Territory");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_AdministrativeAreaId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_BuildingId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_EPSId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_LandPlotId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_MunicipalAreaId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_SettlementId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "AdministrativeAreaId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "EPSId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LandPlotId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "MunicipalAreaId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "TerritoryId",
                table: "Addresses",
                newName: "HouseId");

            migrationBuilder.RenameColumn(
                name: "SpaceId",
                table: "Addresses",
                newName: "DistrictId");

            migrationBuilder.RenameColumn(
                name: "ERNId",
                table: "Addresses",
                newName: "StreetId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_TerritoryId",
                table: "Addresses",
                newName: "IX_Addresses_HouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_SpaceId",
                table: "Addresses",
                newName: "IX_Addresses_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ERNId",
                table: "Addresses",
                newName: "IX_Addresses_StreetId");

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Postcode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Street",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Street", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_District_DistrictId",
                table: "Addresses",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_House_HouseId",
                table: "Addresses",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Street_StreetId",
                table: "Addresses",
                column: "StreetId",
                principalTable: "Street",
                principalColumn: "Id");
        }
    }
}
