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
        private IAutoBaseEntities _dal;

        public DataProvider()
        {
            _dal = new AutoBaseEntities(Common.Constants.DbName);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ObservableCollection<Dump>> GetDumps()
        {
            return new ObservableCollection<Dump>(await _dal.Dumps.ToListAsync());
        }

        public async Task<ObservableCollection<Make>> GetMakes()
        {
            var data = await _dal.Makes.Include(x => x.Models).Include(x => x.Dumps).OrderBy(x => x.Id).ToListAsync();
            return new ObservableCollection<Make>(data);
        }

        public async Task SaveMakeAsync(Make make)
        {
            await _dal.Save(make);
        }
    }
}
