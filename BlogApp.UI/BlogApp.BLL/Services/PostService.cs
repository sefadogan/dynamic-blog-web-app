using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Services
{
    public class PostService : BaseService
    {
        public bool Add(Post data)
        {
            try
            {
                Context.Posts.Add(data);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                // Exception ı da göndermek istesem nasıl yapabilirim?
                return false;
            }
        }
        public bool Update(Post data)
        {
            try
            {
                Context.Posts.Attach(data);
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
                Post post = Context.Posts.Find(id);
                //Context.Posts.Remove(post);
                post.IsActive = false;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public Post BringById(int id)
        {
            return Context.Posts.Find(id);
        }
        public List<Post> ListAll()
        {
            return Context.Posts.ToList();
        }
    }
}
