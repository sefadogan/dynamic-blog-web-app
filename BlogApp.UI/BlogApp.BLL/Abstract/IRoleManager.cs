using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Abstract
{
    public interface IRoleManager
    {
        bool Add(Role data);
        bool Update(Role data);
        bool Delete(int id);
        Role BringById(int id);
        List<Role> ListAll();
    }
}
