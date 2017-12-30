using AutoBase.DAL;
using AutoBase.DataProvider;

namespace AutoBase.LocalClient
{
    public class Globals
    {
        public static IAutoBaseEntities Dal { get; set; }

        public static IDataProvider DataProvider { get; private set; }
        static Globals()
        {
            Dal = new DAL.AutoBaseEntities.AutoBaseEntities();
            DataProvider = new DataProvider.DataProvider();
        }

        private static string _devExpressStyle = "DeepBlue";

        public static string DevExpressStyle
        {
            get { return _devExpressStyle; }
            set { _devExpressStyle = value; }
        }
    }
}
