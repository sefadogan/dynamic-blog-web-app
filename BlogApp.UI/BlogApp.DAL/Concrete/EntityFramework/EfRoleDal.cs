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
    public class EfRoleDal : EfEntityRepositoryBase<Role, BlogAppEntities>, IRoleDal
    {
        private readonly BlogAppEntities _context;

        public EfRoleDal(BlogAppEntities context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            try
            {
                Role role = _context.Roles.Find(id);
                _context.Roles.Remove(role);
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
