using System;

namespace AutoBase.DAL.AutoBaseEntities
{
    public partial class AutoBaseEntities : IAutoBaseEntities
    {
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
    }
}
