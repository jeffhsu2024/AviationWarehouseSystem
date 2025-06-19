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
                    SupplierCode = "TTL001",
                    ContactPerson = "王大明",
                    EnglishName = "Taiwan Tobacco & Liquor Corporation",
                    Phone = "02-12345678",
                    Address = "台北市中正區",
                    ContactEmail = "contact@ttl.com.tw",
                    Email = "info@ttl.com.tw",
                    ContactPhone = "02-12345678",
                    Fax = "02-12345679",
                    TaxNumber = "12345678",
                    PaymentTerms = 30,
                    CreditLimit = 1000000.00m,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Remarks = "主要供應商",
                    Website = "https://www.ttl.com.tw/"
                },
                new Supplier
                {
                    Id = 2,
                    SupplierName = "環球貿易有限公司",
                    SupplierCode = "GLB001",
                    ContactPerson = "李經理",
                    EnglishName = "Global Trading Co., Ltd.",
                    Phone = "07-1234-5678",
                    Address = "高雄市前鎮區中山二路123號",
                    ContactEmail = "manager@global.com.tw",
                    Email = "info@global.com.tw",
                    ContactPhone = "0987-654-321",
                    Fax = "07-1234-5679",
                    TaxNumber = "87654321",
                    PaymentTerms = 45,
                    CreditLimit = 500000.00m,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Remarks = "多元化商品供應商",
                    Website = "www.global.com.tw"
                }
            );

            // TaxCategory 種子資料
            modelBuilder.Entity<TaxCategory>().HasData(
                new TaxCategory
                {
                    Id = 1,
                    CategoryName = "電子產品",
                    EnglishName = "Electronic Products",
                    CategoryCode = "ELEC001",
                    TariffCode = "8517120000",
                    TaxRate = 5.0m,
                    Description = "各類電子產品及配件",
                    ControlDescription = "無特殊管制",
                    SortOrder = 1,
                    Remarks = "一般電子產品稅率",
                    IsActive = true
                },
                new TaxCategory
                {
                    Id = 2,
                    CategoryName = "服飾用品",
                    EnglishName = "Clothing & Accessories",
                    CategoryCode = "CLTH001",
                    TariffCode = "6203420000",
                    TaxRate = 12.0m,
                    Description = "各類服飾及配件",
                    ControlDescription = "無特殊管制",
                    SortOrder = 2,
                    Remarks = "服飾用品稅率",
                    IsActive = true
                },
                new TaxCategory
                {
                    Id = 3,
                    CategoryName = "食品飲料",
                    EnglishName = "Food & Beverages",
                    CategoryCode = "FOOD001",
                    TariffCode = "2202100000",
                    TaxRate = 8.0m,
                    Description = "各類食品及飲料",
                    ControlDescription = "需符合食品安全規範",
                    SortOrder = 3,
                    Remarks = "食品飲料稅率",
                    IsActive = true
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
                    LocationCode = "A001",
                    LocationName = "A區第1排第1層",
                    StorageType = StorageType.General,
                    WarehouseZone = WarehouseZone.AZone,
                    Floor = "1F",
                    Rack = "A",
                    Row = "01",
                    Level = "1",
                    Position = "01",
                    CreatedBy = "system",
                    UpdatedBy = "system",
                    Remarks = "一般貨物儲位",
                    IsActive = true
                },
                new StorageLocation
                {
                    Id = 2,
                    LocationCode = "B001",
                    LocationName = "B區第1排第1層",
                    StorageType = StorageType.Cold,
                    WarehouseZone = WarehouseZone.BZone,
                    Floor = "1F",
                    Rack = "B",
                    Row = "01",
                    Level = "1",
                    Position = "01",
                    CreatedBy = "system",
                    UpdatedBy = "system",
                    Remarks = "冷藏貨物儲位",
                    IsActive = true
                },
                new StorageLocation
                {
                    Id = 3,
                    LocationCode = "C001",
                    LocationName = "C區第1排第1層",
                    StorageType = StorageType.Hazardous,
                    WarehouseZone = WarehouseZone.GeneralZone,
                    Floor = "1F",
                    Rack = "C",
                    Row = "01",
                    Level = "1",
                    Position = "01",
                    CreatedBy = "system",
                    UpdatedBy = "system",
                    Remarks = "危險品儲位",
                    IsActive = true
                }
            );

        }
    }
}
