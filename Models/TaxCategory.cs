using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TaxCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "分類名稱為必填")]
        [StringLength(200, ErrorMessage = "分類名稱不可超過200字元")]
        [Display(Name = "分類名稱")]
        public string CategoryName { get; set; }

        [StringLength(200, ErrorMessage = "英文名稱不可超過200字元")]
        [Display(Name = "英文名稱")]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "分類代碼為必填")]
        [StringLength(50, ErrorMessage = "分類代碼不可超過50字元")]
        [Display(Name = "分類代碼")]
        public string CategoryCode { get; set; }

        [StringLength(20, ErrorMessage = "稅則號列不可超過20字元")]
        [Display(Name = "稅則號列")]
        public string TariffCode { get; set; }

        [Required(ErrorMessage = "稅率為必填")]
        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100, ErrorMessage = "稅率必須在0-100之間")]
        [Display(Name = "稅率(%)")]
        public decimal TaxRate { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100, ErrorMessage = "營業稅率必須在0-100之間")]
        [Display(Name = "營業稅率(%)")]
        public decimal BusinessTaxRate { get; set; } = 5.0m;

        [StringLength(500, ErrorMessage = "分類描述不可超過500字元")]
        [Display(Name = "分類描述")]
        public string Description { get; set; }

        [Display(Name = "上層分類")]
        public int? ParentCategoryId { get; set; }

        [Display(Name = "是否管制品")]
        public bool IsControlledItem { get; set; } = false;

        [Display(Name = "是否需要許可證")]
        public bool RequiresPermit { get; set; } = false;

        [StringLength(500, ErrorMessage = "管制說明不可超過500字元")]
        [Display(Name = "管制說明")]
        public string ControlDescription { get; set; }

        [Display(Name = "排序順序")]
        public int SortOrder { get; set; } = 0;

        [Display(Name = "建立日期")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "更新日期")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "是否啟用")]
        public bool IsActive { get; set; } = true;

        [StringLength(500, ErrorMessage = "備註不可超過500字元")]
        [Display(Name = "備註")]
        public string Remarks { get; set; }

        // 導航屬性
        [ValidateNever]
        public virtual TaxCategory ParentCategory { get; set; }
        [ValidateNever]
        public virtual ICollection<TaxCategory> SubCategories { get; set; } = new List<TaxCategory>();
        [ValidateNever]
        public virtual ICollection<TaxableGoods> TaxableGoods { get; set; } = new List<TaxableGoods>();
    }

}
