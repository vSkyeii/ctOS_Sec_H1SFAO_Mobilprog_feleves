using System.Globalization;
using Microsoft.Maui.Controls;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public class BoolToTextConverter : IValueConverter
    {
        public string TrueText { get; set; } = "Active";
        public string FalseText { get; set; } = "Deactivated";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? TrueText : FalseText;
            }
            return FalseText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}