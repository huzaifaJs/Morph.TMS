using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleUpdateForinkey_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_VehicleTypes_VehicleTypeId",
                table: "vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_fuel_types_FuelTypesId",
                table: "vehicle");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_FuelTypesId",
                table: "vehicle");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_VehicleTypeId",
                table: "vehicle");

            migrationBuilder.DropColumn(
                name: "FuelTypesId",
                table: "vehicle");

            migrationBuilder.DropColumn(
                name: "VehicleTypeId",
                table: "vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_fuel_types_id",
                table: "vehicle",
                column: "fuel_types_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_vehicle_types_id",
                table: "vehicle",
                column: "vehicle_types_id");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_VehicleTypes_vehicle_types_id",
                table: "vehicle",
                column: "vehicle_types_id",
                principalTable: "VehicleTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_fuel_types_fuel_types_id",
                table: "vehicle",
                column: "fuel_types_id",
                principalTable: "fuel_types",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_VehicleTypes_vehicle_types_id",
                table: "vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_fuel_types_fuel_types_id",
                table: "vehicle");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_fuel_types_id",
                table: "vehicle");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_vehicle_types_id",
                table: "vehicle");

            migrationBuilder.AddColumn<long>(
                name: "FuelTypesId",
                table: "vehicle",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VehicleTypeId",
                table: "vehicle",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_FuelTypesId",
                table: "vehicle",
                column: "FuelTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_VehicleTypeId",
                table: "vehicle",
                column: "VehicleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_VehicleTypes_VehicleTypeId",
                table: "vehicle",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_fuel_types_FuelTypesId",
                table: "vehicle",
                column: "FuelTypesId",
                principalTable: "fuel_types",
                principalColumn: "Id");
        }
    }
}
