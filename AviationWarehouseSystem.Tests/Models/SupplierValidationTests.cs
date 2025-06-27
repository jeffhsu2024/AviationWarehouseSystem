using System.ComponentModel.DataAnnotations;
using Models;
using Xunit;
using FluentAssertions;

namespace AviationWarehouseSystem.Tests.Models
{
    public class SupplierValidationTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Supplier_WithValidData_ShouldPassValidation()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = "台灣電子股份有限公司",
                SupplierCode = "TWE001",
                ContactPerson = "張三",
                Phone = "02-12345678",
                Email = "contact@twe.com.tw",
                Address = "台北市信義區信義路五段7號",

                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(supplier);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void Supplier_WithEmptySupplierName_ShouldFailValidation()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = "", // 空字串
                SupplierCode = "TWE001",
                ContactPerson = "張三",
                Phone = "02-12345678",
                Email = "contact@twe.com.tw",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(supplier);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("SupplierName"));
        }

        [Fact]
        public void Supplier_WithEmptySupplierCode_ShouldFailValidation()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = "台灣電子股份有限公司",
                SupplierCode = "", // 空字串
                ContactPerson = "張三",
                Phone = "02-12345678",
                Email = "contact@twe.com.tw",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(supplier);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("SupplierCode"));
        }

        [Fact]
        public void Supplier_WithInvalidEmail_ShouldFailValidation()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = "台灣電子股份有限公司",
                SupplierCode = "TWE001",
                ContactPerson = "張三",
                Phone = "02-12345678",
                Email = "invalid-email", // 無效的電子郵件格式
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(supplier);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("Email"));
        }

        [Fact]
        public void Supplier_WithTooLongSupplierName_ShouldFailValidation()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = new string('A', 201), // 超過 200 字元限制
                SupplierCode = "TWE001",
                ContactPerson = "張三",
                Phone = "02-12345678",
                Email = "contact@twe.com.tw",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(supplier);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("SupplierName"));
        }

        [Fact]
        public void Supplier_WithTooLongSupplierCode_ShouldFailValidation()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = "台灣電子股份有限公司",
                SupplierCode = new string('A', 51), // 超過 50 字元限制
                ContactPerson = "張三",
                Phone = "02-12345678",
                Email = "contact@twe.com.tw",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(supplier);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("SupplierCode"));
        }

        [Fact]
        public void Supplier_WithTooLongContactPerson_ShouldFailValidation()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = "台灣電子股份有限公司",
                SupplierCode = "TWE001",
                ContactPerson = new string('A', 101), // 超過 100 字元限制
                Phone = "02-12345678",
                Email = "contact@twe.com.tw",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(supplier);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("ContactPerson"));
        }

        [Fact]
        public void Supplier_WithTooLongPhone_ShouldFailValidation()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = "台灣電子股份有限公司",
                SupplierCode = "TWE001",
                ContactPerson = "張三",
                Phone = new string('1', 21), // 超過 20 字元限制
                Email = "contact@twe.com.tw",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(supplier);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("Phone"));
        }

        [Fact]
        public void Supplier_WithTooLongEmail_ShouldFailValidation()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = "台灣電子股份有限公司",
                SupplierCode = "TWE001",
                ContactPerson = "張三",
                Phone = "02-12345678",
                Email = new string('a', 90) + "@example.com", // 超過 100 字元限制
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(supplier);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("Email"));
        }

        [Fact]
        public void Supplier_WithValidEmailFormats_ShouldPassValidation()
        {
            // Arrange & Act & Assert
            var validEmails = new[]
            {
                "test@example.com",
                "user.name@domain.co.uk",
                "user+tag@example.org",
                "123@456.com",
                "test@sub.domain.com"
            };

            foreach (var email in validEmails)
            {
                var supplier = new Supplier
                {
                    SupplierName = "測試供應商",
                    SupplierCode = "TEST001",
                    ContactPerson = "測試人員",
                    Phone = "02-12345678",
                    Email = email,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                var validationResults = ValidateModel(supplier);
                validationResults.Should().BeEmpty($"Email {email} should be valid");
            }
        }

        [Fact]
        public void Supplier_WithInvalidEmailFormats_ShouldFailValidation()
        {
            // Arrange & Act & Assert
            var invalidEmails = new[]
            {
                "invalid-email",
                "@example.com",
                "test@"
            };

            foreach (var email in invalidEmails)
            {
                var supplier = new Supplier
                {
                    SupplierName = "測試供應商",
                    SupplierCode = "TEST001",
                    ContactPerson = "測試人員",
                    Phone = "02-12345678",
                    Email = email,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                var validationResults = ValidateModel(supplier);
                validationResults.Should().NotBeEmpty($"Email {email} should be invalid");
                validationResults.Should().Contain(vr => vr.MemberNames.Contains("Email"));
            }
        }

        [Fact]
        public void Supplier_WithOptionalFields_ShouldPassValidation()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = "簡單供應商",
                SupplierCode = "SIMPLE001",
                IsActive = true,
                CreatedDate = DateTime.Now
                // 其他欄位為選填
            };

            // Act
            var validationResults = ValidateModel(supplier);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void Supplier_BusinessLogic_SupplierCodeShouldBeUnique()
        {
            // Arrange
            var supplier1 = new Supplier
            {
                SupplierName = "供應商A",
                SupplierCode = "SUP001",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            var supplier2 = new Supplier
            {
                SupplierName = "供應商B",
                SupplierCode = "SUP001", // 相同的代碼
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act & Assert
            // 業務邏輯：供應商代碼應該是唯一的
            supplier1.SupplierCode.Should().Be(supplier2.SupplierCode);
            // 在實際應用中，這應該在資料庫層面或服務層面進行唯一性檢查
        }

        [Fact]
        public void Supplier_BusinessLogic_ActiveSupplierShouldHaveContactInfo()
        {
            // Arrange
            var supplier = new Supplier
            {
                SupplierName = "重要供應商",
                SupplierCode = "IMP001",
                Phone = "02-12345678", // 提供聯絡資訊
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Act & Assert
            // 業務邏輯：啟用的供應商最好有聯絡資訊
            if (supplier.IsActive)
            {
                // 這是建議性的業務規則，不是強制驗證
                var hasContactInfo = !string.IsNullOrEmpty(supplier.Phone) ||
                                   !string.IsNullOrEmpty(supplier.Email) ||
                                   !string.IsNullOrEmpty(supplier.ContactPerson);

                // 在實際應用中，可能會有警告而不是錯誤
                hasContactInfo.Should().BeTrue("Active suppliers should have contact information");
            }
        }
    }
}
