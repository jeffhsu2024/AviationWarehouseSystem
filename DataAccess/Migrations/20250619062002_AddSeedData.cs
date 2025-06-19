using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DutyFreeProducts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 6, 19, 6, 20, 2, 374, DateTimeKind.Utc).AddTicks(9973), new DateTime(2025, 6, 19, 6, 20, 2, 374, DateTimeKind.Utc).AddTicks(9974) });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 19, 6, 20, 2, 374, DateTimeKind.Utc).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 6, 19, 6, 20, 2, 374, DateTimeKind.Utc).AddTicks(9816));

            migrationBuilder.UpdateData(
                table: "StorageLocations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Floor", "Level", "LocationCode", "LocationName", "Position", "Rack", "Remarks", "Row", "WarehouseZone" },
                values: new object[] { new DateTime(2025, 6, 19, 6, 20, 2, 374, DateTimeKind.Utc).AddTicks(9987), "1F", "1", "A001", "A區第1排第1層", "01", "A", "一般貨物儲位", "01", 1 });

            migrationBuilder.InsertData(
                table: "StorageLocations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Floor", "IsActive", "Level", "LocationCode", "LocationName", "Position", "Rack", "Remarks", "Row", "StorageType", "UpdatedAt", "UpdatedBy", "WarehouseZone" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 6, 19, 6, 20, 2, 374, DateTimeKind.Utc).AddTicks(9990), "system", "1F", true, "1", "B001", "B區第1排第1層", "01", "B", "冷藏貨物儲位", "01", 1, null, "system", 2 },
                    { 3, new DateTime(2025, 6, 19, 6, 20, 2, 374, DateTimeKind.Utc).AddTicks(9993), "system", "1F", true, "1", "C001", "C區第1排第1層", "01", "C", "危險品儲位", "01", 2, null, "system", 0 }
                });

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ContactEmail", "CreatedDate", "CreditLimit", "Email", "EnglishName", "Fax", "Remarks", "SupplierCode", "TaxNumber" },
                values: new object[] { "contact@ttl.com.tw", new DateTime(2025, 6, 19, 6, 20, 2, 374, DateTimeKind.Utc).AddTicks(9924), 1000000.00m, "info@ttl.com.tw", "Taiwan Tobacco & Liquor Corporation", "02-12345679", "主要供應商", "TTL001", "12345678" });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "ContactEmail", "ContactPerson", "ContactPhone", "CreatedDate", "CreditLimit", "Email", "EnglishName", "Fax", "IsActive", "PaymentTerms", "Phone", "Remarks", "SupplierCode", "SupplierName", "SupplierType", "TaxNumber", "UpdatedDate", "Website" },
                values: new object[] { 2, "高雄市前鎮區中山二路123號", "manager@global.com.tw", "李經理", "0987-654-321", new DateTime(2025, 6, 19, 6, 20, 2, 374, DateTimeKind.Utc).AddTicks(9929), 500000.00m, "info@global.com.tw", "Global Trading Co., Ltd.", "07-1234-5679", true, 45, "07-1234-5678", "多元化商品供應商", "GLB001", "環球貿易有限公司", 0, "87654321", null, "www.global.com.tw" });

            migrationBuilder.InsertData(
                table: "TaxCategories",
                columns: new[] { "Id", "BusinessTaxRate", "CategoryCode", "CategoryName", "ControlDescription", "CreatedDate", "Description", "EnglishName", "IsActive", "IsControlledItem", "ParentCategoryId", "Remarks", "RequiresPermit", "SortOrder", "TariffCode", "TaxRate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 5.0m, "ELEC001", "電子產品", "無特殊管制", new DateTime(2025, 6, 19, 14, 20, 2, 374, DateTimeKind.Local).AddTicks(9946), "各類電子產品及配件", "Electronic Products", true, false, null, "一般電子產品稅率", false, 1, "8517120000", 5.0m, null },
                    { 2, 5.0m, "CLTH001", "服飾用品", "無特殊管制", new DateTime(2025, 6, 19, 14, 20, 2, 374, DateTimeKind.Local).AddTicks(9951), "各類服飾及配件", "Clothing & Accessories", true, false, null, "服飾用品稅率", false, 2, "6203420000", 12.0m, null },
                    { 3, 5.0m, "FOOD001", "食品飲料", "需符合食品安全規範", new DateTime(2025, 6, 19, 14, 20, 2, 374, DateTimeKind.Local).AddTicks(9954), "各類食品及飲料", "Food & Beverages", true, false, null, "食品飲料稅率", false, 3, "2202100000", 8.0m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StorageLocations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StorageLocations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaxCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaxCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaxCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "DutyFreeProducts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 6, 19, 3, 29, 52, 925, DateTimeKind.Utc).AddTicks(2873), new DateTime(2025, 6, 19, 3, 29, 52, 925, DateTimeKind.Utc).AddTicks(2874) });

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
                columns: new[] { "CreatedAt", "Floor", "Level", "LocationCode", "LocationName", "Position", "Rack", "Remarks", "Row", "WarehouseZone" },
                values: new object[] { new DateTime(2025, 6, 19, 3, 29, 52, 925, DateTimeKind.Utc).AddTicks(2895), "F1B", "T0", "A01", "主倉A01", "50", "ICE", "", "R1", 0 });

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ContactEmail", "CreatedDate", "CreditLimit", "Email", "EnglishName", "Fax", "Remarks", "SupplierCode", "TaxNumber" },
                values: new object[] { "123456@mail.ttl.com.tw", new DateTime(2025, 6, 19, 3, 29, 52, 925, DateTimeKind.Utc).AddTicks(2848), 0m, "123456@mail.ttl.com.tw", "Mr. Wang", "", "", "", "" });
        }
    }
}
