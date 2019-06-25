using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BatchRenameMaterial
{
    class ConfigButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = (IStringProcessor)((Button)value).DataContext;
            var dialogType = MainWindow.GetDialogTypeFromProcessorType(ProcessorTypeDetector.GetType(item));
            if (dialogType != DialogType.NoDialog)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
