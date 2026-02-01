using System.Globalization;

namespace MyContoso.App.Converters;

/// <summary>
/// Converts accreditation status to appropriate colors.
/// Parameter options: null/empty (icon color), "Background", "Badge", "BadgeText"
/// </summary>
public class AccreditationStatusColorConverter : IValueConverter
{
    // Color definitions matching the prototype design
    private static readonly Color ValidPrimary = Color.FromArgb("#7C3AED");      // Purple
    private static readonly Color ValidBackground = Color.FromArgb("#EDE9FE");   // Light purple
    private static readonly Color ExpiredPrimary = Color.FromArgb("#EF4444");    // Red
    private static readonly Color ExpiredBackground = Color.FromArgb("#FEE2E2"); // Light red
    private static readonly Color PendingPrimary = Color.FromArgb("#14B8A6");    // Teal
    private static readonly Color PendingBackground = Color.FromArgb("#CCFBF1"); // Light teal
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var status = value as string ?? "Unknown";
        var paramStr = parameter as string ?? "";
        
        return (status, paramStr) switch
        {
            // Icon foreground colors
            ("Valid", "" or null) => ValidPrimary,
            ("Expired", "" or null) => ExpiredPrimary,
            ("Pending", "" or null) => PendingPrimary,
            (_, "" or null) => Colors.Gray,
            
            // Background colors (for icon container)
            ("Valid", "Background") => ValidBackground,
            ("Expired", "Background") => ExpiredBackground,
            ("Pending", "Background") => PendingBackground,
            (_, "Background") => Color.FromArgb("#F3F4F6"),
            
            // Badge background colors
            ("Valid", "Badge") => ValidPrimary,
            ("Expired", "Badge") => ExpiredPrimary,
            ("Pending", "Badge") => PendingBackground,
            (_, "Badge") => Color.FromArgb("#E5E7EB"),
            
            // Badge text colors
            ("Valid", "BadgeText") => Colors.White,
            ("Expired", "BadgeText") => Colors.White,
            ("Pending", "BadgeText") => Color.FromArgb("#134E4A"),
            (_, "BadgeText") => Color.FromArgb("#6B7280"),
            
            _ => Colors.Gray
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
