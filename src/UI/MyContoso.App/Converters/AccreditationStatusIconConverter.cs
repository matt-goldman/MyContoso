using System.Globalization;
using Plugin.Maui.Lucide;

namespace MyContoso.App.Converters;

public class AccreditationStatusIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var status = value as string ?? "Unknown";
        return status switch
        {
            "Valid" => Icons.CircleCheck,
            "Expired" or "Overdue" => Icons.CircleAlert,
            "Pending" => Icons.Hourglass,
            _ => Icons.CircleQuestionMark
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}