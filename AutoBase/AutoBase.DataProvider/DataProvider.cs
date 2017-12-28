using System.Collections.ObjectModel;
using System.Data.Entity;
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
    }
}
