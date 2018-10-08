using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Services
{
    public class CommentService : BaseService
    {
        public bool Add(Comment data)
        {
            try
            {
                Context.Comments.Add(data);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                // Exception ı da göndermek istesem nasıl yapabilirim?
                return false;
            }
        }
        public bool Update(Comment data)
        {
            try
            {
                Context.Comments.Attach(data);
                Context.Entry(data).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                // Exception ı da göndermek istesem nasıl yapabilirim?
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                Comment comment = Context.Comments.Find(id);
                //Context.Comments.Remove(comment);
                comment.Status = 0; // 0 : passive.
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public Comment BringById(int id)
        {
            return Context.Comments.Find(id);
        }
        public List<Comment> ListAll()
        {
            return Context.Comments.ToList();
        }
    }
}
