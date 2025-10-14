// Converters/ValidationToBorderBrushConverter.cs
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Master_Floor_Project.Converters
{
    public class ValidationToBorderBrushConverter : IValueConverter
    {
        public static ValidationToBorderBrushConverter Instance { get; } = new ValidationToBorderBrushConverter();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Если есть ошибка валидации - красная рамка, иначе стандартная
            return value != null ? Brushes.Red : Brushes.Gray;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}