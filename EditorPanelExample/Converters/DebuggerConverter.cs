using Avalonia.Data.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.Converters
{
    public class DebuggerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Set breakpoint here
            if (value is IEnumerable && !(value is string))
            {
                foreach (var item in value as IEnumerable)
                {
                    Debug.WriteLine(item);
                }
            }
            else
            {
                Debug.WriteLine(value);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Set breakpoint here
            if (value is IEnumerable && !(value is string))
            {
                foreach (var item in value as IEnumerable)
                {
                    Debug.WriteLine(item);
                }
            }
            else
            {
                Debug.WriteLine(value);
            }

            return value;
        }
    }
}
