using System.Globalization;

namespace CleanProFinder.Mobile.Converters;

public class StatusToColorConverter : IValueConverter
{
    private static readonly Dictionary<string, string> statusToColor = new Dictionary<string, string>
    {
    {"Pending" /*"Placed"*/, "#DE5DF1"},
    {"Sent", "#FFC700"},
    {"Confirmed", "#2FD181"},
    {"Canceled" /*"Denied"*/, "#F54C64"},
    {"Done", "#7E6B5A"},
    {"Has answers", "#9631F5"}
    };

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string status && statusToColor.ContainsKey(status))
        {
            string colorCode = statusToColor[status];
            return Color.FromHex(colorCode);
        }

        return Color.FromHex("#1db29f");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
