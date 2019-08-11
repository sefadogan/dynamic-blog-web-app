using BlogApp.Core.DataAccess.EntityFramework;
using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, BlogAppEntities>, ICategoryDal
    {
        private readonly BlogAppEntities _context;

        public EfCategoryDal(BlogAppEntities context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            try
            {
                Category category = _context.Categories.Find(id);
                category.IsActive = false;
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
