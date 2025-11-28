using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleDocsV1Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "mainVehicleDocumentTypeId",
                table: "vehicle_documents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "mainVehiclesId",
                table: "vehicle_documents",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_documents_mainVehicleDocumentTypeId",
                table: "vehicle_documents",
                column: "mainVehicleDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_documents_mainVehiclesId",
                table: "vehicle_documents",
                column: "mainVehiclesId");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_documents_vehicle_document_types_mainVehicleDocumen~",
                table: "vehicle_documents",
                column: "mainVehicleDocumentTypeId",
                principalTable: "vehicle_document_types",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_documents_vehicle_mainVehiclesId",
                table: "vehicle_documents",
                column: "mainVehiclesId",
                principalTable: "vehicle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_documents_vehicle_document_types_mainVehicleDocumen~",
                table: "vehicle_documents");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_documents_vehicle_mainVehiclesId",
                table: "vehicle_documents");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_documents_mainVehicleDocumentTypeId",
                table: "vehicle_documents");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_documents_mainVehiclesId",
                table: "vehicle_documents");

            migrationBuilder.DropColumn(
                name: "mainVehicleDocumentTypeId",
                table: "vehicle_documents");

            migrationBuilder.DropColumn(
                name: "mainVehiclesId",
                table: "vehicle_documents");
        }
    }
}
