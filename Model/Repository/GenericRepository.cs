﻿using Elfie.Serialization;
using EventSeller.Model.EF;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq.Expressions;
using System.Reflection;

namespace EventSeller.Model.Repository
{
    public class GenericRepository<TEntity> where TEntity : class,IEntity
    {
        internal SellerContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(SellerContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            var existingEntity = dbSet.FirstOrDefault(e => e.ID==entityToUpdate.ID);
            var sourceProperties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProperty in sourceProperties)
            {
                if (sourceProperty.CanWrite)
                {
                    var value = sourceProperty.GetValue(entityToUpdate);
                    sourceProperty.SetValue(existingEntity, value);
                }
            }
            dbSet.Attach(existingEntity);
            context.Entry(existingEntity).State = EntityState.Modified;
        }
    }
}
