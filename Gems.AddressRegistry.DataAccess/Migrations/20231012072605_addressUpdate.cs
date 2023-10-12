using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addressUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AdministrativeArea_AdministrativeAreaId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Building_BuildingId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_City_CityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Eps_EpsId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Ern_ErnId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_LandPlot_LandPlotId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Space_SpaceId",
                table: "Addresses");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpaceId",
                table: "Addresses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "LandPlotId",
                table: "Addresses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ErnId",
                table: "Addresses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "EpsId",
                table: "Addresses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                table: "Addresses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "BuildingId",
                table: "Addresses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdministrativeAreaId",
                table: "Addresses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AdministrativeArea_AdministrativeAreaId",
                table: "Addresses",
                column: "AdministrativeAreaId",
                principalTable: "AdministrativeArea",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Building_BuildingId",
                table: "Addresses",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_City_CityId",
                table: "Addresses",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id");

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
                name: "FK_Addresses_LandPlot_LandPlotId",
                table: "Addresses",
                column: "LandPlotId",
                principalTable: "LandPlot",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Space_SpaceId",
                table: "Addresses",
                column: "SpaceId",
                principalTable: "Space",
                principalColumn: "Id");
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
                name: "FK_Addresses_City_CityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Eps_EpsId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Ern_ErnId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_LandPlot_LandPlotId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Space_SpaceId",
                table: "Addresses");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpaceId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LandPlotId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ErnId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EpsId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BuildingId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AdministrativeAreaId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
                name: "FK_Addresses_City_CityId",
                table: "Addresses",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Addresses_LandPlot_LandPlotId",
                table: "Addresses",
                column: "LandPlotId",
                principalTable: "LandPlot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Space_SpaceId",
                table: "Addresses",
                column: "SpaceId",
                principalTable: "Space",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
