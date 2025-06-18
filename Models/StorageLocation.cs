using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class StorageLocation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "儲位編號為必填")]
        [StringLength(50, ErrorMessage = "儲位編號不可超過50字元")]
        [Display(Name = "儲位編號")]
        public string LocationCode { get; set; }

        [Required(ErrorMessage = "儲位名稱為必填")]
        [StringLength(200, ErrorMessage = "儲位名稱不可超過200字元")]
        [Display(Name = "儲位名稱")]
        public string LocationName { get; set; }

        [Display(Name = "儲位類型")]
        public StorageType StorageType { get; set; } = StorageType.General;

        [Display(Name = "倉庫區域")]
        public WarehouseZone WarehouseZone { get; set; } = WarehouseZone.GeneralZone;

        [StringLength(10, ErrorMessage = "樓層不可超過10字元")]
        [Display(Name = "樓層")]
        public string Floor { get; set; }

        [StringLength(20, ErrorMessage = "貨架不可超過20字元")]
        [Display(Name = "貨架")]
        public string Rack { get; set; }

        [StringLength(20, ErrorMessage = "排不可超過20字元")]
        [Display(Name = "排")]
        public string Row { get; set; }

        [StringLength(20, ErrorMessage = "層不可超過20字元")]
        [Display(Name = "層")]
        public string Level { get; set; }

        [StringLength(20, ErrorMessage = "格不可超過20字元")]
        [Display(Name = "格")]
        public string Position { get; set; }

        [Display(Name = "啟用狀態")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "建立時間")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        [Display(Name = "建立人員")]
        public string CreatedBy { get; set; }

        [Display(Name = "修改時間")]
        public DateTime? UpdatedAt { get; set; }

        [StringLength(50)]
        [Display(Name = "修改人員")]
        public string UpdatedBy { get; set; }

        [StringLength(500, ErrorMessage = "備註不可超過500字元")]
        [Display(Name = "備註")]
        public string Remarks { get; set; }

        // 反向導航屬性（選擇性）
        // public virtual ICollection<TaxableGoods> TaxableGoods { get; set; } = new List<TaxableGoods>();
    }

    public enum StorageType
    {
        General = 0,
        Cold = 1,
        Hazardous = 2
    }

    public enum WarehouseZone
    {
        GeneralZone = 0,
        AZone = 1,
        BZone = 2
    }
}
