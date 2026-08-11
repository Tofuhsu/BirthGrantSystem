using System.ComponentModel.DataAnnotations;
using BirthGrant.Shared.Validation;

namespace BirthGrant.Web.Models;

public class BirthGrantCreateViewModel : IValidatableObject
{
    [Required(ErrorMessage = "請選擇申請日期。")]
    [Display(Name = "申請日期")]
    [DataType(DataType.Date)]
    public DateTime ApplyDate { get; set; } = DateTime.Now;


    [Required(ErrorMessage = "請填寫新生兒身分證字號。")]
    [TaiwanNationalId(
        ErrorMessage = "新生兒身分證字號格式錯誤，請輸入 1 個大寫英文字母加 9 位數字。")]
    [StringLength(
        10,
        MinimumLength = 10,
        ErrorMessage = "新生兒身分證字號必須為 10 碼。")]
    [Display(Name = "新生兒身分證字號")]
    public string ChildIdNo { get; set; } = string.Empty;


    [Required(ErrorMessage = "請填寫新生兒姓名。")]
    [StringLength(
        50,
        ErrorMessage = "新生兒姓名不得超過 50 個字元。")]
    [Display(Name = "新生兒姓名")]
    public string ChildName { get; set; } = string.Empty;


    [Required(ErrorMessage = "請選擇新生兒出生日期。")]
    [Display(Name = "新生兒出生日期")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; } = DateTime.Now;


    [Required(ErrorMessage = "請填寫子女數。")]
    [Range(
        1,
        10,
        ErrorMessage = "子女數必須介於 1 至 10。")]
    [Display(Name = "第幾名子女")]
    public int ChildOrder { get; set; } = 1;

    [Required(ErrorMessage = "請填寫設籍人身分證字號。")]
    [TaiwanNationalId(
        ErrorMessage = "設籍人身分證字號格式錯誤，請輸入 1 個大寫英文字母加 9 位數字。")]
    [StringLength(
        10,
        MinimumLength = 10,
        ErrorMessage = "設籍人身分證字號必須為 10 碼。")]
    [Display(Name = "設籍人身分證字號")]
    public string ApplicantIdNo { get; set; } = string.Empty;


    [Required(ErrorMessage = "請填寫設籍人姓名。")]
    [StringLength(
        50,
        ErrorMessage = "設籍人姓名不得超過 50 個字元。")]
    [Display(Name = "設籍人姓名")]
    public string ApplicantName { get; set; } = string.Empty;


    [Required(ErrorMessage = "請選擇設籍日期。")]
    [Display(Name = "設籍日期")]
    [DataType(DataType.Date)]
    public DateTime? ApplicantRegisterDate { get; set; }


    [Required(ErrorMessage = "請填寫設籍人連絡電話。")]
    [RegularExpression(
        @"^09\d{8}$",
        ErrorMessage = "設籍人連絡電話格式錯誤，請輸入 10 位數字，例如 0912345678。")]
    [Display(Name = "設籍人連絡電話")]
    public string ApplicantPhone { get; set; } = string.Empty;


    [Required(ErrorMessage = "請填寫設籍人戶籍地址。")]
    [StringLength(
        200,
        ErrorMessage = "設籍人戶籍地址不得超過 200 個字元。")]
    [Display(Name = "設籍人戶籍地址")]
    public string ApplicantAddress { get; set; } = string.Empty;


    [TaiwanNationalId(
        ErrorMessage = "配偶身分證字號格式錯誤，請輸入 1 個大寫英文字母加 9 位數字。")]
    [StringLength(
        10,
        MinimumLength = 10,
        ErrorMessage = "配偶身分證字號必須為 10 碼。")]
    [Display(Name = "配偶身分證字號")]
    public string? SpouseIdNo { get; set; }


    [StringLength(
        50,
        ErrorMessage = "配偶姓名不得超過 50 個字元。")]
    [Display(Name = "配偶姓名")]
    public string? SpouseName { get; set; }


    [Display(Name = "配偶設籍日期")]
    [DataType(DataType.Date)]
    public DateTime? SpouseRegisterDate { get; set; }


    [RegularExpression(
        @"^09\d{8}$",
        ErrorMessage = "配偶連絡電話格式錯誤，請輸入 10 位數字，例如 0912345678。")]
    [Display(Name = "配偶連絡電話")]
    public string? SpousePhone { get; set; }


