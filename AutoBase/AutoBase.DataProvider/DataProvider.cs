using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoBase.DAL;
using AutoBase.DAL.AutoBaseEntities;

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

        public ObservableCollection<Module> GetModules()
        {
            var data = Dal.Modules.ToListAsync().Result;
            return new ObservableCollection<Module>(data);
        }
        public async Task SaveMakeAsync(Make make)
        {
            await Dal.Save(make);
        }
    }
}
