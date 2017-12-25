using AutoBase.DAL;
using System.Windows;

namespace AutoBase.LocalClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IAutoBaseEntities Db { get; set; }

        static App()
        {
            Db = new DAL.AutoBaseEntities.AutoBaseEntities();
        }
    }
}
