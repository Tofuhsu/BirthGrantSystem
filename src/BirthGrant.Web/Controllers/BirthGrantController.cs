using BirthGrant.Domain.Entities;
using BirthGrant.Domain.Enums;
using BirthGrant.Infrastructure.Data;
using BirthGrant.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;

namespace BirthGrant.Web.Controllers;

public class BirthGrantController : Controller
{
    private const string FixedOfficeName = "新北市○○戶政事務所";
    private const string FixedOfficeAddress = "新北市○○區○○路○○號";
    private const string FixedOfficePhone = "02-29655656分機107";
    private const string FixedQrText = "https://baby.ntpc.gov.tw";

    private static readonly IReadOnlyList<OfficeContactViewModel> NewTaipeiOfficeContacts =
        new List<OfficeContactViewModel>
        {
            new("板橋戶政事務所", "（02）29655656分機107"),
            new("中和戶政事務所", "（02）29495759分機432"),
            new("新莊戶政事務所", "（02）22012128分機12"),
            new("鶯歌戶政事務所", "（02）26787784分機14"),
            new("淡水戶政事務所", "（02）26232830分機2112"),
            new("土城戶政事務所", "（02）22605640分機311"),
            new("泰山戶政事務所", "（02）29096775分機21"),
            new("新店戶政事務所", "（02）29147675分機405"),
            new("石碇區所", "（02）26631337分機105"),
            new("烏來區所", "（02）26616713"),
            new("五股戶政事務所", "（02）22916698分機27"),
            new("瑞芳戶政事務所", "（02）24972229分機51"),
            new("雙溪區所", "（02）24931105分機21"),
            new("金山戶政事務所", "（02）24982059"),
            new("三芝區所", "（02）26362889分機15"),
            new("三重戶政事務所", "（02）29812291分機205"),
            new("永和戶政事務所", "（02）29291276分機211"),
            new("樹林戶政事務所", "（02）26842131分機204"),
            new("三峽戶政事務所", "（02）26711173分機311"),
            new("汐止戶政事務所", "（02）26429866分機634"),
            new("蘆洲戶政事務所", "（02）22818536分機116"),
            new("林口戶政事務所", "（02）26011574分機30"),
            new("深坑區所", "（02）26620383分機16"),
            new("坪林區所", "（02）26656231分機12"),
            new("八里區所", "（02）26102204"),
            new("平溪區所", "（02）24951076分機14"),
            new("貢寮區所", "（02）24941034分機13"),
            new("萬里區所", "（02）24922063分機17"),
            new("石門區所", "（02）26381307分機12")
        };

    private readonly BirthGrantDbContext _context;

    public BirthGrantController(BirthGrantDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        var vm = new BirthGrantCreateViewModel
        {
            ApplyDate = DateTime.Now,
            ReceiptOfficePhone = FixedOfficePhone,
            ReceiptQrText = FixedQrText,
            PaymentMethod = "BANK",
            GrantAmount = 30000
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BirthGrantCreateViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var entity = new BirthGrantCase
        {
            Id = Guid.NewGuid(),
            CaseNo = GenerateCaseNo(),
            ApplyDate = vm.ApplyDate,
            ChildIdNo = vm.ChildIdNo,
            ChildName = vm.ChildName,
            BirthDate = DateOnly.FromDateTime(vm.BirthDate),
            ChildOrder = vm.ChildOrder,

            ApplicantIdNo = vm.ApplicantIdNo,
            ApplicantName = vm.ApplicantName,
            ApplicantRegisterDate = vm.ApplicantRegisterDate.HasValue
                ? DateOnly.FromDateTime(vm.ApplicantRegisterDate.Value)
                : null,
            ApplicantPhone = vm.ApplicantPhone,
            ApplicantAddress = vm.ApplicantAddress,

            SpouseIdNo = vm.SpouseIdNo,
            SpouseName = vm.SpouseName,
            SpouseRegisterDate = vm.SpouseRegisterDate.HasValue
                ? DateOnly.FromDateTime(vm.SpouseRegisterDate.Value)
                : null,
            SpousePhone = vm.SpousePhone,
            SpouseAddress = vm.SpouseAddress,

            AgentIdNo = vm.AgentIdNo,
            AgentName = vm.AgentName,
            AgentPhone = vm.AgentPhone,

            PayeeName = vm.PayeeName,
            PayeePhone = vm.PayeePhone,

            PaymentMethod = vm.PaymentMethod,
            BankCode = vm.BankCode,
            BankAccount = vm.BankAccount,

            GrantAmount = vm.GrantAmount,
            ReceiptOfficePhone = vm.ReceiptOfficePhone,
            ReceiptQrText = vm.ReceiptQrText,

            Status = BirthGrantCaseStatus.Submitted,
            CreatedAt = DateTime.Now
        };

        entity.Histories.Add(new BirthGrantCaseHistory
        {
            BirthGrantCaseId = entity.Id,
            ActionType = "CREATE",
            ActionDescription = "建立案件並送出",
            OperatorName = "System",
            OperatedAt = DateTime.Now
        });

        _context.BirthGrantCases.Add(entity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Success), new { id = entity.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Success(Guid id)
    {
        var entity = await _context.BirthGrantCases
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return NotFound();
        }

        return View(entity);
    }

    [HttpGet]
    public async Task<IActionResult> Print(Guid id)
    {
        var entity = await _context.BirthGrantCases
            .AsNoTracking()
            .Include(x => x.Histories)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return NotFound();
        }

        var vm = new BirthGrantPrintViewModel
        {
            Case = entity,
            QrCodeBase64 = GenerateQrCodeBase64(entity.ReceiptQrText),
            OfficeName = FixedOfficeName,
            OfficeAddress = FixedOfficeAddress,
            OfficePhone = FixedOfficePhone,
            QueryUrl = FixedQrText,
            OfficeContacts = NewTaipeiOfficeContacts
        };

        return View(vm);
    }

    private static string GenerateCaseNo()
    {
        return $"BG{DateTime.Now:yyyyMMddHHmmssfff}";
    }

    private static string GenerateQrCodeBase64(string payload)
    {
        using var generator = new QRCodeGenerator();
        using var qrData = generator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrData);
        var bytes = qrCode.GetGraphic(10);

        return Convert.ToBase64String(bytes);
    }
}