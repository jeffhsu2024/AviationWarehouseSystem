using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Data;
using DataAccess.IService;
using Models;
using AviationWarehouseSystem.Areas.TaxDeclaration.Controllers;
using Xunit;

namespace AviationWarehouseSystem.Tests
{
    public class TaxCategoriesControllerTests : IDisposable
    {
        private readonly WarehouseContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TaxCategoriesController _controller;

        public TaxCategoriesControllerTests()
        {
            // 使用 In-Memory 資料庫進行測試
            var options = new DbContextOptionsBuilder<WarehouseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new WarehouseContext(options);
            _unitOfWork = new UnitOfWork(_context);
            _controller = new TaxCategoriesController(_unitOfWork);

            // 初始化測試資料
            SeedTestData();
        }

        private void SeedTestData()
        {
            var testCategories = new List<TaxCategory>
            {
                new TaxCategory
                {
                    Id = 1,
                    CategoryName = "電子產品",
                    EnglishName = "Electronic Products",
                    CategoryCode = "ELEC001",
                    TariffCode = "8517120000",
                    TaxRate = 5.0m,
                    BusinessTaxRate = 5.0m,
                    Description = "各類電子產品及配件",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new TaxCategory
                {
                    Id = 2,
                    CategoryName = "服飾用品",
                    EnglishName = "Clothing & Accessories",
                    CategoryCode = "CLTH001",
                    TariffCode = "6203420000",
                    TaxRate = 12.0m,
                    BusinessTaxRate = 5.0m,
                    Description = "各類服飾及配件",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            };

            _context.TaxCategories.AddRange(testCategories);
            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsViewWithCategories()
        {
            // Act
            var result = await _controller.Index(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TaxCategory>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Index_WithSearchString_ReturnsFilteredCategories()
        {
            // Act
            var result = await _controller.Index("電子");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TaxCategory>>(viewResult.Model);
            Assert.Single(model);
            Assert.Contains(model, c => c.CategoryName.Contains("電子"));
        }

        [Fact]
        public async Task Details_WithValidId_ReturnsViewWithCategory()
        {
            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<TaxCategory>(viewResult.Model);
            Assert.Equal("電子產品", model.CategoryName);
        }

        [Fact]
        public async Task Details_WithInvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_Get_ReturnsView()
        {
            // Act
            var result = await _controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.ViewData["ParentCategoryId"]);
        }

        [Fact]
        public async Task Create_Post_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var newCategory = new TaxCategory
            {
                CategoryName = "測試分類",
                EnglishName = "Test Category",
                CategoryCode = "TEST001",
                TariffCode = "1234567890",
                TaxRate = 10.0m,
                BusinessTaxRate = 5.0m,
                Description = "測試用分類",
                IsActive = true
            };

            // Act
            var result = await _controller.Create(newCategory);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Edit_Get_WithValidId_ReturnsViewWithCategory()
        {
            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<TaxCategory>(viewResult.Model);
            Assert.Equal("電子產品", model.CategoryName);
        }

        [Fact]
        public async Task Edit_Get_WithInvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Edit(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CheckCategoryCode_WithUniqueCode_ReturnsTrue()
        {
            // Act
            var result = await _controller.CheckCategoryCode("UNIQUE001");

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.True((bool)jsonResult.Value);
        }

        [Fact]
        public async Task CheckCategoryCode_WithExistingCode_ReturnsFalse()
        {
            // Act
            var result = await _controller.CheckCategoryCode("ELEC001");

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.False((bool)jsonResult.Value);
        }

        [Fact]
        public async Task GetSubCategories_ReturnsJsonResult()
        {
            // 先建立一個子分類
            var parentCategory = await _context.TaxCategories.FirstAsync();
            var subCategory = new TaxCategory
            {
                CategoryName = "子分類",
                CategoryCode = "SUB001",
                TaxRate = 5.0m,
                ParentCategoryId = parentCategory.Id,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            _context.TaxCategories.Add(subCategory);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetSubCategories(parentCategory.Id);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.NotNull(jsonResult.Value);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
