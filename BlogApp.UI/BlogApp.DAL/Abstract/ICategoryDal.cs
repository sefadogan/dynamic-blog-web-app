using BlogApp.Core.DataAccess;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        bool Delete(int id);
    }
}
