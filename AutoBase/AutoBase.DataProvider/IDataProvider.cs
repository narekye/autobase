using AutoBase.DAL;
using AutoBase.DAL.AutoBaseEntities;
using AutoBase.DAL.Filters;
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
        Task<ObservableCollection<Dump>> GetDumpsByFilter(FilterDump filter);
        Task<ObservableCollection<Model>> GetModelsForMake(int makeId);
        Task SaveModuleAsync(Module module);
        Task RemoveDump(Dump dump);
    }
}