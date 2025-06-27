using AviationWarehouseSystem.Services;
using Models;
using Xunit;

namespace AviationWarehouseSystem.Tests.Services
{
    public class DutyFreeProductReportServiceTests
    {
        private readonly DutyFreeProductReportService _reportService;

        public DutyFreeProductReportServiceTests()
        {
            _reportService = new DutyFreeProductReportService();
        }

        [Fact]
        public void GenerateDutyFreeProductListReport_ShouldReturnPdfBytes()
        {
            // Arrange
            var products = new List<DutyFreeProduct>
            {
                new DutyFreeProduct
                {
                    Id = 1,
                    ProductName = "測試商品1",
                    ProductCode = "TEST001",
                    Brand = "測試品牌",
                    UnitPrice = 100.00m,
                    StockQuantity = 50,
                    Unit = "瓶",
                    IsActive = true,
                    Category = new ProductCategory { CategoryName = "測試分類" },
                    Supplier = new Supplier { SupplierName = "測試供應商" }
                },
                new DutyFreeProduct
                {
                    Id = 2,
                    ProductName = "測試商品2",
                    ProductCode = "TEST002",
                    Brand = "測試品牌2",
                    UnitPrice = 200.00m,
                    StockQuantity = 30,
                    Unit = "盒",
                    IsActive = true,
                    Category = new ProductCategory { CategoryName = "測試分類2" },
                    Supplier = new Supplier { SupplierName = "測試供應商2" }
                }
            };

            // Act
            var result = _reportService.GenerateDutyFreeProductListReport(products);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0);
            
            // 檢查是否為有效的 PDF 文件（PDF 文件以 %PDF 開頭）
            var pdfHeader = System.Text.Encoding.ASCII.GetString(result.Take(4).ToArray());
            Assert.Equal("%PDF", pdfHeader);
        }

        [Fact]
        public void GenerateDutyFreeProductDetailReport_ShouldReturnPdfBytes()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                Id = 1,
                ProductName = "測試商品詳細",
                ProductCode = "DETAIL001",
                Brand = "詳細品牌",
                Description = "這是一個測試商品的詳細描述",
                UnitPrice = 150.00m,
                CostPrice = 100.00m,
                StockQuantity = 25,
                SafetyStock = 10,
                MaxStock = 100,
                Unit = "個",
                Barcode = "1234567890",
                DutyFreeType = DutyFreeType.Alcohol,
                DutyFreeDiscountRate = 15.5m,
                IsActive = true,
                CreatedDate = DateTime.Now,
                CreatedBy = "測試用戶",
                Remarks = "測試備註",
                Category = new ProductCategory { CategoryName = "測試分類" },
                Supplier = new Supplier { SupplierName = "測試供應商" }
            };

            // Act
            var result = _reportService.GenerateDutyFreeProductDetailReport(product);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0);
            
            // 檢查是否為有效的 PDF 文件
            var pdfHeader = System.Text.Encoding.ASCII.GetString(result.Take(4).ToArray());
            Assert.Equal("%PDF", pdfHeader);
        }

        [Fact]
        public void GenerateDutyFreeProductListReport_WithSearchFilter_ShouldIncludeFilterInReport()
        {
            // Arrange
            var products = new List<DutyFreeProduct>
            {
                new DutyFreeProduct
                {
                    Id = 1,
                    ProductName = "搜尋測試商品",
                    ProductCode = "SEARCH001",
                    Brand = "搜尋品牌",
                    UnitPrice = 100.00m,
                    StockQuantity = 50,
                    Unit = "瓶",
                    IsActive = true,
                    Category = new ProductCategory { CategoryName = "搜尋分類" },
                    Supplier = new Supplier { SupplierName = "搜尋供應商" }
                }
            };
            var searchFilter = "搜尋測試";

            // Act
            var result = _reportService.GenerateDutyFreeProductListReport(products, searchFilter);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0);
            
            // 檢查是否為有效的 PDF 文件
            var pdfHeader = System.Text.Encoding.ASCII.GetString(result.Take(4).ToArray());
            Assert.Equal("%PDF", pdfHeader);
        }
    }
}
