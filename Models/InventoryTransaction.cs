using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InventoryTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "商品為必填")]
        [Display(Name = "商品")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "異動類型為必填")]
        [Display(Name = "異動類型")]
        public TransactionType TransactionType { get; set; }

        [Required(ErrorMessage = "異動數量為必填")]
        [Display(Name = "異動數量")]
        public int Quantity { get; set; }

        [Display(Name = "異動前庫存")]
        public int BeforeQuantity { get; set; }

        [Display(Name = "異動後庫存")]
        public int AfterQuantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "單價")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "總金額")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "異動日期為必填")]
        [Display(Name = "異動日期")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [StringLength(100, ErrorMessage = "單據號碼不可超過100字元")]
        [Display(Name = "單據號碼")]
        public string DocumentNumber { get; set; }

        [StringLength(100, ErrorMessage = "供應商/客戶不可超過100字元")]
        [Display(Name = "供應商/客戶")]
        public string SupplierCustomer { get; set; }

        [StringLength(100, ErrorMessage = "經手人不可超過100字元")]
        [Display(Name = "經手人")]
        public string HandledBy { get; set; }

        [StringLength(500, ErrorMessage = "備註不可超過500字元")]
        [Display(Name = "備註")]
        public string Remarks { get; set; }

        [Display(Name = "建立日期")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(100, ErrorMessage = "建立者不可超過100字元")]
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }

        // 導航屬性
        public virtual DutyFreeProduct Product { get; set; }
    }

    public enum TransactionType
    {
        [Display(Name = "期初庫存")]
        OpeningStock = 0,
        [Display(Name = "進貨")]
        Purchase = 1,
        [Display(Name = "銷售")]
        Sale = 2,
        [Display(Name = "退貨")]
        Return = 3,
        [Display(Name = "調整")]
        Adjustment = 4,
        [Display(Name = "盤點")]
        StockTaking = 5,
        [Display(Name = "報廢")]
        Scrap = 6,
        [Display(Name = "轉倉")]
        Transfer = 7
    }
}
