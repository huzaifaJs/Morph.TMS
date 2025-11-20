using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class Initial_TMS_Schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OccurredAtUtc",
                table: "violations",
                newName: "OccurredAt");

            migrationBuilder.AddColumn<Guid>(
                name: "IoTDeviceId",
                table: "violations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SensorType",
                table: "violations",
                type: "integer",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "violations",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "violations",
                type: "numeric(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "IoTDeviceId",
                table: "telemetry_records",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LastKnown_Accuracy",
                table: "iot_devices",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LastKnown_Altitude",
                table: "iot_devices",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LastKnown_Latitude",
                table: "iot_devices",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LastKnown_Longitude",
                table: "iot_devices",
                type: "double precision",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "device_logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Severity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    TimestampUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoFenceZones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ShapeType = table.Column<int>(type: "integer", nullable: false),
                    CenterLatitude = table.Column<double>(type: "double precision", nullable: true),
                    CenterLongitude = table.Column<double>(type: "double precision", nullable: true),
                    RadiusMeters = table.Column<double>(type: "double precision", nullable: true),
                    PolygonCoordinatesJson = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoFenceZones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_violations_IoTDeviceId",
                table: "violations",
                column: "IoTDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_telemetry_records_IoTDeviceId",
                table: "telemetry_records",
                column: "IoTDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_device_logs_DeviceId",
                table: "device_logs",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_telemetry_records_iot_devices_IoTDeviceId",
                table: "telemetry_records",
                column: "IoTDeviceId",
                principalTable: "iot_devices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_violations_iot_devices_IoTDeviceId",
                table: "violations",
                column: "IoTDeviceId",
                principalTable: "iot_devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_telemetry_records_iot_devices_IoTDeviceId",
                table: "telemetry_records");

            migrationBuilder.DropForeignKey(
                name: "FK_violations_iot_devices_IoTDeviceId",
                table: "violations");

            migrationBuilder.DropTable(
                name: "device_logs");

            migrationBuilder.DropTable(
                name: "GeoFenceZones");

            migrationBuilder.DropIndex(
                name: "IX_violations_IoTDeviceId",
                table: "violations");

            migrationBuilder.DropIndex(
                name: "IX_telemetry_records_IoTDeviceId",
                table: "telemetry_records");

            migrationBuilder.DropColumn(
                name: "IoTDeviceId",
                table: "violations");

            migrationBuilder.DropColumn(
                name: "SensorType",
                table: "violations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "violations");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "violations");

            migrationBuilder.DropColumn(
                name: "IoTDeviceId",
                table: "telemetry_records");

            migrationBuilder.DropColumn(
                name: "LastKnown_Accuracy",
                table: "iot_devices");

            migrationBuilder.DropColumn(
                name: "LastKnown_Altitude",
                table: "iot_devices");

            migrationBuilder.DropColumn(
                name: "LastKnown_Latitude",
                table: "iot_devices");

            migrationBuilder.DropColumn(
                name: "LastKnown_Longitude",
                table: "iot_devices");

            migrationBuilder.RenameColumn(
                name: "OccurredAt",
                table: "violations",
                newName: "OccurredAtUtc");
        }
    }
}
