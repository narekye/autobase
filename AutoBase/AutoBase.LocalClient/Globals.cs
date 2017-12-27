using AutoBase.DAL;

namespace AutoBase.LocalClient
{
    public class Globals
    {
        public static IAutoBaseEntities Dal { get; set; }

        static Globals()
        {
            Dal = new DAL.AutoBaseEntities.AutoBaseEntities();
        }

        private static string _devExpressStyle = "Office2013";

        public static string DevExpressStyle
        {
            get { return _devExpressStyle; }
            set { _devExpressStyle = value; }
        }
    }
}
