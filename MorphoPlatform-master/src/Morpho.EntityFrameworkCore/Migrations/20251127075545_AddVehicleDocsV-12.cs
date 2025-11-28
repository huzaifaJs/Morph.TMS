using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleDocsV12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_documents_document_type_id",
                table: "vehicle_documents",
                column: "document_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_documents_vehicle_id",
                table: "vehicle_documents",
                column: "vehicle_id");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_documents_vehicle_document_types_document_type_id",
                table: "vehicle_documents",
                column: "document_type_id",
                principalTable: "vehicle_document_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_documents_vehicle_vehicle_id",
                table: "vehicle_documents",
                column: "vehicle_id",
                principalTable: "vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_documents_vehicle_document_types_document_type_id",
                table: "vehicle_documents");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_documents_vehicle_vehicle_id",
                table: "vehicle_documents");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_documents_document_type_id",
                table: "vehicle_documents");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_documents_vehicle_id",
                table: "vehicle_documents");

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
    }
}
