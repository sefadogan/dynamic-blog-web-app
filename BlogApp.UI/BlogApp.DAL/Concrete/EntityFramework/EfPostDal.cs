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
    public class EfPostDal : EfEntityRepositoryBase<Post, BlogAppEntities>, IPostDal
    {
        private readonly BlogAppEntities _context;

        public EfPostDal(BlogAppEntities context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            try
            {
                Post post = _context.Posts.Find(id);
                post.IsActive = false;
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
