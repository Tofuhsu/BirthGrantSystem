using BirthGrant.Domain.Enums;

namespace BirthGrant.Domain.Entities;

public class BirthGrantCase
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string CaseNo { get; set; } = string.Empty;
    public DateTime ApplyDate { get; set; } = DateTime.Now;

    public string ChildIdNo { get; set; } = string.Empty;
    public string ChildName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public int ChildOrder { get; set; }

    public string ApplicantIdNo { get; set; } = string.Empty;
    public string ApplicantName { get; set; } = string.Empty;
    public DateOnly? ApplicantRegisterDate { get; set; }
    public string ApplicantPhone { get; set; } = string.Empty;
    public string ApplicantAddress { get; set; } = string.Empty;

    public string? SpouseIdNo { get; set; }
    public string? SpouseName { get; set; }
    public DateOnly? SpouseRegisterDate { get; set; }
    public string? SpousePhone { get; set; }
    public string? SpouseAddress { get; set; }

    public string? AgentIdNo { get; set; }
    public string? AgentName { get; set; }
    public string? AgentPhone { get; set; }

    public string PayeeName { get; set; } = string.Empty;
    public string? PayeePhone { get; set; }

    public string PaymentMethod { get; set; } = "BANK";
    public string? BankCode { get; set; }
    public string? BankAccount { get; set; }

    public decimal GrantAmount { get; set; }

    public string ReceiptOfficePhone { get; set; } = string.Empty;
    public string ReceiptQrText { get; set; } = "https://baby.ntpc.gov.tw";

    public BirthGrantCaseStatus Status { get; set; } = BirthGrantCaseStatus.Draft;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public List<BirthGrantCaseHistory> Histories { get; set; } = new();
}