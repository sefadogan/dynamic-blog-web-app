using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Services
{
    public class PostTagService : BaseService
    {
        public bool Add(PostTag data)
        {
            try
            {
                Context.PostTags.Add(data);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                // Exception ı da göndermek istesem nasıl yapabilirim?
                return false;
            }
        }
        public bool Update(PostTag data)
        {
            try
            {
                Context.PostTags.Attach(data);
                Context.Entry(data).State = System.Data.Entity.EntityState.Modified;
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
                PostTag postTag = Context.PostTags.Find(id);
                Context.PostTags.Remove(postTag);
                // postTag.IsActive = false; --> silmek için daha sonradan isActive eklenebilir.
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public PostTag BringById(int id)
        {
            return Context.PostTags.Find(id);
        }
        public List<PostTag> ListAll()
        {
            return Context.PostTags.ToList();
        }
    }
}
