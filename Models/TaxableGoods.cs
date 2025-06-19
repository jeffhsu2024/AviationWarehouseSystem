using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TaxableGoods
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "貨物名稱為必填")]
        [StringLength(200, ErrorMessage = "貨物名稱不可超過200字元")]
        [Display(Name = "貨物名稱")]
        public string GoodsName { get; set; }

        [StringLength(200, ErrorMessage = "英文名稱不可超過200字元")]
        [Display(Name = "英文名稱")]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "貨物編號為必填")]
        [StringLength(50, ErrorMessage = "貨物編號不可超過50字元")]
        [Display(Name = "貨物編號")]
        public string GoodsCode { get; set; }

        [StringLength(20, ErrorMessage = "稅則號列不可超過20字元")]
        [Display(Name = "稅則號列")]
        public string TariffCode { get; set; }

        [StringLength(500, ErrorMessage = "貨物描述不可超過500字元")]
        [Display(Name = "貨物描述")]
        public string Description { get; set; }

        [Required(ErrorMessage = "完稅價格為必填")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "完稅價格必須大於0")]
        [Display(Name = "完稅價格")]
        public decimal DutiableValue { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100, ErrorMessage = "稅率必須在0-100之間")]
        [Display(Name = "稅率(%)")]
        public decimal TaxRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "應納稅額")]
        public decimal TaxAmount { get; set; }

        [Required(ErrorMessage = "數量為必填")]
        [Range(1, int.MaxValue, ErrorMessage = "數量必須大於0")]
        [Display(Name = "數量")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "單位為必填")]
        [StringLength(20, ErrorMessage = "單位不可超過20字元")]
        [Display(Name = "單位")]
        public string Unit { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Display(Name = "重量(KG)")]
        public decimal Weight { get; set; }

        [StringLength(50, ErrorMessage = "原產地不可超過50字元")]
        [Display(Name = "原產地")]
        public string OriginCountry { get; set; }

        [Required(ErrorMessage = "稅務分類為必填")]
        public int? TaxCategoryId { get; set; }

        [Display(Name = "供應商")]
        public int? SupplierId { get; set; }

        [Required(ErrorMessage = "儲位為必填")]
        public int? StorageLocationId { get; set; }

        [Display(Name = "報關狀態")]
        public CustomsStatus CustomsStatus { get; set; } = CustomsStatus.Pending;

        [Display(Name = "進口日期")]
        public DateTime? ImportDate { get; set; }

        [Display(Name = "報關日期")]
        public DateTime? CustomsDeclarationDate { get; set; }

        [Display(Name = "通關日期")]
        public DateTime? ClearanceDate { get; set; }

        [StringLength(100, ErrorMessage = "報關單號不可超過100字元")]
        [Display(Name = "報關單號")]
        public string CustomsDeclarationNumber { get; set; }

        [Display(Name = "建立日期")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "更新日期")]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(100, ErrorMessage = "建立者不可超過100字元")]
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }

        [Display(Name = "是否啟用")]
        public bool IsActive { get; set; } = true;

        [StringLength(500, ErrorMessage = "備註不可超過500字元")]
        [Display(Name = "備註")]
        public string Remarks { get; set; }

        // 導航屬性
        [ValidateNever]
        public virtual TaxCategory TaxCategory { get; set; }
        [ValidateNever]
        public virtual Supplier Supplier { get; set; }
        [ValidateNever]
        public virtual StorageLocation StorageLocation { get; set; }
        [ValidateNever]
        public virtual ICollection<CustomsDeclaration> CustomsDeclarations { get; set; } = new List<CustomsDeclaration>();
    }
    public enum CustomsStatus
    {
        [Display(Name = "待報關")]
        Pending = 0,
        [Display(Name = "已報關")]
        Declared = 1,
        [Display(Name = "通關中")]
        InProcess = 2,
        [Display(Name = "已通關")]
        Cleared = 3,
        [Display(Name = "被拒絕")]
        Rejected = 4,
        [Display(Name = "暫扣")]
        Detained = 5
    }
}
