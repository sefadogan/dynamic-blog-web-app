using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Concrete
{
    public class CategoryRepository : BaseRepository<Category, int, BlogAppEntities>, ICategoryRepository
    {
        public CategoryRepository(BlogAppEntities context) : base(context)
        {

        }
    }
}
