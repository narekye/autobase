using AutoBase.DAL.AutoBaseEntities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AutoBase.DataProvider
{
    public interface IDataProvider
    {
        Task<ObservableCollection<Dump>> Dumps();
        Task<ObservableCollection<Make>> Makes();
    }
}