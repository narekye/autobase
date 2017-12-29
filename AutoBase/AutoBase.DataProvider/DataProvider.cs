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
            _dal = new AutoBaseEntities();
        }

        public async Task<ObservableCollection<Dump>> Dumps()
        {
            return new ObservableCollection<Dump>(await _dal.Dumps.ToListAsync());
        }

        public async Task<ObservableCollection<Make>> Makes()
        {
            var data = await _dal.Makes.Include(x => x.Models).Include(x => x.Dumps).OrderBy(x => x.Id).ToListAsync();
            data.ForEach(dt =>
            {
                dt.ModelsCount = dt.Models.Count;
                dt.DumpsCount = dt.Dumps.Count;
            });
            return new ObservableCollection<Make>(data);
        }
    }
}
