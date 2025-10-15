using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Master_Floor_Project.Converters
{
    // Конвертер для выбора значения на основе наличия объекта (цвета, текст и т.д.)
    public class ObjectToValueConverter : IValueConverter
    {
        public static ObjectToValueConverter Instance { get; } = new ObjectToValueConverter();

        // Возвращает значение из параметра в формате "значение_при_наличии|значение_при_отсутствии"
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (parameter is string paramString)
            {
                var parts = paramString.Split('|'); // Разделяем параметр на две части
                if (parts.Length == 2)
                {
                    // Возвращаем первую часть при наличии объекта, вторую - при отсутствии
                    return value != null ? parts[0] : parts[1];
                }
            }

            // Значения по умолчанию для цветов
            return value != null ? "#67BA80" : "#CCCCCC"; // Зеленый при наличии, серый при отсутствии
        }

        // Обратное преобразование не реализовано
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}