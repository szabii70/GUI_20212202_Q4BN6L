using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EscapeFromZaun.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)value)
            {
                string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                string newpath = System.IO.Path.GetFullPath(System.IO.Path.Combine(path, @"..\..\..\")) + @"\Views\Images\SoundOn.png";
                ImageBrush a = new ImageBrush(new BitmapImage(new Uri(newpath, UriKind.RelativeOrAbsolute)));
                return a;
            }
            else
            {
                string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                string newpath = System.IO.Path.GetFullPath(System.IO.Path.Combine(path, @"..\..\..\")) + @"\Views\Images\SoundOff.png";
                ImageBrush b = new ImageBrush(new BitmapImage(new Uri(newpath, UriKind.RelativeOrAbsolute)));
                return b;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
