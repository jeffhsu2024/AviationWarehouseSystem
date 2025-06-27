using Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AviationWarehouseSystem.Services
{
    public class DutyFreeProductReportService
    {
        public byte[] GenerateDutyFreeProductListReport(IEnumerable<DutyFreeProduct> products, string searchFilter = "")
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Microsoft JhengHei"));

                    page.Header()
                        .Text("免稅商品清單報表")
                        .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            // 報表資訊
                            x.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text($"報表產生時間：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                                    if (!string.IsNullOrEmpty(searchFilter))
                                    {
                                        col.Item().Text($"搜尋條件：{searchFilter}");
                                    }
                                    col.Item().Text($"商品總數：{products.Count()} 項");
                                });
                            });

                            x.Item().PaddingTop(20).Element(ComposeTable);
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("第 ");
                            x.CurrentPageNumber();
                            x.Span(" 頁，共 ");
                            x.TotalPages();
                            x.Span(" 頁");
                        });
                });
            });

            return document.GeneratePdf();

            void ComposeTable(IContainer container)
            {
                container.Table(table =>
                {
                    // 定義欄位
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(80);  // 商品編號
                        columns.RelativeColumn(3);   // 商品名稱
                        columns.RelativeColumn(2);   // 品牌
                        columns.ConstantColumn(60);  // 單價
                        columns.ConstantColumn(50);  // 庫存
                        columns.ConstantColumn(40);  // 單位
                        columns.RelativeColumn(1);   // 分類
                        columns.RelativeColumn(2);   // 供應商
                        columns.ConstantColumn(50);  // 狀態
                    });

                    // 表頭
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("商品編號").SemiBold();
                        header.Cell().Element(CellStyle).Text("商品名稱").SemiBold();
                        header.Cell().Element(CellStyle).Text("品牌").SemiBold();
                        header.Cell().Element(CellStyle).Text("單價").SemiBold();
                        header.Cell().Element(CellStyle).Text("庫存").SemiBold();
                        header.Cell().Element(CellStyle).Text("單位").SemiBold();
                        header.Cell().Element(CellStyle).Text("分類").SemiBold();
                        header.Cell().Element(CellStyle).Text("供應商").SemiBold();
                        header.Cell().Element(CellStyle).Text("狀態").SemiBold();

                        static IContainer CellStyle(IContainer container)
                        {
                            return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                        }
                    });

                    // 資料行
                    foreach (var product in products)
                    {
                        table.Cell().Element(CellStyle).Text(product.ProductCode ?? "");
                        table.Cell().Element(CellStyle).Text(product.ProductName ?? "");
                        table.Cell().Element(CellStyle).Text(product.Brand ?? "");
                        table.Cell().Element(CellStyle).Text($"${product.UnitPrice:N2}");
                        table.Cell().Element(CellStyle).Text(product.StockQuantity.ToString());
                        table.Cell().Element(CellStyle).Text(product.Unit ?? "");
                        table.Cell().Element(CellStyle).Text(product.Category?.CategoryName ?? "");
                        table.Cell().Element(CellStyle).Text(product.Supplier?.SupplierName ?? "");
                        table.Cell().Element(CellStyle).Text(product.IsActive ? "啟用" : "停用");

                        static IContainer CellStyle(IContainer container)
                        {
                            return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                        }
                    }
                });
            }
        }

        public byte[] GenerateDutyFreeProductDetailReport(DutyFreeProduct product)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Microsoft JhengHei"));

                    page.Header()
                        .Text("免稅商品詳細資料")
                        .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            // 基本資訊
                            x.Item().Text("基本資訊").SemiBold().FontSize(14).FontColor(Colors.Blue.Medium);
                            x.Item().PaddingTop(10).Element(ComposeBasicInfo);

                            // 價格與庫存資訊
                            x.Item().PaddingTop(20).Text("價格與庫存資訊").SemiBold().FontSize(14).FontColor(Colors.Blue.Medium);
                            x.Item().PaddingTop(10).Element(ComposePriceStockInfo);

                            // 免稅資訊
                            x.Item().PaddingTop(20).Text("免稅資訊").SemiBold().FontSize(14).FontColor(Colors.Blue.Medium);
                            x.Item().PaddingTop(10).Element(ComposeDutyFreeInfo);

                            // 其他資訊
                            x.Item().PaddingTop(20).Text("其他資訊").SemiBold().FontSize(14).FontColor(Colors.Blue.Medium);
                            x.Item().PaddingTop(10).Element(ComposeOtherInfo);
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text($"報表產生時間：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                });
            });

            return document.GeneratePdf();

            void ComposeBasicInfo(IContainer container)
            {
                container.Column(column =>
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"商品編號：{product.ProductCode}");
                        row.RelativeItem().Text($"商品名稱：{product.ProductName}");
                    });
                    column.Item().PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem().Text($"品牌：{product.Brand ?? "無"}");
                        row.RelativeItem().Text($"條碼：{product.Barcode ?? "無"}");
                    });
                    column.Item().PaddingTop(5).Text($"商品描述：{product.Description ?? "無"}");
                });
            }

            void ComposePriceStockInfo(IContainer container)
            {
                container.Column(column =>
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"單價：${product.UnitPrice:N2}");
                        row.RelativeItem().Text($"成本：${product.CostPrice:N2}");
                    });
                    column.Item().PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem().Text($"庫存數量：{product.StockQuantity} {product.Unit}");
                        row.RelativeItem().Text($"安全庫存：{product.SafetyStock} {product.Unit}");
                    });
                    column.Item().PaddingTop(5).Text($"最大庫存：{product.MaxStock} {product.Unit}");
                });
            }

            void ComposeDutyFreeInfo(IContainer container)
            {
                container.Column(column =>
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"免稅類型：{product.DutyFreeType}");
                        row.RelativeItem().Text($"免稅折扣率：{product.DutyFreeDiscountRate}%");
                    });
                });
            }

            void ComposeOtherInfo(IContainer container)
            {
                container.Column(column =>
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"商品分類：{product.Category?.CategoryName ?? "無"}");
                        row.RelativeItem().Text($"供應商：{product.Supplier?.SupplierName ?? "無"}");
                    });
                    column.Item().PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem().Text($"建立日期：{product.CreatedDate:yyyy-MM-dd}");
                        row.RelativeItem().Text($"建立者：{product.CreatedBy ?? "系統"}");
                    });
                    column.Item().PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem().Text($"更新日期：{product.UpdatedDate?.ToString("yyyy-MM-dd") ?? "無"}");
                        row.RelativeItem().Text($"更新者：{product.UpdatedBy ?? "無"}");
                    });
                    column.Item().PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem().Text($"狀態：{(product.IsActive ? "啟用" : "停用")}");
                        row.RelativeItem().Text($"儲位：{product.StorageLocation?.LocationName ?? "無"}");
                    });
                    if (!string.IsNullOrEmpty(product.Remarks))
                    {
                        column.Item().PaddingTop(5).Text($"備註：{product.Remarks}");
                    }
                });
            }
        }
    }
}
