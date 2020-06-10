using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace mobileApp
{
    class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            {
                byte[] bytes = value as byte[];

                if (bytes != null && bytes.Length!=0)
                {
                    return BytesToImageSource(bytes);
                }

                return "no_profile";
            }
        }
        private ImageSource BytesToImageSource(byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            return ImageSource.FromStream(() => stream);
            
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
