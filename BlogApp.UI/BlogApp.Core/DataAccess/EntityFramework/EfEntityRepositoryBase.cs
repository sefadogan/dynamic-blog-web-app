using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
       where TEntity : class, new()
       //where TEntity : class, IEntity, new()
       where TContext : DbContext, new()
    {
        TContext context = new TContext();

        public bool Add(TEntity entity)
        {
            try
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;

                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return context.Set<TEntity>().SingleOrDefault(filter);
        }
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null 
                ? context.Set<TEntity>().ToList() 
                : context.Set<TEntity>().Where(filter).ToList();
        }
        public bool Update(TEntity entity)
        {
            try
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
