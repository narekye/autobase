using DevExpress.Xpf.Core;
using System;
using System.Windows.Media.Imaging;

namespace AutoBase.LocalClient.Controls
{
    public class AdvWindow : DXWindow
    {
        public AdvWindow()
        {

            Loaded += AdvWindow_Loaded;
            Icon = new BitmapImage(new Uri("pack://application:,,,/Images/MainIco.ico", UriKind.Absolute));
        }

        void AdvWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ThemeManager.SetThemeName(this, Globals.DevExpressStyle);
            }
            catch (Exception)
            {
                ThemeManager.SetThemeName(this, "DeepBlue");
            }
        }
    }
}
