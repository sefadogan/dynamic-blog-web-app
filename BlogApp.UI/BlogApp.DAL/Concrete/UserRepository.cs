using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Concrete
{
    public class UserRepository : BaseRepository<User, int, BlogAppEntities>, IUserRepository
    {
        public UserRepository(BlogAppEntities context) : base(context)
        {

        }
    }
}
