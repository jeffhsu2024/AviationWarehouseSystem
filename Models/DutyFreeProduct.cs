using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class DutyFreeProduct
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "商品名稱為必填")]
        [StringLength(100, ErrorMessage = "商品名稱不可超過100字元")]
        [Display(Name = "商品名稱")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "商品編號為必填")]
        [StringLength(50, ErrorMessage = "商品編號不可超過50字元")]
        [Display(Name = "商品編號")]
        public string ProductCode { get; set; }

        [StringLength(100, ErrorMessage = "品牌名稱不可超過100字元")]
        [Display(Name = "品牌")]
        public string Brand { get; set; }

        [StringLength(500, ErrorMessage = "商品描述不可超過500字元")]
        [Display(Name = "商品描述")]
        public string Description { get; set; }

        [Required(ErrorMessage = "單價為必填")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "單價必須大於0")]
        [Display(Name = "單價")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "成本為必填")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "成本必須大於0")]
        [Display(Name = "成本")]
        public decimal CostPrice { get; set; }

        [Display(Name = "庫存數量")]
        public int StockQuantity { get; set; }

        [Display(Name = "安全庫存")]
        public int SafetyStock { get; set; }

        [Display(Name = "最大庫存")]
        public int MaxStock { get; set; }

        [StringLength(20, ErrorMessage = "單位不可超過20字元")]
        [Display(Name = "單位")]
        public string Unit { get; set; }

        [StringLength(50, ErrorMessage = "條碼不可超過50字元")]
        [Display(Name = "條碼")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "商品分類為必填")]
        [Display(Name = "商品分類")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "供應商為必填")]
        [Display(Name = "供應商")]
        public int SupplierId { get; set; }

        [Display(Name = "免稅類型")]
        public DutyFreeType DutyFreeType { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Display(Name = "免稅折扣率(%)")]
        public decimal DutyFreeDiscountRate { get; set; }

        [Display(Name = "建立日期")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "更新日期")]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(100, ErrorMessage = "建立者名稱不可超過100字元")]
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }

        [StringLength(100, ErrorMessage = "更新者名稱不可超過100字元")]
        [Display(Name = "更新者")]
        public string UpdatedBy { get; set; }

        [Display(Name = "是否啟用")]
        public bool IsActive { get; set; }

        [StringLength(500, ErrorMessage = "備註不可超過500字元")]
        [Display(Name = "備註")]
        public string Remarks { get; set; }

        // 導航屬性
        [ValidateNever]
        public virtual ProductCategory Category { get; set; }

        [ValidateNever]
        public virtual Supplier Supplier { get; set; }

        [ValidateNever]
        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; }


    }

    public enum DutyFreeType
    {
        [Display(Name = "一般免稅品")]
        General = 0,
        [Display(Name = "煙酒類")]
        TobaccoAlcohol = 1,
        [Display(Name = "化妝品")]
        Cosmetics = 2,
        [Display(Name = "精品")]
        Luxury = 3,
        [Display(Name = "食品")]
        Food = 4
    }

}
