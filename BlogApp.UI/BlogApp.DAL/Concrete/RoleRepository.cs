using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Concrete
{
    public class RoleRepository : BaseRepository<Role, int, BlogAppEntities>, IRoleRepository
    {
        public RoleRepository(BlogAppEntities context) : base(context)
        {

        }
    }
}
