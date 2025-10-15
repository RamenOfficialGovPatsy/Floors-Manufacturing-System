using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Master_Floor_Project.Converters
{
    // Конвертер для изменения цвета рамки при ошибках валидации
    public class ValidationToBorderBrushConverter : IValueConverter
    {
        public static ValidationToBorderBrushConverter Instance { get; } = new ValidationToBorderBrushConverter();

        // Возвращает красный цвет при ошибке валидации, серый - при отсутствии ошибок
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Если есть ошибка валидации - красная рамка, иначе стандартная
            return value != null ? Brushes.Red : Brushes.Gray;
        }

        // Обратное преобразование не поддерживается
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}