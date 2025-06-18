using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixedAddDutyFreeProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DutyFreeProducts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3832), new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3833) });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3682));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3684));

            migrationBuilder.UpdateData(
                table: "StorageLocations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3848));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3809));
        }
    }
}
