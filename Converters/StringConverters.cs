using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Master_Floor_Project.Converters
{
    public static class StringConverters
    {
        // Конвертер проверки на null
        public static readonly IValueConverter IsNotNull = new IsNotNullConverter();
    }

    // Конвертер для проверки что строка не является null
    public class IsNotNullConverter : IValueConverter
    {
        // Возвращает true если строка не null, false если null
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value != null;
        }

        // Обратное преобразование не поддерживается
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}