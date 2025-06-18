using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IndexWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DutyFreeProducts_productCategories_CategoryId",
                table: "DutyFreeProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_productCategories_productCategories_ParentCategoryId",
                table: "productCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productCategories",
                table: "productCategories");

            migrationBuilder.RenameTable(
                name: "productCategories",
                newName: "ProductCategories");

            migrationBuilder.RenameIndex(
                name: "IX_productCategories_ParentCategoryId",
                table: "ProductCategories",
                newName: "IX_ProductCategories_ParentCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                column: "Id");

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "CreatedDate", "Description", "IsActive", "ParentCategoryId", "Remarks", "SortOrder" },
                values: new object[,]
                {
                    { 1, "CIG", "香菸", new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3682), "香菸類商品", true, null, "", 1 },
                    { 2, "ALC", "酒類", new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3684), "酒類商品", true, null, "", 2 }
                });

            migrationBuilder.InsertData(
                table: "StorageLocations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Floor", "IsActive", "Level", "LocationCode", "LocationName", "Position", "Rack", "Remarks", "Row", "StorageType", "UpdatedAt", "UpdatedBy", "WarehouseZone" },
                values: new object[] { 1, new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3848), "system", "F1B", true, "T0", "A01", "主倉A01", "50", "ICE", "", "R1", 0, null, "system", 0 });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "ContactEmail", "ContactPerson", "ContactPhone", "CreatedDate", "CreditLimit", "Email", "EnglishName", "Fax", "IsActive", "PaymentTerms", "Phone", "Remarks", "SupplierCode", "SupplierName", "SupplierType", "TaxNumber", "UpdatedDate", "Website" },
                values: new object[] { 1, "台北市中正區", "123456@mail.ttl.com.tw", "王大明", "02-12345678", new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3809), 0m, "123456@mail.ttl.com.tw", "Mr. Wang", "", true, 30, "02-12345678", "", "", "台灣菸酒公司", 0, "", null, "https://www.ttl.com.tw/" });

            migrationBuilder.InsertData(
                table: "DutyFreeProducts",
                columns: new[] { "Id", "Barcode", "Brand", "CategoryId", "CostPrice", "CreatedBy", "CreatedDate", "Description", "DutyFreeDiscountRate", "DutyFreeType", "IsActive", "MaxStock", "ProductCode", "ProductName", "Remarks", "SafetyStock", "StockQuantity", "SupplierId", "Unit", "UnitPrice", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, "4711234567890", "台啤", 2, 20m, "system", new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3832), "台灣啤酒 330ml", 0m, 1, true, 500, "TWBEER001", "台灣啤酒", "", 10, 100, 1, "瓶", 30m, "system", new DateTime(2025, 6, 17, 11, 28, 8, 9, DateTimeKind.Utc).AddTicks(3833) });

            migrationBuilder.CreateIndex(
                name: "IX_StorageLocations_LocationCode",
                table: "StorageLocations",
                column: "LocationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryCode",
                table: "ProductCategories",
                column: "CategoryCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryName",
                table: "ProductCategories",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_DutyFreeProducts_ProductCode",
                table: "DutyFreeProducts",
                column: "ProductCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DutyFreeProducts_ProductName",
                table: "DutyFreeProducts",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_CustomsDeclarations_DeclarationNumber",
                table: "CustomsDeclarations",
                column: "DeclarationNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DutyFreeProducts_ProductCategories_CategoryId",
                table: "DutyFreeProducts",
                column: "CategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_ProductCategories_ParentCategoryId",
                table: "ProductCategories",
                column: "ParentCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DutyFreeProducts_ProductCategories_CategoryId",
                table: "DutyFreeProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_ProductCategories_ParentCategoryId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_StorageLocations_LocationCode",
                table: "StorageLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_CategoryCode",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_CategoryName",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_DutyFreeProducts_ProductCode",
                table: "DutyFreeProducts");

            migrationBuilder.DropIndex(
                name: "IX_DutyFreeProducts_ProductName",
                table: "DutyFreeProducts");

            migrationBuilder.DropIndex(
                name: "IX_CustomsDeclarations_DeclarationNumber",
                table: "CustomsDeclarations");

            migrationBuilder.DeleteData(
                table: "DutyFreeProducts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StorageLocations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "productCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_ParentCategoryId",
                table: "productCategories",
                newName: "IX_productCategories_ParentCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productCategories",
                table: "productCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DutyFreeProducts_productCategories_CategoryId",
                table: "DutyFreeProducts",
                column: "CategoryId",
                principalTable: "productCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productCategories_productCategories_ParentCategoryId",
                table: "productCategories",
                column: "ParentCategoryId",
                principalTable: "productCategories",
                principalColumn: "Id");
        }
    }
}
