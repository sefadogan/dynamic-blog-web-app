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
    public class EfCommentDal : EfEntityRepositoryBase<Comment, BlogAppEntities>, ICommentDal
    {
        private readonly BlogAppEntities _context;

        public EfCommentDal(BlogAppEntities context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            try
            {
                Comment comment = _context.Comments.Find(id);
                comment.Status = 0;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
