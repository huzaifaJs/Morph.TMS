using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class AddShipmentothertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedOn",
                table: "tms_shipment_container_assignment",
                newName: "assignedOn");

            migrationBuilder.AddColumn<long>(
                name: "vehicle_id",
                table: "tms_shipment_container_assignment",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tms_shipment_iot_policy_assignment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    device_id = table.Column<long>(type: "bigint", nullable: true),
                    shipment_id = table.Column<long>(type: "bigint", nullable: true),
                    vehicle_container_id = table.Column<long>(type: "bigint", nullable: true),
                    min_value = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    max_value = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    assigned_by = table.Column<long>(type: "bigint", nullable: true),
                    AssignedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true),
                    deleted_by = table.Column<long>(type: "bigint", nullable: true),
                    active_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    active_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tms_shipment_iot_policy_assignment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tms_shipment_package_assignment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    vehicle_container_id = table.Column<long>(type: "bigint", nullable: true),
                    shipment_id = table.Column<long>(type: "bigint", nullable: true),
                    package_id = table.Column<long>(type: "bigint", nullable: true),
                    assigned_by = table.Column<long>(type: "bigint", nullable: true),
                    assigned_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true),
                    deleted_by = table.Column<long>(type: "bigint", nullable: true),
                    active_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    active_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tms_shipment_package_assignment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tms_shipment_route_assignment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    shipment_id = table.Column<long>(type: "bigint", nullable: true),
                    vehicle_container_id = table.Column<long>(type: "bigint", nullable: true),
                    vehicle_id = table.Column<long>(type: "bigint", nullable: true),
                    longitude = table.Column<string>(type: "text", nullable: true),
                    latitude = table.Column<string>(type: "text", nullable: true),
                    assigned_by = table.Column<long>(type: "bigint", nullable: true),
                    AssignedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true),
                    deleted_by = table.Column<long>(type: "bigint", nullable: true),
                    active_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    active_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tms_shipment_route_assignment", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tms_shipment_iot_policy_assignment");

            migrationBuilder.DropTable(
                name: "tms_shipment_package_assignment");

            migrationBuilder.DropTable(
                name: "tms_shipment_route_assignment");

            migrationBuilder.DropColumn(
                name: "vehicle_id",
                table: "tms_shipment_container_assignment");

            migrationBuilder.RenameColumn(
                name: "assignedOn",
                table: "tms_shipment_container_assignment",
                newName: "AssignedOn");
        }
    }
}
