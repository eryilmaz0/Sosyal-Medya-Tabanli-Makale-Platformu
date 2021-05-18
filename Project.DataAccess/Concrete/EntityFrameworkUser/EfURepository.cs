using Project.Entities.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Project.Core.DataAccess.Repository;
using Project.Core.Entities;

namespace Project.DataAccess.EntityFrameworkWithSoftDelete
{
    public class EfURepository<TEntity> : IRepository<TEntity> where TEntity:class,IEntity<long>,new()
    {

        private readonly ProjectDbContext _context;

        //DI
        public EfURepository(ProjectDbContext context)
        {
            _context = context;
        }



        public EfURepository()
        {
            //DEFAULT
        }
        
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _context.Set<TEntity>().AsNoTracking();

            return predicate == null
                ? query.ToList()
                : query.Where(predicate).ToList();
        }




        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
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