using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Abstract
{
    public interface IUserManager
    {
        bool Add(User data);
        bool Update(User data);
        bool Delete(int id);
        User BringById(int id);
        List<User> ListAll();
    }
}
