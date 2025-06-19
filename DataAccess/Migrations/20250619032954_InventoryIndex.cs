using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InventoryIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorageLocationId",
                table: "DutyFreeProducts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DutyFreeProducts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "StorageLocationId", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 6, 19, 3, 29, 52, 925, DateTimeKind.Utc).AddTicks(2873), null, new DateTime(2025, 6, 19, 3, 29, 52, 925, DateTimeKind.Utc).AddTicks(2874) });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 19, 3, 29, 52, 925, DateTimeKind.Utc).AddTicks(2688));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 19, 3, 29, 52, 925, DateTimeKind.Utc).AddTicks(2692));

            migrationBuilder.UpdateData(
                table: "StorageLocations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 19, 3, 29, 52, 925, DateTimeKind.Utc).AddTicks(2895));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 19, 3, 29, 52, 925, DateTimeKind.Utc).AddTicks(2848));

            migrationBuilder.CreateIndex(
                name: "IX_DutyFreeProducts_StorageLocationId",
                table: "DutyFreeProducts",
                column: "StorageLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DutyFreeProducts_StorageLocations_StorageLocationId",
                table: "DutyFreeProducts",
                column: "StorageLocationId",
                principalTable: "StorageLocations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DutyFreeProducts_StorageLocations_StorageLocationId",
                table: "DutyFreeProducts");

            migrationBuilder.DropIndex(
                name: "IX_DutyFreeProducts_StorageLocationId",
                table: "DutyFreeProducts");

            migrationBuilder.DropColumn(
                name: "StorageLocationId",
                table: "DutyFreeProducts");

            migrationBuilder.UpdateData(
                table: "DutyFreeProducts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 6, 17, 14, 20, 14, 401, DateTimeKind.Utc).AddTicks(6143), new DateTime(2025, 6, 17, 14, 20, 14, 401, DateTimeKind.Utc).AddTicks(6144) });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 17, 14, 20, 14, 401, DateTimeKind.Utc).AddTicks(5970));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 17, 14, 20, 14, 401, DateTimeKind.Utc).AddTicks(5974));

            migrationBuilder.UpdateData(
                table: "StorageLocations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 17, 14, 20, 14, 401, DateTimeKind.Utc).AddTicks(6164));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 17, 14, 20, 14, 401, DateTimeKind.Utc).AddTicks(6118));
        }
    }
}
