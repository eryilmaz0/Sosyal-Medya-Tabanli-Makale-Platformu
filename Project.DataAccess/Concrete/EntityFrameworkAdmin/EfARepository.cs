using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Project.Core.DataAccess.Repository;
using Project.Core.Entities;
using Project.Entities.DbContext;

namespace Project.DataAccess.EntityFramework
{
    public class EfARepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity<long>, new()
    {

        private readonly ProjectDbContext _context;


        //DI
        public EfARepository(ProjectDbContext context)
        {
            _context = context;
        }



        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _context.Set<TEntity>().IgnoreQueryFilters().AsNoTracking();

            return predicate == null 
                ? query.ToList() 
                : query.Where(predicate).ToList();
        }



        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().IgnoreQueryFilters().AsNoTracking().FirstOrDefault(predicate);
        }




        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }




        public void AddRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

       


        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }




        public void RemoveRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }




        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}