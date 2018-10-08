using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Services
{
    public class TagService : BaseService
    {
        public bool Add(Tag data)
        {
            try
            {
                Context.Tags.Add(data);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(Tag data)
        {
            try
            {
                Context.Tags.Attach(data);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                Tag tag = Context.Tags.Find(id);
                Context.Tags.Remove(tag);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
