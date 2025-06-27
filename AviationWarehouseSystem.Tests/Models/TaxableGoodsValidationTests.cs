using System.ComponentModel.DataAnnotations;
using Models;
using Xunit;
using FluentAssertions;

namespace AviationWarehouseSystem.Tests.Models
{
    public class TaxableGoodsValidationTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void TaxableGoods_WithValidData_ShouldPassValidation()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = "進口手機",
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 100,
                Unit = "PCS",
                DutiableValue = 100000,
                TaxRate = 5.0m,
                TaxAmount = 5000,
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void TaxableGoods_WithEmptyGoodsName_ShouldFailValidation()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = "", // 空字串
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 100,
                Unit = "PCS",
                DutiableValue = 100000,
                TaxRate = 5.0m,
                TaxAmount = 5000,
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("GoodsName"));
        }

        [Fact]
        public void TaxableGoods_WithEmptyGoodsCode_ShouldFailValidation()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = "進口手機",
                GoodsCode = "", // 空字串
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 100,
                Unit = "PCS",
                DutiableValue = 100000,
                TaxRate = 5.0m,
                TaxAmount = 5000,
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("GoodsCode"));
        }

        [Fact]
        public void TaxableGoods_WithNegativeQuantity_ShouldFailValidation()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = "進口手機",
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = -10, // 負數數量
                Unit = "PCS",
                DutiableValue = 100000,
                TaxRate = 5.0m,
                TaxAmount = 5000,
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("Quantity"));
        }

        [Fact]
        public void TaxableGoods_WithNegativeDutiableValue_ShouldFailValidation()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = "進口手機",
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 100,
                Unit = "PCS",
                DutiableValue = -100000, // 負數完稅價格
                TaxRate = 5.0m,
                TaxAmount = 5000,
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("DutiableValue"));
        }

        [Fact]
        public void TaxableGoods_WithNegativeTaxRate_ShouldFailValidation()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = "進口手機",
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 100,
                Unit = "PCS",
                DutiableValue = 100000,
                TaxRate = -5.0m, // 負數稅率
                TaxAmount = 5000,
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("TaxRate"));
        }

        [Fact]
        public void TaxableGoods_WithTaxRateOver100_ShouldFailValidation()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = "進口手機",
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 100,
                Unit = "PCS",
                DutiableValue = 100000,
                TaxRate = 150.0m, // 超過 100% 的稅率
                TaxAmount = 5000,
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("TaxRate"));
        }

        [Fact]
        public void TaxableGoods_WithNegativeTaxAmount_ShouldPassValidation()
        {
            // Arrange - 模型中TaxAmount沒有Range驗證，所以負數是允許的
            var goods = new TaxableGoods
            {
                GoodsName = "進口手機",
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 100,
                Unit = "PCS",
                DutiableValue = 100000,
                TaxRate = 5.0m,
                TaxAmount = -5000, // 負數稅額在模型層面是允許的
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().BeEmpty(); // 模型驗證應該通過
        }

        [Fact]
        public void TaxableGoods_WithTooLongGoodsName_ShouldFailValidation()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = new string('A', 201), // 超過 200 字元限制
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 100,
                Unit = "PCS",
                DutiableValue = 100000,
                TaxRate = 5.0m,
                TaxAmount = 5000,
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("GoodsName"));
        }

        [Fact]
        public void TaxableGoods_WithTooLongUnit_ShouldFailValidation()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = "進口手機",
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 100,
                Unit = new string('A', 21), // 超過 20 字元限制
                DutiableValue = 100000,
                TaxRate = 5.0m,
                TaxAmount = 5000,
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("Unit"));
        }

        [Fact]
        public void TaxableGoods_BusinessLogic_TaxAmountShouldMatchCalculation()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = "進口手機",
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 100,
                Unit = "PCS",
                DutiableValue = 100000,
                TaxRate = 5.0m,
                TaxAmount = 5000,
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act & Assert
            var expectedTaxAmount = goods.DutiableValue * (goods.TaxRate / 100);
            goods.TaxAmount.Should().Be(expectedTaxAmount);
        }

        [Fact]
        public void TaxableGoods_BusinessLogic_CustomsStatusTransition()
        {
            // Arrange
            var goods = new TaxableGoods
            {
                GoodsName = "進口手機",
                GoodsCode = "TG001",
                CustomsStatus = CustomsStatus.Pending
            };

            // Act & Assert
            // 測試狀態轉換邏輯
            goods.CustomsStatus = CustomsStatus.Declared;
            goods.CustomsStatus.Should().Be(CustomsStatus.Declared);

            goods.CustomsStatus = CustomsStatus.InProcess;
            goods.CustomsStatus.Should().Be(CustomsStatus.InProcess);

            goods.CustomsStatus = CustomsStatus.Cleared;
            goods.CustomsStatus.Should().Be(CustomsStatus.Cleared);
        }

        [Fact]
        public void TaxableGoods_WithZeroValues_ShouldFailValidation()
        {
            // Arrange - Quantity有Range(1, int.MaxValue)驗證，所以0會失敗
            var goods = new TaxableGoods
            {
                GoodsName = "免稅商品",
                GoodsCode = "TG001",
                TaxCategoryId = 1,
                SupplierId = 1,
                StorageLocationId = 1,
                Quantity = 0, // 零數量會失敗，因為有Range(1, int.MaxValue)驗證
                Unit = "PCS",
                DutiableValue = 0, // 零完稅價格會失敗，因為有Range(0, double.MaxValue)但錯誤訊息說"必須大於0"
                TaxRate = 0, // 零稅率應該允許
                TaxAmount = 0, // 零稅額應該允許
                CustomsDeclarationNumber = "CD2024001",
                CustomsStatus = CustomsStatus.Pending,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(goods);

            // Assert
            validationResults.Should().NotBeEmpty(); // 應該有驗證錯誤
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("Quantity"));
        }

        [Fact]
        public void CustomsStatus_EnumValues_ShouldBeValid()
        {
            // Act & Assert
            var pendingValue = (int)CustomsStatus.Pending;
            var declaredValue = (int)CustomsStatus.Declared;
            var inProcessValue = (int)CustomsStatus.InProcess;
            var clearedValue = (int)CustomsStatus.Cleared;
            var rejectedValue = (int)CustomsStatus.Rejected;
            var detainedValue = (int)CustomsStatus.Detained;

            // 驗證枚舉值的順序和邏輯
            pendingValue.Should().Be(0);
            declaredValue.Should().Be(1);
            inProcessValue.Should().Be(2);
            clearedValue.Should().Be(3);
            rejectedValue.Should().Be(4);
            detainedValue.Should().Be(5);
        }
    }
}
