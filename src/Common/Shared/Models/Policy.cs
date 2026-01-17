namespace Shared.Models;

public class Policy
{
    public int PolicyId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; }
    public string Content { get; set; } = string.Empty;
}
