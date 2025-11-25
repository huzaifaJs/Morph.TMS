using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleUpdateForinkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalDeviceId",
                table: "iot_devices");

            migrationBuilder.AlterColumn<long>(
                name: "vehicle_types_id",
                table: "vehicle",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "fuel_types_id",
                table: "vehicle",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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

            migrationBuilder.AddColumn<int>(
                name: "MorphoDeviceId",
                table: "iot_devices",
                type: "integer",
                maxLength: 128,
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "MorphoDeviceId",
                table: "iot_devices");

            migrationBuilder.AlterColumn<long>(
                name: "vehicle_types_id",
                table: "vehicle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "fuel_types_id",
                table: "vehicle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalDeviceId",
                table: "iot_devices",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }
    }
}
