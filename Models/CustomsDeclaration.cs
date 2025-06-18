using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CustomsDeclaration
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "報關單號為必填")]
        [StringLength(100, ErrorMessage = "報關單號不可超過100字元")]
        [Display(Name = "報關單號")]
        public string DeclarationNumber { get; set; }

        [Display(Name = "報關類型")]
        public DeclarationType DeclarationType { get; set; } = DeclarationType.Import;

        [Required(ErrorMessage = "報關日期為必填")]
        [Display(Name = "報關日期")]
        public DateTime DeclarationDate { get; set; }

        [Display(Name = "通關日期")]
        public DateTime? ClearanceDate { get; set; }

        [Required(ErrorMessage = "貨物為必填")]
        [Display(Name = "貨物")]
        public int TaxableGoodsId { get; set; }

        [Required(ErrorMessage = "數量為必填")]
        [Range(1, int.MaxValue, ErrorMessage = "數量必須大於0")]
        [Display(Name = "數量")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "完稅價格為必填")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "完稅價格必須大於0")]
        [Display(Name = "完稅價格")]
        public decimal DutiableValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "應納稅額")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "已繳稅額")]
        public decimal PaidTaxAmount { get; set; }

        [Display(Name = "報關狀態")]
        public CustomsDeclarationStatus Status { get; set; } = CustomsDeclarationStatus.Draft;

        [StringLength(200, ErrorMessage = "報關行不可超過200字元")]
        [Display(Name = "報關行")]
        public string CustomsBroker { get; set; }

        [StringLength(100, ErrorMessage = "報關人員不可超過100字元")]
        [Display(Name = "報關人員")]
        public string CustomsOfficer { get; set; }

        [StringLength(200, ErrorMessage = "進/出口商不可超過200字元")]
        [Display(Name = "進/出口商")]
        public string ImporterExporter { get; set; }

        [StringLength(200, ErrorMessage = "運輸工具不可超過200字元")]
        [Display(Name = "運輸工具")]
        public string TransportVehicle { get; set; }

        [StringLength(100, ErrorMessage = "航班/船次不可超過100字元")]
        [Display(Name = "航班/船次")]
        public string FlightShipNumber { get; set; }

        [Display(Name = "到達日期")]
        public DateTime? ArrivalDate { get; set; }

        [StringLength(100, ErrorMessage = "艙單號不可超過100字元")]
        [Display(Name = "艙單號")]
        public string ManifestNumber { get; set; }

        [StringLength(1000, ErrorMessage = "拒絕原因不可超過1000字元")]
        [Display(Name = "拒絕原因")]
        public string RejectionReason { get; set; }

        [Display(Name = "建立日期")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "更新日期")]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(100, ErrorMessage = "建立者不可超過100字元")]
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }

        [StringLength(100, ErrorMessage = "更新者不可超過100字元")]
        [Display(Name = "更新者")]
        public string UpdatedBy { get; set; }

        [StringLength(500, ErrorMessage = "備註不可超過500字元")]
        [Display(Name = "備註")]
        public string Remarks { get; set; }

        // 導航屬性
        public virtual TaxableGoods TaxableGoods { get; set; }
    }

    public enum DeclarationType
    {
        [Display(Name = "進口")]
        Import = 0,
        [Display(Name = "出口")]
        Export = 1,
        [Display(Name = "轉運")]
        Transit = 2,
        [Display(Name = "退運")]
        Return = 3
    }

    public enum CustomsDeclarationStatus
    {
        [Display(Name = "草稿")]
        Draft = 0,
        [Display(Name = "已提交")]
        Submitted = 1,
        [Display(Name = "審核中")]
        UnderReview = 2,
        [Display(Name = "已核准")]
        Approved = 3,
        [Display(Name = "已放行")]
        Released = 4,
        [Display(Name = "被拒絕")]
        Rejected = 5,
        [Display(Name = "暫扣")]
        Detained = 6
    }
}
