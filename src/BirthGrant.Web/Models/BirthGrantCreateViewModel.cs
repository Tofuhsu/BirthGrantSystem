using System.ComponentModel.DataAnnotations;

namespace BirthGrant.Web.Models;

public class BirthGrantCreateViewModel
{
    [Required]
    [Display(Name = "申請日期")]
    [DataType(DataType.Date)]
    public DateTime ApplyDate { get; set; } = DateTime.Now;

    [Required]
    [StringLength(20)]
    [Display(Name = "新生兒身分證字號")]
    public string ChildIdNo { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    [Display(Name = "新生兒姓名")]
    public string ChildName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "新生兒出生日期")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; } = DateTime.Now;

    [Required]
    [Range(1, 10)]
    [Display(Name = "子女數")]
    public int ChildOrder { get; set; } = 1;

    [Required]
    [StringLength(20)]
    [Display(Name = "設籍人身分證字號")]
    public string ApplicantIdNo { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    [Display(Name = "設籍人姓名")]
    public string ApplicantName { get; set; } = string.Empty;

    [Display(Name = "設籍日期")]
    [DataType(DataType.Date)]
    public DateTime? ApplicantRegisterDate { get; set; }

    [Required]
    [StringLength(20)]
    [Display(Name = "設籍人電話")]
    public string ApplicantPhone { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [Display(Name = "設籍人戶籍地址")]
    public string ApplicantAddress { get; set; } = string.Empty;

    [StringLength(20)]
    [Display(Name = "配偶身分證字號")]
    public string? SpouseIdNo { get; set; }

    [StringLength(50)]
    [Display(Name = "配偶姓名")]
    public string? SpouseName { get; set; }

    [Display(Name = "配偶設籍日期")]
    [DataType(DataType.Date)]
    public DateTime? SpouseRegisterDate { get; set; }

    [StringLength(20)]
    [Display(Name = "配偶電話")]
    public string? SpousePhone { get; set; }

    [StringLength(200)]
    [Display(Name = "配偶戶籍地址")]
    public string? SpouseAddress { get; set; }

    [StringLength(20)]
    [Display(Name = "受託人身分證字號")]
    public string? AgentIdNo { get; set; }

    [StringLength(50)]
    [Display(Name = "受託人姓名")]
    public string? AgentName { get; set; }

    [StringLength(20)]
    [Display(Name = "受託人電話")]
    public string? AgentPhone { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "受款人姓名")]
    public string PayeeName { get; set; } = string.Empty;

    [StringLength(20)]
    [Display(Name = "受款人電話")]
    public string? PayeePhone { get; set; }

    [Required]
    [StringLength(20)]
    [Display(Name = "領取方式")]
    public string PaymentMethod { get; set; } = "BANK";

    [StringLength(10)]
    [Display(Name = "金融行庫代碼")]
    public string? BankCode { get; set; }

    [StringLength(30)]
    [Display(Name = "金融帳號")]
    public string? BankAccount { get; set; }

    [Required]
    [Range(0, 999999)]
    [Display(Name = "核發金額")]
    public decimal GrantAmount { get; set; } = 30000;

    [Required]
    [StringLength(30)]
    [Display(Name = "收執聯電話")]
    public string ReceiptOfficePhone { get; set; } = "02-29655656分機107";

    [Required]
    [StringLength(200)]
    [Display(Name = "QR Code 內容")]
    public string ReceiptQrText { get; set; } = "https://baby.ntpc.gov.tw";
}