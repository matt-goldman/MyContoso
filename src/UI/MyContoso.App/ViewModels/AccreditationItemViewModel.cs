using MyContoso.App.Models;
using Plugin.Maui.Lucide;

namespace MyContoso.App.ViewModels;

/// <summary>
/// 🚨 Anti-pattern: Item ViewModel with UI presentation properties.
/// This couples the ViewModel layer to specific UI concerns (colors, icons).
/// The correct approach is to use ValueConverters in the View layer.
/// </summary>
public class AccreditationItemViewModel(Accreditation accreditation)
{
    // The underlying model
    public Accreditation Accreditation { get; } = accreditation;

    // Pass-through properties for convenience
    public int AccreditationId => Accreditation.AccreditationId;
    public string Name => Accreditation.Name;
    public string Description => Accreditation.Description;
    public string Status => Accreditation.Status;
    public string Category => Accreditation.Category;
    public DateTime? ExpiryDate => Accreditation.ExpiryDate;

    // 🚨 UI presentation logic that should be in ValueConverters
    private static readonly Color ValidPrimary = Color.FromArgb("#7C3AED");
    private static readonly Color ValidBackground = Color.FromArgb("#EDE9FE");
    private static readonly Color ExpiredPrimary = Color.FromArgb("#EF4444");
    private static readonly Color ExpiredBackground = Color.FromArgb("#FEE2E2");
    private static readonly Color PendingPrimary = Color.FromArgb("#14B8A6");
    private static readonly Color PendingBackground = Color.FromArgb("#CCFBF1");

    public Color StatusIconColor => Status switch
    {
        "Valid" => ValidPrimary,
        "Expired" or "Overdue" => ExpiredPrimary,
        "Pending" => PendingPrimary,
        _ => Colors.Gray
    };

    public Color StatusBackgroundColor => Status switch
    {
        "Valid" => ValidBackground,
        "Expired" or "Overdue" => ExpiredBackground,
        "Pending" => PendingBackground,
        _ => Color.FromArgb("#F3F4F6")
    };

    public Color StatusBadgeColor => StatusIconColor;

    public Color StatusBadgeTextColor => Colors.White;

    public string StatusIcon => Status switch
    {
        "Valid" => Icons.CircleCheck,
        "Expired" or "Overdue" => Icons.CircleAlert,
        "Pending" => Icons.Hourglass,
        _ => Icons.CircleQuestionMark
    };
}
