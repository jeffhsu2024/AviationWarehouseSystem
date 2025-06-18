using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "分類名稱為必填")]
        [StringLength(100, ErrorMessage = "分類名稱不可超過100字元")]
        [Display(Name = "分類名稱")]
        public string CategoryName { get; set; }

        [StringLength(50, ErrorMessage = "分類代碼不可超過50字元")]
        [Display(Name = "分類代碼")]
        public string CategoryCode { get; set; }

        [StringLength(500, ErrorMessage = "分類描述不可超過500字元")]
        [Display(Name = "分類描述")]
        public string Description { get; set; }

        [Display(Name = "上層分類")]
        public int? ParentCategoryId { get; set; }

        [Display(Name = "排序順序")]
        public int SortOrder { get; set; } = 0;

        [Display(Name = "建立日期")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "是否啟用")]
        public bool IsActive { get; set; } = true;

        [StringLength(500, ErrorMessage = "備註不可超過500字元")]
        [Display(Name = "備註")]
        public string Remarks { get; set; }

        // 導航屬性
        [ValidateNever]
        public virtual ProductCategory ParentCategory { get; set; }
        [ValidateNever]
        public virtual ICollection<ProductCategory> SubCategories { get; set; } = new List<ProductCategory>();
        [ValidateNever]
        public virtual ICollection<DutyFreeProduct> Products { get; set; } = new List<DutyFreeProduct>();
    }
}
