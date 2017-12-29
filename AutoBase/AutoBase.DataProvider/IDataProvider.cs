using AutoBase.DAL.AutoBaseEntities;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AutoBase.DataProvider
{
    public interface IDataProvider : IDisposable
    {
        Task<ObservableCollection<Dump>> GetDumps();
        Task<ObservableCollection<Make>> GetMakes();
        Task SaveMakesAsync(ObservableCollection<Make> makes);
    }
}