using System.Globalization;
using CleanProFinder.Shared.Enums;

namespace CleanProFinder.Mobile.Converters;

public class StatusToStringConverter : IValueConverter
{
    private static readonly Dictionary<RequestStatus, string> statusToString = new Dictionary<RequestStatus, string>
    {
        { RequestStatus.HasAnswers, "Has answers" }
    };

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is RequestStatus status && statusToString.TryGetValue(status, out var stringStatus))
        {
            return stringStatus;
        }

        return value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
