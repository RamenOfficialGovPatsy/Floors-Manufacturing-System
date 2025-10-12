using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Master_Floor_Project.Converters
{
    public class ObjectToValueConverter : IValueConverter
    {
        public static ObjectToValueConverter Instance { get; } = new ObjectToValueConverter();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (parameter is string paramString)
            {
                var parts = paramString.Split('|');
                if (parts.Length == 2)
                {
                    return value != null ? parts[0] : parts[1];
                }
            }

            return value != null ? "#67BA80" : "#CCCCCC";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}