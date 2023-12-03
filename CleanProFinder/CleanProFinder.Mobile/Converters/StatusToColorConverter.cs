using System.Globalization;
using CleanProFinder.Shared.Enums;

namespace CleanProFinder.Mobile.Converters;

public class StatusToColorConverter : IValueConverter
{
    private static readonly Dictionary<RequestStatus, string> statusToColor = new Dictionary<RequestStatus, string>
    {
        { RequestStatus.Placed, "Heliotrope" },
        { RequestStatus.Sent, "Philippine Yellow" },
        { RequestStatus.Canceled, "Magic Potion" },
        { RequestStatus.Concluded, "Pastel Brown" },
        { RequestStatus.HasAnswers, "Purple (X11)" }
    };

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is RequestStatus status && Application.Current.Resources.TryGetValue(statusToColor[status], out var resource))
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
