using AutoBase.DAL;
using AutoBase.DataProvider;
using AutoBase.DataProvider.FileSystem;

namespace AutoBase.LocalClient
{
    public class Globals
    {
        public static IAutoBaseEntities Dal { get; set; }

        public static IDataProvider DataProvider { get; private set; }

        public static IFileSystem FileSystem { get; private set; }

        static Globals()
        {
            Dal = new DAL.AutoBaseEntities.AutoBaseEntities(Common.Constants.DbName);
            DataProvider = new DataProvider.DataProvider();
            FileSystem = new FileSystem(DataProvider);
        }

        private static string _devExpressStyle = "DeepBlue";

        public static string DevExpressStyle
        {
            get { return _devExpressStyle; }
            set { _devExpressStyle = value; }
        }
    }
}
