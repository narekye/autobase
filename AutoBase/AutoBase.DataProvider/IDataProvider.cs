using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AutoBase.DataProvider
{
    public interface IDataProvider
    {
        Task<ObservableCollection<DAL.AutoBaseEntities.Dump>> Dumps();
    }
}