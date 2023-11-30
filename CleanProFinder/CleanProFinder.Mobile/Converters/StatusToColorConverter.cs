using System.Globalization;

namespace CleanProFinder.Mobile.Converters;

public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string status && Application.Current.Resources.TryGetValue(status, out var resource))
        {
            if (resource is Color statusColor)
            {
                return statusColor;
            }
        }

        return new Color(29, 178, 159);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
