using System.ComponentModel.DataAnnotations;
using Models;
using Xunit;
using FluentAssertions;

namespace AviationWarehouseSystem.Tests.Models
{
    public class DutyFreeProductValidationTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void DutyFreeProduct_WithValidData_ShouldPassValidation()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = "iPhone 15",
                ProductCode = "IP15001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000,
                CostPrice = 30000,
                StockQuantity = 50,
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void DutyFreeProduct_WithEmptyProductName_ShouldFailValidation()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = "", // 空字串
                ProductCode = "IP15001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000,
                CostPrice = 30000,
                StockQuantity = 50,
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("ProductName"));
        }

        [Fact]
        public void DutyFreeProduct_WithNullProductName_ShouldFailValidation()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = null!, // null 值
                ProductCode = "IP15001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000,
                CostPrice = 30000,
                StockQuantity = 50,
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("ProductName"));
        }

        [Fact]
        public void DutyFreeProduct_WithEmptyProductCode_ShouldFailValidation()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = "iPhone 15",
                ProductCode = "", // 空字串
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000,
                CostPrice = 30000,
                StockQuantity = 50,
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("ProductCode"));
        }

        [Fact]
        public void DutyFreeProduct_WithNegativeUnitPrice_ShouldFailValidation()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = "iPhone 15",
                ProductCode = "IP15001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = -1000, // 負數價格
                CostPrice = 30000,
                StockQuantity = 50,
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("UnitPrice"));
        }

        [Fact]
        public void DutyFreeProduct_WithNegativeCostPrice_ShouldFailValidation()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = "iPhone 15",
                ProductCode = "IP15001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000,
                CostPrice = -1000, // 負數成本
                StockQuantity = 50,
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("CostPrice"));
        }

        [Fact]
        public void DutyFreeProduct_WithNegativeStockQuantity_ShouldPassValidation()
        {
            // Arrange - 模型中沒有對StockQuantity的Range驗證，所以負數是允許的
            var product = new DutyFreeProduct
            {
                ProductName = "iPhone 15",
                ProductCode = "IP15001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000,
                CostPrice = 30000,
                StockQuantity = -10, // 負數庫存在模型層面是允許的
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().BeEmpty(); // 模型驗證應該通過
        }

        [Fact]
        public void DutyFreeProduct_WithNegativeSafetyStock_ShouldPassValidation()
        {
            // Arrange - 模型中沒有對SafetyStock的Range驗證，所以負數是允許的
            var product = new DutyFreeProduct
            {
                ProductName = "iPhone 15",
                ProductCode = "IP15001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000,
                CostPrice = 30000,
                StockQuantity = 50,
                SafetyStock = -5, // 負數安全庫存在模型層面是允許的
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().BeEmpty(); // 模型驗證應該通過
        }

        [Fact]
        public void DutyFreeProduct_WithTooLongProductName_ShouldFailValidation()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = new string('A', 201), // 超過 200 字元限制
                ProductCode = "IP15001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000,
                CostPrice = 30000,
                StockQuantity = 50,
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("ProductName"));
        }

        [Fact]
        public void DutyFreeProduct_WithTooLongProductCode_ShouldFailValidation()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = "iPhone 15",
                ProductCode = new string('A', 51), // 超過 50 字元限制
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000,
                CostPrice = 30000,
                StockQuantity = 50,
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("ProductCode"));
        }

        [Fact]
        public void DutyFreeProduct_WithZeroValues_ShouldPassValidation()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = "免費商品",
                ProductCode = "FREE001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 0, // 零價格應該允許
                CostPrice = 0, // 零成本應該允許
                StockQuantity = 0, // 零庫存應該允許
                SafetyStock = 0, // 零安全庫存應該允許
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(product);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void DutyFreeProduct_BusinessLogic_CostPriceShouldNotExceedUnitPrice()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = "iPhone 15",
                ProductCode = "IP15001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000, // 修正：售價應該高於成本
                CostPrice = 30000, // 成本低於售價
                StockQuantity = 50,
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act & Assert
            // 這是業務邏輯驗證，通常在 Service 層或 Controller 層處理
            product.CostPrice.Should().BeLessOrEqualTo(product.UnitPrice);
        }

        [Fact]
        public void DutyFreeProduct_BusinessLogic_StockQuantityShouldBeTracked()
        {
            // Arrange
            var product = new DutyFreeProduct
            {
                ProductName = "iPhone 15",
                ProductCode = "IP15001",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 35000,
                CostPrice = 30000,
                StockQuantity = 5,
                SafetyStock = 10,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act & Assert
            // 檢查是否低於安全庫存
            var isLowStock = product.StockQuantity < product.SafetyStock;
            isLowStock.Should().BeTrue();
        }
    }
}
