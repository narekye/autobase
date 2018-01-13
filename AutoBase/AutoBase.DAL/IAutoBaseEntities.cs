using AutoBase.DAL.AutoBaseEntities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AutoBase.DAL
{
    public interface IAutoBaseEntities : IDisposable
    {
        DbSet<Dump> Dumps { get; set; }
        DbSet<Make> Makes { get; set; }
        DbSet<Model> Models { get; set; }
        DbSet<Module> Modules { get; set; }
        TEntity Find<TEntity, TKey>(params object[] keyValues) where TEntity : class where TKey : struct, IComparable, IEquatable<TKey>;
        TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;
        void SetValues(object from, object to);
        Task Save<TEntity>(TEntity value) where TEntity : class;
        Task<int> SaveChangesAsync();
        Task Remove<TEntity>(TEntity record) where TEntity : class;
    }
}