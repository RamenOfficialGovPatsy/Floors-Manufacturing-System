using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Master_Floor_Project.Converters
{
    public static class StringConverters
    {
        public static readonly IValueConverter IsNotNull = new IsNotNullConverter();
    }

    public class IsNotNullConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}