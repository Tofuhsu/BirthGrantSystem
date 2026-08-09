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
    private const string FixedOfficePhone = "02-29655656分機107";
    private const string FixedQrText = "https://baby.ntpc.gov.tw";

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
            QrCodeBase64 = GenerateQrCodeBase64(entity.ReceiptQrText)
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