using System;
using System.Collections.Generic;
using Entities;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class EFRepository : IRepository
    {
        private readonly SalesDbContext _dbContext;

        public EFRepository(SalesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity Create<TEntity>(TEntity toCreate) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(toCreate);
            _dbContext.SaveChanges();
            return toCreate;
        }

        public TEntity Retrieve<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(criteria);
        }

        public List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Where(criteria).ToList();
        }

        public bool Update<TEntity>(TEntity toUpdate) where TEntity : class
        {
            _dbContext.Entry(toUpdate).State = EntityState.Modified;
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete<TEntity>(TEntity toDelete) where TEntity : class
        {
            _dbContext.Entry(toDelete).State = EntityState.Deleted;
            return _dbContext.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}