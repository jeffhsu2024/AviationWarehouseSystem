using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "供應商名稱為必填")]
        [StringLength(200, ErrorMessage = "供應商名稱不可超過200字元")]
        [Display(Name = "供應商名稱")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "供應商代碼為必填")]
        [StringLength(50, ErrorMessage = "供應商代碼不可超過50字元")]
        [Display(Name = "供應商代碼")]
        public string SupplierCode { get; set; }

        [StringLength(200, ErrorMessage = "英文名稱不可超過200字元")]
        [Display(Name = "英文名稱")]
        public string EnglishName { get; set; }

        [StringLength(20, ErrorMessage = "統一編號不可超過20字元")]
        [Display(Name = "統一編號")]
        public string TaxNumber { get; set; }

        [StringLength(500, ErrorMessage = "地址不可超過500字元")]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "電話不可超過20字元")]
        [Display(Name = "電話")]
        public string Phone { get; set; }

        [StringLength(20, ErrorMessage = "傳真不可超過20字元")]
        [Display(Name = "傳真")]
        public string Fax { get; set; }

        [StringLength(100, ErrorMessage = "Email不可超過100字元")]
        [EmailAddress(ErrorMessage = "請輸入有效的Email格式")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "網站不可超過100字元")]
        [Display(Name = "網站")]
        public string Website { get; set; }

        [StringLength(100, ErrorMessage = "聯絡人不可超過100字元")]
        [Display(Name = "聯絡人")]
        public string ContactPerson { get; set; }

        [StringLength(20, ErrorMessage = "聯絡人電話不可超過20字元")]
        [Display(Name = "聯絡人電話")]
        public string ContactPhone { get; set; }

        [StringLength(100, ErrorMessage = "聯絡人Email不可超過100字元")]
        [EmailAddress(ErrorMessage = "請輸入有效的Email格式")]
        [Display(Name = "聯絡人Email")]
        public string ContactEmail { get; set; }

        [Display(Name = "供應商類型")]
        public SupplierType SupplierType { get; set; } = SupplierType.General;

        [Display(Name = "付款條件(天)")]
        public int PaymentTerms { get; set; } = 30;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "信用額度")]
        public decimal CreditLimit { get; set; } = 0;

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
        public virtual ICollection<DutyFreeProduct> Products { get; set; } = new List<DutyFreeProduct>();
        public virtual ICollection<TaxableGoods> TaxableGoods { get; set; } = new List<TaxableGoods>();
    }
    public enum SupplierType
    {
        [Display(Name = "一般供應商")]
        General = 0,
        [Display(Name = "免稅品供應商")]
        DutyFree = 1,
        [Display(Name = "進口商")]
        Importer = 2,
        [Display(Name = "代理商")]
        Agent = 3
    }
}
