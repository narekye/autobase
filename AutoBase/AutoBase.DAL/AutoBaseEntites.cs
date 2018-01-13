using System;
using System.Threading.Tasks;

namespace AutoBase.DAL.AutoBaseEntities
{
    public partial class AutoBaseEntities : IAutoBaseEntities
    {
        private static IAutoBaseEntities _instance;
        public AutoBaseEntities(string dbName) : base(dbName)
        {
            if (_instance == null)
            {
                Configuration.AutoDetectChangesEnabled = false;
                Configuration.LazyLoadingEnabled = false;
                Configuration.ProxyCreationEnabled = false;
                _instance = this;
            }
        }

        public async Task Save<TEntity>(TEntity value) where TEntity : class
        {
            var state = Entry<TEntity>(value).State;

            if (state == System.Data.Entity.EntityState.Unchanged) return;

            if (state == System.Data.Entity.EntityState.Detached)
            {
                Set<TEntity>().Add(value);
            }

            else if (state == System.Data.Entity.EntityState.Deleted)
            {
                Set<TEntity>().Remove(value);
            }

            await SaveChangesAsync();
        }

        public TEntity Find<TEntity, TKey>(params object[] keyValues)
           where TEntity : class
           where TKey : struct, IComparable, IEquatable<TKey>
        {
            return Find<TEntity>(keyValues);
        }

        public TEntity Find<TEntity>(params object[] keyValues) where TEntity : class
        {
            return Set<TEntity>().Find(keyValues);
        }

        public void SetValues(object from, object to)
        {
            Entry(to).CurrentValues.SetValues(from);
        }

        public new async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        
        public async Task Remove<TEntity>(TEntity record) where TEntity : class
        {
            if (record != null)
                Set<TEntity>().Remove(record);

            await SaveChangesAsync();
        }
    }
}
