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

        public static async void Init()
        {
            Modules = Globals.DataProvider.GetModules();
            Makes = await Globals.DataProvider.GetMakes();
        }

        public static ObservableCollection<Module> Modules { get; set; }
        public static ObservableCollection<Make> Makes { get; set; }
    }
}
