using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Master_Floor_Project.Converters
{
    // Конвертер объекта в булево значение (true если не null)
    public class ObjectToBoolConverter : IValueConverter
    {
        public static ObjectToBoolConverter Instance { get; } = new ObjectToBoolConverter();

        // Преобразует объект в true/false в зависимости от наличия значения
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value != null; // true если объект существует, false если null
        }

        // Обратное преобразование не поддерживается
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}