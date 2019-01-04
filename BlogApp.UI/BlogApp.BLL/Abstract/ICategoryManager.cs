using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Abstract
{
    public interface ICategoryManager
    {
        bool Add(Category data);
        bool Update(Category data);
        bool Delete(int id);
        Category BringById(int id);
        List<Category> ListAll();
    }
}
