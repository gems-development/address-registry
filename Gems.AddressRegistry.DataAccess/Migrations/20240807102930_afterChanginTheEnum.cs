using Gems.AddressRegistry.Entities.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class afterChanginTheEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SourceType",
                table: "DataSource",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
            migrationBuilder.Sql("UPDATE \"DataSource\" SET \"SourceType\" = 'Fias' WHERE \"SourceType\" = '1'");
            migrationBuilder.Sql("UPDATE \"DataSource\" SET \"SourceType\" = 'Osm' WHERE \"SourceType\" = '0'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SourceType",
                table: "DataSource",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
