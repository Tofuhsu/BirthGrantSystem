using BirthGrant.Domain.Entities;

namespace BirthGrant.Web.Models;

public class BirthGrantPrintViewModel
{
    public required BirthGrantCase Case { get; set; }

    public required string QrCodeBase64 { get; set; }

    public required string OfficeName { get; set; }

    public required string OfficeAddress { get; set; }

    public required string OfficePhone { get; set; }

    public required string QueryUrl { get; set; }

    public IReadOnlyList<OfficeContactViewModel> OfficeContacts { get; set; }
        = Array.Empty<OfficeContactViewModel>();

    public string ApplyDateRoc =>
        ToRocDate(Case.ApplyDate);

    public string BirthDateRoc =>
        ToRocDate(Case.BirthDate);

    public string ApplicantRegisterDateRoc =>
        ToRocDate(Case.ApplicantRegisterDate);

    public string SpouseRegisterDateRoc =>
        ToRocDate(Case.SpouseRegisterDate);

    private static string ToRocDate(DateTime value)
    {
        return $"{value.Year - 1911}年{value.Month}月{value.Day}日";
    }

    private static string ToRocDate(DateOnly value)
    {
        return $"{value.Year - 1911}年{value.Month}月{value.Day}日";
    }

    private static string ToRocDate(DateOnly? value)
    {
        return value.HasValue
            ? ToRocDate(value.Value)
            : string.Empty;
    }
}