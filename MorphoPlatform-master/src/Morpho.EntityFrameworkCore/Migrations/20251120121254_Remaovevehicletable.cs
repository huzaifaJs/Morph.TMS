using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class Remaovevehicletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vehicle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    block_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    block_by = table.Column<long>(type: "bigint", nullable: true),
                    chassis_number = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<long>(type: "bigint", nullable: true),
                    engine_number = table.Column<string>(type: "text", nullable: true),
                    fuel_type = table.Column<string>(type: "text", nullable: true),
                    fuel_types = table.Column<long>(type: "bigint", nullable: false),
                    isblock = table.Column<bool>(type: "boolean", nullable: false),
                    manufacturer = table.Column<string>(type: "text", nullable: true),
                    manufacturing_year = table.Column<int>(type: "integer", nullable: false),
                    model = table.Column<string>(type: "text", nullable: true),
                    model_name = table.Column<string>(type: "text", nullable: true),
                    remark = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true),
                    vehicle_name = table.Column<string>(type: "text", nullable: true),
                    vehicle_number = table.Column<string>(type: "text", nullable: true),
                    vehicle_type = table.Column<string>(type: "text", nullable: true),
                    vehicle_types_id = table.Column<long>(type: "bigint", nullable: false),
                    vehicle_unqiue_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle", x => x.Id);
                });
        }
    }
}
