using AutoBase.DAL;
using AutoBase.DAL.AutoBaseEntities;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AutoBase.DataProvider
{
    public interface IDataProvider : IDisposable
    {
        IAutoBaseEntities Dal { get; }
        Task<ObservableCollection<Dump>> GetDumps();
        Task<ObservableCollection<Make>> GetMakes();
        Task<ObservableCollection<Model>> GetModels();
        ObservableCollection<Module> GetModules();
        Task SaveModelAsync(Model model);
        Task SaveMakeAsync(Make makee);
    }
}