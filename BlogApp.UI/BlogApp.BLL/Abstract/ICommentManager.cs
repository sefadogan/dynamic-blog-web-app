using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Abstract
{
    public interface ICommentManager
    {
        bool Add(Comment data);

        bool Update(Comment data);

        bool Delete(int id);

        Comment BringById(int id);

        List<Comment> ListAll();
    }
}
