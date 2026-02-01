using System.Globalization;

namespace MyContoso.App.Converters;

public class IsLikedToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            if (App.Current.Resources.TryGetValue("Color.Primary", out var likedColor) && boolValue && likedColor is Color color)
            {
                return color;
            }

            return Colors.Transparent;
        }
        
        throw new NotSupportedException();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}