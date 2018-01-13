using AutoBase.DAL.AutoBaseEntities;
using System.Collections.ObjectModel;

namespace AutoBase.LocalClient
{
    public static class AppCache
    {
        static AppCache()
        {
            // Modules = Globals.DataProvider.GetModules().GetAwaiter().GetResult();
        }

        public static void Init()
        {
            Modules = Globals.DataProvider.GetModules();
        }

        public static ObservableCollection<Module> Modules { get; set; }
    }
}
