using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Abstract
{
    public interface IPostManager
    {
        bool Add(Post data);
        bool Update(Post data);
        bool Delete(int id);
        Post BringById(int id);
        List<Post> ListAll();
    }
}
