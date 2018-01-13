// using AutoBase.DAL;
using System.Windows;

namespace AutoBase.LocalClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppCache.Init();
        }
    }
}
