using BirthGrant.Domain.Entities;

namespace BirthGrant.Web.Models;

public class BirthGrantPrintViewModel
{
    public required BirthGrantCase Case { get; set; }
    public required string QrCodeBase64 { get; set; }
}