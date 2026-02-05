using Plugin.Maui.Lucide;

namespace MyContoso.App.Models;

public record Accreditation(
    int AccreditationId,
    string Name,
    string Description,
    string Status,
    string Category,
    DateTime? ExpiryDate
)
{
    private static readonly Color ValidPrimary = Color.FromArgb("#7C3AED");      // Purple
    private static readonly Color ValidBackground = Color.FromArgb("#EDE9FE");   // Light purple
    private static readonly Color ExpiredPrimary = Color.FromArgb("#EF4444");    // Red
    private static readonly Color ExpiredBackground = Color.FromArgb("#FEE2E2"); // Light red
    private static readonly Color PendingPrimary = Color.FromArgb("#14B8A6");    // Teal
    private static readonly Color PendingBackground = Color.FromArgb("#CCFBF1"); // Light teal
    private static readonly Color DefaultGray = Colors.Gray;
    private static readonly Color DefaultBackground = Color.FromArgb("#F3F4F6");

    public Color StatusIconColor => Status switch
    {
        "Valid" => ValidPrimary,
        "Expired" or "Overdue" => ExpiredPrimary,
        "Pending" => PendingPrimary,
        _ => DefaultGray
    };

    public Color StatusBackgroundColor => Status switch
    {
        "Valid" => ValidBackground,
        "Expired" or "Overdue" => ExpiredBackground,
        "Pending" => PendingBackground,
        _ => DefaultBackground
    };

    public Color StatusBadgeColor => Status switch
    {
        "Valid" => ValidPrimary,
        "Expired" or "Overdue" => ExpiredPrimary,
        "Pending" => PendingPrimary,
        _ => DefaultGray
    };

    public Color StatusBadgeTextColor => Colors.White;

    public string StatusIcon => Status switch
    {
        "Valid" => Icons.CircleCheck,
        "Expired" or "Overdue" => Icons.CircleAlert,
        "Pending" => Icons.Hourglass,
        _ => Icons.CircleQuestionMark
    };
}
