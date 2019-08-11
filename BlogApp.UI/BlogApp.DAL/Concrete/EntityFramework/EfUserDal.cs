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
    public class EfUserDal : EfEntityRepositoryBase<User, BlogAppEntities>, IUserDal
    {
        private readonly BlogAppEntities _context;

        public EfUserDal(BlogAppEntities context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            try
            {
                User user = _context.Users.Find(id);
                user.IsActive = false;
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
