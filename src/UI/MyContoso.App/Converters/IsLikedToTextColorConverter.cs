using System.Globalization;

namespace MyContoso.App.Converters;

public class IsLikedToTextColorConverter: IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Colors.White : GetAppThemePrimaryColor();
        }
        
        throw new NotSupportedException();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static Color GetAppThemePrimaryColor()
    {
        var currentTheme = App.Current.RequestedTheme;

        if (App.Current.Resources.TryGetValue("Color.Primary", out var primary)
            && App.Current.Resources.TryGetValue("Color.Primary.Dark", out var primaryDark))
        {
            return currentTheme == AppTheme.Light ? (Color)primary : (Color)primaryDark;
        }
        
        throw new NotSupportedException($"Theme '{currentTheme}' is not supported.");
    }
}