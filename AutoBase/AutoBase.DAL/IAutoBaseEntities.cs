using AutoBase.DAL.AutoBaseEntities;
using System;
using System.Data.Entity;

namespace AutoBase.DAL
{
    public interface IAutoBaseEntities : IDisposable
    {
        DbSet<Dump> Dumps { get; set; }
        DbSet<Make> Makes { get; set; }
        DbSet<Model> Models { get; set; }
        DbSet<Module> Modules { get; set; }
    }
}