    [StringLength(
        200,
        ErrorMessage = "配偶戶籍地址不得超過 200 個字元。")]
    [Display(Name = "配偶戶籍地址")]
    public string? SpouseAddress { get; set; }


    [TaiwanNationalId(
        ErrorMessage = "受託人身分證字號格式錯誤，請輸入 1 個大寫英文字母加 9 位數字。")]
    [StringLength(
        10,
        MinimumLength = 10,
        ErrorMessage = "受託人身分證字號必須為 10 碼。")]
    [Display(Name = "受託人身分證字號")]
    public string? AgentIdNo { get; set; }


    [StringLength(
        50,
        ErrorMessage = "受託人姓名不得超過 50 個字元。")]
    [Display(Name = "受託人姓名")]
    public string? AgentName { get; set; }


    [RegularExpression(
        @"^09\d{8}$",
        ErrorMessage = "受託人連絡電話格式錯誤，請輸入 10 位數字，例如 0912345678。")]
    [Display(Name = "受託人連絡電話")]
    public string? AgentPhone { get; set; }


    [Required(ErrorMessage = "請填寫受款人姓名。")]
    [StringLength(
        50,
        ErrorMessage = "受款人姓名不得超過 50 個字元。")]
    [Display(Name = "受款人姓名")]
    public string PayeeName { get; set; } = string.Empty;


    [RegularExpression(
        @"^09\d{8}$",
        ErrorMessage = "受款人連絡電話格式錯誤，請輸入 10 位數字，例如 0912345678。")]
    [Display(Name = "受款人連絡電話")]
    public string? PayeePhone { get; set; }

    [Required(ErrorMessage = "請選擇領取方式。")]
    [Display(Name = "領取方式")]
    public string PaymentMethod { get; set; } = "BANK";


    [RegularExpression(
        @"^\d{3}$",
        ErrorMessage = "金融行庫代碼格式錯誤，請輸入 3 位數字，例如 700。")]
    [Display(Name = "金融行庫代碼")]
    public string? BankCode { get; set; }


    [RegularExpression(
        @"^\d{6,30}$",
        ErrorMessage = "金融帳號格式錯誤，請輸入 6 至 30 位數字。")]
    [Display(Name = "金融帳號")]
    public string? BankAccount { get; set; }


    [Required(ErrorMessage = "請填寫核發金額。")]
    [Range(
        0,
        999999,
        ErrorMessage = "核發金額必須介於 0 至 999,999 元。")]
    [Display(Name = "核發金額")]
    public decimal GrantAmount { get; set; } = 30000;



    [Required]
    [StringLength(30)]
    [Display(Name = "收執聯電話")]
    public string ReceiptOfficePhone { get; set; }
        = "02-29655656分機107";


    [Required]
    [StringLength(200)]
    [Display(Name = "QR Code 內容")]
    public string ReceiptQrText { get; set; }
        = "https://baby.ntpc.gov.tw";


    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        var applyDate = ApplyDate.Date;
        var birthDate = BirthDate.Date;

        if (applyDate < birthDate.AddDays(1))
        {
            results.Add(
                new ValidationResult(
                    "申請日期必須為新生兒出生次日或之後。",
                    new[]
                    {
                        nameof(ApplyDate),
                        nameof(BirthDate)
                    }));
        }


        if (applyDate > birthDate.AddYears(1))
        {
            results.Add(
                new ValidationResult(
                    "申請日期不得超過新生兒出生次日起一年。",
                    new[]
                    {
                        nameof(ApplyDate),
                        nameof(BirthDate)
                    }));
        }


        if (!ApplicantRegisterDate.HasValue)
        {
            results.Add(
                new ValidationResult(
                    "請選擇設籍日期。",
                    new[]
                    {
                        nameof(ApplicantRegisterDate)
                    }));
        }
        else
        {
            var registerDate =
                ApplicantRegisterDate.Value.Date;

            if (registerDate > birthDate.AddMonths(-10))
            {
                results.Add(
                    new ValidationResult(
                        "設籍日期不符合目前設定的十個月資格條件。",
                        new[]
                        {
                            nameof(ApplicantRegisterDate),
                            nameof(BirthDate)
                        }));
            }
        }

        return results;
    }
}