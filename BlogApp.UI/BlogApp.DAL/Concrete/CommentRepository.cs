using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Concrete
{
    public class CommentRepository : BaseRepository<Comment, int, BlogAppEntities>, ICommentRepository
    {
        public CommentRepository(BlogAppEntities context) : base(context)
        {

        }
    }
}
