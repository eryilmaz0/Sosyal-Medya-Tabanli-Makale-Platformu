using System;

namespace Project.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        void Dispose();
    }
}