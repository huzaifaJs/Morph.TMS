using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class AddShipmentPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "package_type_id",
                table: "shipment_package",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "package_unique_id",
                table: "shipment_package",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "package_type_id",
                table: "shipment_package");

            migrationBuilder.DropColumn(
                name: "package_unique_id",
                table: "shipment_package");
        }
    }
}
