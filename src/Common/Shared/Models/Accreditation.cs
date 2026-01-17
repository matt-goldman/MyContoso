namespace Shared.Models;

public class Accreditation
{
    public int AccreditationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? ExpiryDate { get; set; }
}
