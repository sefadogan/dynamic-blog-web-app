using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Abstract
{
    public interface ICommentRepository : IBaseRepository<Comment, int, BlogAppEntities>
    {

    }
}
