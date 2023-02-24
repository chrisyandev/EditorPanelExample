using Avalonia.Data.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace EditorPanelExample.Debugger
{
    public class DebuggerConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
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

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
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

        #endregion
    }
}
