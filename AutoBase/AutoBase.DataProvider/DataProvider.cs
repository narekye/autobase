using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoBase.DAL;
using AutoBase.DAL.AutoBaseEntities;
using AutoBase.DAL.Filters;

namespace AutoBase.DataProvider
{
    public class DataProvider : IDataProvider
    {
        public DataProvider()
        {
            Dal = new AutoBaseEntities(Common.Constants.DbName);
        }

        public IAutoBaseEntities Dal
        {
            get; private set;
        }

        public void Dispose()
        {
            Dal.Dispose();
        }

        public async Task<ObservableCollection<Dump>> GetDumps()
        {
            return new ObservableCollection<Dump>(await Dal.Dumps.ToListAsync());
        }

        public async Task<ObservableCollection<Dump>> GetDumpsByFilter(FilterDump filter)
        {
            IQueryable<Dump> query = Dal.Dumps;
            var filteredData = filter.FilterObjects(query);
            return new ObservableCollection<Dump>(await filteredData.ToListAsync());
        }

        public async Task<ObservableCollection<Make>> GetMakes()
        {
            var data = await Dal.Makes.Include(x => x.Models).OrderBy(x => x.Id).ToListAsync();
            data.ForEach(x =>
            {
                x.DumpsCount = Dal.Dumps.Where(t => t.MakeId == x.Id).Count();
            });

            return new ObservableCollection<Make>(data);
        }

        public async Task<ObservableCollection<Model>> GetModels()
        {
            var data = await Dal.Models.Include(x => x.Make).ToListAsync();
            return new ObservableCollection<Model>(data);
        }

        public async Task<ObservableCollection<Model>> GetModelsForMake(int makeId)
        {
            var data = Dal.Models.Where(x => x.MakeId == makeId).ToListAsync();
            return new ObservableCollection<Model>(await data);
        }

        public ObservableCollection<Module> GetModules()
        {
            var data = Dal.Modules.ToListAsync().Result;
            return new ObservableCollection<Module>(data);
        }

        public async Task RemoveDump(Dump dump)
        {
            var record = Dal.Find<Dump>(dump.Id);
            if (record != null)
            {
                await Dal.Remove(record);
            }
        }

        public async Task SaveMakeAsync(Make make)
        {
            await Dal.Save(make);
        }

        public async Task SaveModelAsync(Model model)
        {
            await Dal.Save(model);
        }

        public async Task SaveModuleAsync(Module module)
        {
            await Dal.Save(module);
        }
    }
}
