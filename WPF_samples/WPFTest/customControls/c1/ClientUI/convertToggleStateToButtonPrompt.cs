﻿//        Another Demo from Andy L. & MissedMemo.com
// Borrow whatever code seems useful - just don't try to hold
// me responsible for any ill effects. My demos sometimes use
// licensed images which CANNOT legally be copied and reused.

using System.Windows.Data;
using System.Windows;


namespace ClientUI
{
    public class ToggleStateToButtonPromptConverter : IValueConverter
    {
        public object Convert( object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            return ((bool)value) ? "<" : ">";
        }


        public object ConvertBack( object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            throw new System.NotImplementedException();
        }
    }
}
