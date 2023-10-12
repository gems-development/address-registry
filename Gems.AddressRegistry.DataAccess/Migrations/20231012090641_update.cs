using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Eps_EpsId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Ern_ErnId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Eps_EpsId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_Ern_ErnId",
                table: "DataSource");

            migrationBuilder.DropTable(
                name: "Eps");

            migrationBuilder.DropTable(
                name: "Ern");

            migrationBuilder.CreateTable(
                name: "PlaningStructureElement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaningStructureElement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoadNetworkElement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoadNetworkElementType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoadNetworkElement", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PlaningStructureElement_EpsId",
                table: "Addresses",
                column: "EpsId",
                principalTable: "PlaningStructureElement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_RoadNetworkElement_ErnId",
                table: "Addresses",
                column: "ErnId",
                principalTable: "RoadNetworkElement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_PlaningStructureElement_EpsId",
                table: "DataSource",
                column: "EpsId",
                principalTable: "PlaningStructureElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataSource_RoadNetworkElement_ErnId",
                table: "DataSource",
                column: "ErnId",
                principalTable: "RoadNetworkElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PlaningStructureElement_EpsId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_RoadNetworkElement_ErnId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_PlaningStructureElement_EpsId",
                table: "DataSource");

            migrationBuilder.DropForeignKey(
                name: "FK_DataSource_RoadNetworkElement_ErnId",
                table: "DataSource");

            migrationBuilder.DropTable(
                name: "PlaningStructureElement");

            migrationBuilder.DropTable(
                name: "RoadNetworkElement");

            migrationBuilder.CreateTable(
                name: "Eps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ern",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Apdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ern", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Eps_EpsId",
                table: "Addresses",
                column: "EpsId",
                principalTable: "Eps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Ern_ErnId",
                table: "Addresses",
                column: "ErnId",
                principalTable: "Ern",
                principalColumn: "Id");

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
        }
    }
}
