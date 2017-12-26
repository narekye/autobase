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
    }
}
