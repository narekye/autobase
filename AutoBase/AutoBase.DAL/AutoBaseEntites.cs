using System;
using System.Threading.Tasks;

namespace AutoBase.DAL.AutoBaseEntities
{
    public partial class AutoBaseEntities : IAutoBaseEntities
    {
        public async Task Save<TEntity>(TEntity value) where TEntity : class                  
        {
            var state = Entry<TEntity>(value).State;
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
            ChangeTracker.DetectChanges();
            if (ChangeTracker.HasChanges())
            {
                return await SaveChangesAsync();
            }
            return 0;
        }
    }
}
