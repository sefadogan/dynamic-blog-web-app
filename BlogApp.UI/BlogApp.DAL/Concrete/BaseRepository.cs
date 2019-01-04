using BlogApp.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Concrete
{
    public class BaseRepository<TEntity, TId, TContext> : IBaseRepository<TEntity, TId, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly DbContext _dbContext;

        private IDbSet<TEntity> Dbset
        {
            get { return _dbContext.Set<TEntity>(); }
        }

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(TEntity entity)
        {
            try
            {
                Dbset.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TEntity BringById(TId id)
        {
            return Dbset.Find(id);
        }

        public List<TEntity> ListAll()
        {
            return Dbset.ToList();
        }

        public bool Update(TEntity entity)
        {
            try
            {
                Dbset.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(TId id)
        {
            try
            {
                var entity = BringById(id);
                Dbset.Remove(entity);
                //category.IsActive = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
