using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Project.Core.Entities;

namespace Project.Core.DataAccess.Repository
{
    //TENTİTY REFERANS TİP OLMALI, IENTİTY LONG GENERİC TİPİNDE VE NEWLENEBİLİR OLMALI

    public interface IRepository<TEntity> where TEntity: class, IEntity<long>,new()
    {
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate=null);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);
        void Update(TEntity entity);
    }
}