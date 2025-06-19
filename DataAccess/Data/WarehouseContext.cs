using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
        {

        }
        public DbSet<DutyFreeProduct> DutyFreeProducts { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<TaxableGoods> TaxableGoods { get; set; }
        public DbSet<CustomsDeclaration> CustomsDeclarations { get; set; }
        public DbSet<TaxCategory> TaxCategories { get; set; }
        public DbSet<StorageLocation> StorageLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // DutyFreeProduct 索引
            modelBuilder.Entity<DutyFreeProduct>()
                .HasIndex(p => p.ProductCode)
                .IsUnique();

            modelBuilder.Entity<DutyFreeProduct>()
                .HasIndex(p => p.ProductName);

            // ProductCategory 索引
            modelBuilder.Entity<ProductCategory>()
                .HasIndex(c => c.CategoryCode)
                .IsUnique();

            modelBuilder.Entity<ProductCategory>()
                .HasIndex(c => c.CategoryName);

            // StorageLocation 索引
            modelBuilder.Entity<StorageLocation>()
                .HasIndex(l => l.LocationCode)
                .IsUnique();

            // CustomsDeclaration 索引
            modelBuilder.Entity<CustomsDeclaration>()
                .HasIndex(d => d.DeclarationNumber)
                .IsUnique();


            modelBuilder.Entity<DutyFreeProduct>()
                .HasOne(p => p.StorageLocation)
                .WithMany()
                .HasForeignKey(p => p.StorageLocationId);

            // 種子資料
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory
                {
                    Id = 1,
                    CategoryName = "香菸",
                    CategoryCode = "CIG",
                    Description = "香菸類商品",
                    SortOrder = 1,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Remarks = ""
                },
                new ProductCategory
                {
                    Id = 2,
                    CategoryName = "酒類",
                    CategoryCode = "ALC",
                    Description = "酒類商品",
                    SortOrder = 2,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Remarks = ""
                }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier
                {
                    Id = 1,
                    SupplierName = "台灣菸酒公司",
                    ContactPerson = "王大明",
                    EnglishName = "Mr. Wang",
                    Phone = "02-12345678",
                    Address = "台北市中正區",
                    ContactEmail = "123456@mail.ttl.com.tw",
                    Email = "123456@mail.ttl.com.tw",
                    ContactPhone = "02-12345678",
                    Fax = "",
                    TaxNumber = "",
                    SupplierCode = "",
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Remarks = "",
                    Website = @"https://www.ttl.com.tw/"
                }
            );

            modelBuilder.Entity<DutyFreeProduct>().HasData(
                new DutyFreeProduct
                {
                    Id = 1,
                    ProductName = "台灣啤酒",
                    ProductCode = "TWBEER001",
                    Brand = "台啤",
                    Description = "台灣啤酒 330ml",
                    UnitPrice = 30,
                    CostPrice = 20,
                    StockQuantity = 100,
                    SafetyStock = 10,
                    MaxStock = 500,
                    Unit = "瓶",
                    Barcode = "4711234567890",
                    CategoryId = 2,      // 參考 ProductCategory 種子
                    SupplierId = 1,      // 參考 Supplier 種子
                    DutyFreeType = Models.DutyFreeType.TobaccoAlcohol,
                    DutyFreeDiscountRate = 0,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    CreatedBy = "system", // 必填
                    UpdatedBy = "system",
                    UpdatedDate = DateTime.UtcNow,
                    Remarks = "" // 這裡改為空字串
                }
            );

            modelBuilder.Entity<StorageLocation>().HasData(
                new StorageLocation
                {
                    Id = 1,
                    LocationCode = "A01",
                    LocationName = "主倉A01",
                    CreatedBy = "system",
                    UpdatedBy = "system",
                    Floor = "F1B",
                    Position = "50",
                    Level = "T0",
                    Rack = "ICE",
                    Row = "R1",
                    Remarks = ""
                }
            );

        }
    }
}
