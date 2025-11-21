using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Morpho.Migrations
{
    /// <inheritdoc />
    public partial class removeDevicetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "device_type");

            migrationBuilder.DropColumn(
                name: "device_type_id",
                table: "tracking_devices");

            migrationBuilder.RenameColumn(
                name: "model_no",
                table: "tracking_devices",
                newName: "device_type_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "device_type_name",
                table: "tracking_devices",
                newName: "model_no");

            migrationBuilder.AddColumn<long>(
                name: "device_type_id",
                table: "tracking_devices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "device_type",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    active_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    active_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<long>(type: "bigint", nullable: true),
                    device_type_name = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    remark = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_type", x => x.Id);
                });
        }
    }
}
