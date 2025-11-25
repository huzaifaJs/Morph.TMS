using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class AddTelemetryRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_telemetry_records_iot_devices_IoTDeviceId",
                table: "telemetry_records");

            migrationBuilder.DropForeignKey(
                name: "FK_violations_iot_devices_IoTDeviceId",
                table: "violations");

            migrationBuilder.DropIndex(
                name: "IX_violations_IoTDeviceId",
                table: "violations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_telemetry_records",
                table: "telemetry_records");

            migrationBuilder.DropIndex(
                name: "IX_telemetry_records_TenantId_DeviceId_Timestamp",
                table: "telemetry_records");

            migrationBuilder.DropIndex(
                name: "IX_telemetry_records_TenantId_ShipmentId_Timestamp",
                table: "telemetry_records");

            migrationBuilder.DropColumn(
                name: "IoTDeviceId",
                table: "violations");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "telemetry_records");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "telemetry_records");

            migrationBuilder.RenameTable(
                name: "telemetry_records",
                newName: "TelemetryRecords");

            migrationBuilder.RenameColumn(
                name: "gps_altitude",
                table: "TelemetryRecords",
                newName: "Gps_Altitude");

            migrationBuilder.RenameColumn(
                name: "gps_accuracy",
                table: "TelemetryRecords",
                newName: "Gps_Accuracy");

            migrationBuilder.RenameColumn(
                name: "gps_longitude",
                table: "TelemetryRecords",
                newName: "GpsLongitude");

            migrationBuilder.RenameColumn(
                name: "gps_latitude",
                table: "TelemetryRecords",
                newName: "GpsLatitude");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "TelemetryRecords",
                newName: "TimestampUtc");

            migrationBuilder.RenameColumn(
                name: "SensorType",
                table: "TelemetryRecords",
                newName: "Nbrfid");

            migrationBuilder.RenameIndex(
                name: "IX_telemetry_records_IoTDeviceId",
                table: "TelemetryRecords",
                newName: "IX_TelemetryRecords_IoTDeviceId");

            migrationBuilder.AddColumn<string>(
                name: "FirmwareVersion",
                table: "iot_devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "iot_devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSeenUtc",
                table: "iot_devices",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<double>(
                name: "BatteryLevel",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Accuracy",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Altitude",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Co2",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionType",
                table: "TelemetryRecords",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceMode",
                table: "TelemetryRecords",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceState",
                table: "TelemetryRecords",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmwareVersion",
                table: "TelemetryRecords",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Humidity",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "TelemetryRecords",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Light",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MeanVibration",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pressure",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rssi",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SignalQuality",
                table: "TelemetryRecords",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Speed",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TelemetryRecords",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "TimestampRaw",
                table: "TelemetryRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "Voc",
                table: "TelemetryRecords",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TelemetryRecords",
                table: "TelemetryRecords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "device_events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_device_events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicyViolation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uuid", nullable: false),
                    RuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SensorType = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    OccurredAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShipmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ContainerId = table.Column<Guid>(type: "uuid", nullable: true),
                    IoTDeviceId = table.Column<Guid>(type: "uuid", nullable: true),
                    PolicyRuleId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_PolicyViolation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyViolation_iot_devices_IoTDeviceId",
                        column: x => x.IoTDeviceId,
                        principalTable: "iot_devices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PolicyViolation_policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyViolation_policy_rules_PolicyRuleId",
                        column: x => x.PolicyRuleId,
                        principalTable: "policy_rules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    ShipmentNumber = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SourceLocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    DestinationLocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExpectedDispatchDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                    table.PrimaryKey("PK_Shipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Container",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    ShipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContainerIdentifier = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: true),
                    ContainerType = table.Column<string>(type: "text", nullable: true),
                    PolicyId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_Container", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Container_Shipment_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sku = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_Package", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Package_Container_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Container",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryRecords_ContainerId",
                table: "TelemetryRecords",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryRecords_ShipmentId",
                table: "TelemetryRecords",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Container_ShipmentId",
                table: "Container",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Package_ContainerId",
                table: "Package",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyViolation_IoTDeviceId",
                table: "PolicyViolation",
                column: "IoTDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyViolation_PolicyId",
                table: "PolicyViolation",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyViolation_PolicyRuleId",
                table: "PolicyViolation",
                column: "PolicyRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TelemetryRecords_Container_ContainerId",
                table: "TelemetryRecords",
                column: "ContainerId",
                principalTable: "Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelemetryRecords_Shipment_ShipmentId",
                table: "TelemetryRecords",
                column: "ShipmentId",
                principalTable: "Shipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelemetryRecords_iot_devices_IoTDeviceId",
                table: "TelemetryRecords",
                column: "IoTDeviceId",
                principalTable: "iot_devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelemetryRecords_Container_ContainerId",
                table: "TelemetryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TelemetryRecords_Shipment_ShipmentId",
                table: "TelemetryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TelemetryRecords_iot_devices_IoTDeviceId",
                table: "TelemetryRecords");

            migrationBuilder.DropTable(
                name: "device_events");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "PolicyViolation");

            migrationBuilder.DropTable(
                name: "Container");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TelemetryRecords",
                table: "TelemetryRecords");

            migrationBuilder.DropIndex(
                name: "IX_TelemetryRecords_ContainerId",
                table: "TelemetryRecords");

            migrationBuilder.DropIndex(
                name: "IX_TelemetryRecords_ShipmentId",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "FirmwareVersion",
                table: "iot_devices");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "iot_devices");

            migrationBuilder.DropColumn(
                name: "LastSeenUtc",
                table: "iot_devices");

            migrationBuilder.DropColumn(
                name: "Accuracy",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "Altitude",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "Co2",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "ConnectionType",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "DeviceMode",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "DeviceState",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "FirmwareVersion",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "Humidity",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "Light",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "MeanVibration",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "Rssi",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "SignalQuality",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "TimestampRaw",
                table: "TelemetryRecords");

            migrationBuilder.DropColumn(
                name: "Voc",
                table: "TelemetryRecords");

            migrationBuilder.RenameTable(
                name: "TelemetryRecords",
                newName: "telemetry_records");

            migrationBuilder.RenameColumn(
                name: "Gps_Altitude",
                table: "telemetry_records",
                newName: "gps_altitude");

            migrationBuilder.RenameColumn(
                name: "Gps_Accuracy",
                table: "telemetry_records",
                newName: "gps_accuracy");

            migrationBuilder.RenameColumn(
                name: "GpsLongitude",
                table: "telemetry_records",
                newName: "gps_longitude");

            migrationBuilder.RenameColumn(
                name: "GpsLatitude",
                table: "telemetry_records",
                newName: "gps_latitude");

            migrationBuilder.RenameColumn(
                name: "TimestampUtc",
                table: "telemetry_records",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "Nbrfid",
                table: "telemetry_records",
                newName: "SensorType");

            migrationBuilder.RenameIndex(
                name: "IX_TelemetryRecords_IoTDeviceId",
                table: "telemetry_records",
                newName: "IX_telemetry_records_IoTDeviceId");

            migrationBuilder.AddColumn<Guid>(
                name: "IoTDeviceId",
                table: "violations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryLevel",
                table: "telemetry_records",
                type: "numeric(5,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "telemetry_records",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "telemetry_records",
                type: "numeric(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_telemetry_records",
                table: "telemetry_records",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_violations_IoTDeviceId",
                table: "violations",
                column: "IoTDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_telemetry_records_TenantId_DeviceId_Timestamp",
                table: "telemetry_records",
                columns: new[] { "TenantId", "DeviceId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_telemetry_records_TenantId_ShipmentId_Timestamp",
                table: "telemetry_records",
                columns: new[] { "TenantId", "ShipmentId", "Timestamp" });

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
    }
}
