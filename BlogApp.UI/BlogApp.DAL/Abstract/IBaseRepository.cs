using System.Collections.Generic;
using System.Data.Entity;

namespace BlogApp.DAL.Abstract
{
    public interface IBaseRepository<TEntity, TId, TContext> 
        where TEntity : class
        where TContext : DbContext
    {
        bool Add(TEntity data);
        bool Update(TEntity data);
        bool Delete(TId id);
        TEntity BringById(TId id);
        List<TEntity> ListAll();
    }
}
