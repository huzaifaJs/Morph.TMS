using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleContainer_V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "weight_capacity",
                table: "vehicle_containers",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_containers_container_type_id",
                table: "vehicle_containers",
                column: "container_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_containers_vehicle_document_types_container_type_id",
                table: "vehicle_containers",
                column: "container_type_id",
                principalTable: "vehicle_document_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_containers_vehicle_document_types_container_type_id",
                table: "vehicle_containers");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_containers_container_type_id",
                table: "vehicle_containers");

            migrationBuilder.AlterColumn<decimal>(
                name: "weight_capacity",
                table: "vehicle_containers",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
