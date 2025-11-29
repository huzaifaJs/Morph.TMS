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
                name: "FK_TelemetryRecords_Container_ContainerId",
                table: "TelemetryRecords");

            migrationBuilder.DropIndex(
                name: "IX_TelemetryRecords_ContainerId",
                table: "TelemetryRecords");

            migrationBuilder.RenameColumn(
                name: "Gps_Altitude",
                table: "TelemetryRecords",
                newName: "GpsAltitude");

            migrationBuilder.RenameColumn(
                name: "Gps_Accuracy",
                table: "TelemetryRecords",
                newName: "GpsAccuracy");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryRecords_DeviceId",
                table: "TelemetryRecords",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TelemetryRecords_DeviceId",
                table: "TelemetryRecords");

            migrationBuilder.RenameColumn(
                name: "GpsAltitude",
                table: "TelemetryRecords",
                newName: "Gps_Altitude");

            migrationBuilder.RenameColumn(
                name: "GpsAccuracy",
                table: "TelemetryRecords",
                newName: "Gps_Accuracy");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryRecords_ContainerId",
                table: "TelemetryRecords",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TelemetryRecords_Container_ContainerId",
                table: "TelemetryRecords",
                column: "ContainerId",
                principalTable: "Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
